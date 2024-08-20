using System.Text;

using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using iText.Kernel.Pdf.Xobject;

using MyRag.Application.Common.RagChain.Interfaces;

using SkiaSharp;

namespace MyRag.Infrastructure.RagChain.Extractors;

public class IText7PdfExtractor : IExtractor
{
    public async Task<string> Extract(Stream stream)
    {
        var result = new StringBuilder();

        using (var pdfReader = new PdfReader(stream))
        using (var pdfDocument = new PdfDocument(pdfReader))
        {
            int pageCount = pdfDocument.GetNumberOfPages();
            for (int i = 1; i <= pageCount; i++)
            {
                var page = pdfDocument.GetPage(i);

                var strategy = new SimpleTextExtractionStrategy();
                var text = PdfTextExtractor.GetTextFromPage(page, strategy);
                result.AppendLine(text);

                var resources = page.GetResources();
                var imageFiles = await ExtractImagesFromResources(resources);
                foreach (var imageFile in imageFiles)
                {
                    result.AppendLine($"[Image: {imageFile}]");
                }
            }
        }
        return result.ToString();
    }

    private async Task<List<string>> ExtractImagesFromResources(PdfResources resources)
    {
        var imageFiles = new List<string>();
        var resourceNames = resources.GetResourceNames();

        foreach (var name in resourceNames)
        {
            PdfStream stream = resources.GetResource(PdfName.XObject) as PdfStream;
            if (stream != null)
            {
                PdfObject obj = stream.Get(name);
                if (obj != null && obj.IsStream())
                {
                    PdfStream imgStream = (PdfStream)obj;
                    if (PdfName.Image.Equals(imgStream.GetAsName(PdfName.Subtype)))
                    {
                        try
                        {
                            var imageXObject = new PdfImageXObject(imgStream);
                            var imagePath = await SaveImage(imageXObject);
                            imageFiles.Add(imagePath);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error processing image: {ex.Message}");
                        }
                    }
                }
            }
        }

        return imageFiles;
    }

    private async Task<string> SaveImage(PdfImageXObject imageXObject)
    {
        var imageBytes = imageXObject.GetImageBytes();
        using (var inputStream = new MemoryStream(imageBytes))
        using (var skiaImage = SKImage.FromEncodedData(inputStream))
        {
            var fileName = $"image_{Guid.NewGuid()}.png";
            var filePath = Path.Combine(Path.GetTempPath(), fileName);

            using (var outputStream = File.OpenWrite(filePath))
            using (var data = skiaImage.Encode(SKEncodedImageFormat.Png, 100))
            {
                data.SaveTo(outputStream);
            }

            return filePath;
        }
    }
}
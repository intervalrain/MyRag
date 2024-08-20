using MyRag.Application.Common.RagChain.Interfaces;

namespace MyRag.Infrastructure.RagChain.Extractors;

public class SimplePdfExtractor : IExtractor
{
    public Task<string> Extract(Stream stream)
    {
        return Task.FromResult("This is the content of the PDF"); 
    }
}


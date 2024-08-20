using ErrorOr;

using MediatR;

using MyRag.Application.Common.RagChain.Interfaces;

namespace MyRag.Application.Chat.Commands.EmbeddingPdfs;

public class EmbeddingPdfsCommandHandler : IRequestHandler<EmbeddingPdfsCommand, ErrorOr<Unit>>
{
    private readonly IDocumentStore _documentStore;
    private readonly IEmbeddingGenerator _embeddingGenerator;
    private readonly IExtractor _extractor;
    private readonly ITextSplitter _textSplitter;

    public EmbeddingPdfsCommandHandler(IDocumentStore documentStore, IEmbeddingGenerator embeddingGenerator, IExtractor extractor, ITextSplitter textSplitter)
    {
        _documentStore = documentStore;
        _embeddingGenerator = embeddingGenerator;
        _extractor = extractor;
        _textSplitter = textSplitter;
    }

    public async Task<ErrorOr<Unit>> Handle(EmbeddingPdfsCommand request, CancellationToken cancellationToken)
    {
        if (request.ForceUpdate)
        {
            await _documentStore.ClearAll();
        }

        var files = Directory.GetFiles(request.FilePath);

        foreach (var path in files)
        {
            using (var stream = File.OpenRead(path))
            {
                var content = await _extractor.Extract(stream);
                var chunks = _textSplitter.Split(content);

                foreach (var chunk in chunks)
                {
                    var embedding = await _embeddingGenerator.GenerateEmbedding(chunk);
                    await _documentStore.StoreDocument(Guid.NewGuid().ToString(), embedding, chunk);
                }
            }
        }

        return Unit.Value;
    }
}


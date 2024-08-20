using ErrorOr;

using MediatR;

using MyRag.Application.Common.RagChain.Interfaces;

namespace MyRag.Application.Chat.Queries.SendMessages;

public class SendMessagesQueryHandler : IRequestHandler<SendMessagesQuery, ErrorOr<string>>
{
    private readonly IDocumentStore _documentStore;
    private readonly IEmbeddingGenerator _embeddingGenerator;
    private readonly ILlmService _llmService;

    public SendMessagesQueryHandler(IDocumentStore documentStore, IEmbeddingGenerator embeddingGenerator, ILlmService llmService)
    {
        _documentStore = documentStore;
        _embeddingGenerator = embeddingGenerator;
        _llmService = llmService;
    }

    public async Task<ErrorOr<string>> Handle(SendMessagesQuery request, CancellationToken cancellationToken)
    {
        var embedding = await _embeddingGenerator.GenerateEmbedding(request.Message);
        var similarDocuments = await _documentStore.SearchSimilarDocuments(embedding, request.TopK);

        var prompt = ConstructPrompt(request.Message, similarDocuments);

        return await _llmService.GenerateResponse(prompt);
    }

    private string ConstructPrompt(string message, List<(string documentId, float similarity, string content)> similarDocuments)
    {
        return $"Context: {string.Join("\n", similarDocuments.Select(d => d.content))}\n\n System Prompt: Please answer the question based on context\n\n User Prompt: {message}";
    }
}


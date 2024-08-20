using MyRag.Application.Common.RagChain.Interfaces;
using MyRag.Util.VectorTools;

namespace MyRag.Infrastructure.RagChain.DocumentStores;

public class InMemoryDocumentStore : IDocumentStore
{
    private static readonly Dictionary<string, (float[] embedding, string content)> _documents = new(); 

    public Task ClearAll()
    {
        _documents.Clear();
        return Task.CompletedTask;
    }

    public Task DeleteDocument(string documentId)
    {
        _documents.Remove(documentId);
        return Task.CompletedTask;
    }

    public Task<List<(string documentId, float similarity, string content)>> SearchSimilarDocuments(float[] queryEmbedding, int topK)
    {
        var similarities = _documents.Select(kvp => (
            kvp.Key,
            similarity: Vector.CosineSimilarity(queryEmbedding, kvp.Value.embedding)))
            .OrderByDescending(x => x.similarity)
            .Take(topK);

        var results = similarities.Select(x => (x.Key, x.similarity, _documents[x.Key].content))
                .ToList();
        return Task.FromResult(results);
    }

    public Task StoreDocument(string documentId, float[] embedding, string content)
    {
        _documents[documentId] = (embedding, content);
        return Task.CompletedTask;
    }
}


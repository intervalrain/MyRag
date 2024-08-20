namespace MyRag.Application.Common.RagChain.Interfaces;

public interface IDocumentStore
{
    Task StoreDocument(string documentId, float[] embedding, string content);
    Task<List<(string documentId, float similarity, string content)>> SearchSimilarDocuments(float[] queryEmbedding, int topK);
    Task ClearAll();
    Task DeleteDocument(string documentId);
}


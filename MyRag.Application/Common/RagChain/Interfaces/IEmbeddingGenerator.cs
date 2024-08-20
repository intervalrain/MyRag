namespace MyRag.Application.Common.RagChain.Interfaces;

public interface IEmbeddingGenerator
{
    Task<float[]> GenerateEmbedding(string text);
}


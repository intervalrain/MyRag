using MyRag.Application.Common.RagChain.Interfaces;

namespace MyRag.Infrastructure.RagChain.EmbeddingGenerators;

public class SimpleEmbeddingGenerator : IEmbeddingGenerator
{
    public Task<float[]> GenerateEmbedding(string text)
    {
        var random = new Random();
        return Task.FromResult(Enumerable.Range(0, 10).Select(_ => (float)random.NextDouble()).ToArray());
    }
}


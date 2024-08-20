using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using MyRag.Application.Common.RagChain.Interfaces;
using MyRag.Infrastructure.RagChain.DocumentStores;
using MyRag.Infrastructure.RagChain.EmbeddingGenerators;
using MyRag.Infrastructure.RagChain.LlmServices;
using MyRag.Infrastructure.RagChain.Extractors;
using MyRag.Infrastructure.RagChain.TextSplitters;

namespace MyRag.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureRagChain();

        return services;
    }

    public static IServiceCollection ConfigureRagChain(this IServiceCollection services)
    {
        services.AddSingleton<IDocumentStore, InMemoryDocumentStore>();
        services.AddSingleton<IEmbeddingGenerator, SimpleEmbeddingGenerator>();
        services.AddSingleton<ILlmService, SimpleLlmService>();
        services.AddSingleton<IExtractor, IText7PdfExtractor>();
        services.AddSingleton<ITextSplitter, SimpleTextSplitter>();

        return services;
    }
}
using MyRag.Application.Common.RagChain.Interfaces;

namespace MyRag.Infrastructure.RagChain.LlmServices;

public class SimpleLlmService : ILlmService
{
    public SimpleLlmService()
    {
    }

    public Task<string> GenerateResponse(string prompt)
    {
        return Task.FromResult($"Response to: {prompt}\n\n");
    }
}


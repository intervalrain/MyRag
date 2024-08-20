namespace MyRag.Application.Common.RagChain.Interfaces;

public interface ILlmService
{
    Task<string> GenerateResponse(string prompt);
}


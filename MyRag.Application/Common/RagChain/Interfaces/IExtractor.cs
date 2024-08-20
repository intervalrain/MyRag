namespace MyRag.Application.Common.RagChain.Interfaces;

public interface IExtractor
{
    Task<string> Extract(Stream stream);
}


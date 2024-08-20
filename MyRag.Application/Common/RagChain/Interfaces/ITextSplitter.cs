namespace MyRag.Application.Common.RagChain.Interfaces;

public interface ITextSplitter
{
    List<string> Split(string text);
}


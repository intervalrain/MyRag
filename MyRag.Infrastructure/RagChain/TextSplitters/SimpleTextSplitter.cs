using MyRag.Application.Common.RagChain.Interfaces;

namespace MyRag.Infrastructure.RagChain.TextSplitters;

public class SimpleTextSplitter : ITextSplitter
{
    public List<string> Split(string text)
    {
        return text.Split('\n', StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim()).ToList();
    }
}


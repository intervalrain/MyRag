using ErrorOr;

using MediatR;

namespace MyRag.Application.Chat.Commands.EmbeddingPdfs;

public record EmbeddingPdfsCommand(string FilePath, bool ForceUpdate) : IRequest<ErrorOr<Unit>>;
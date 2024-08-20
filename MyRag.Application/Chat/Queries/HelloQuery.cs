using ErrorOr;

using MediatR;

namespace MyRag.Application.Chat.Queries;

public record HelloQuery() : IRequest<ErrorOr<string>>;
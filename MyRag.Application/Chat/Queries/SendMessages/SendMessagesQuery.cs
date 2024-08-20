using ErrorOr;

using MediatR;

namespace MyRag.Application.Chat.Queries.SendMessages;

public record SendMessagesQuery(string Message, int TopK) : IRequest<ErrorOr<string>>;
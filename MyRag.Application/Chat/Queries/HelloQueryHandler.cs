using ErrorOr;

using MediatR;

namespace MyRag.Application.Chat.Queries;

public class HelloQueryHandler : IRequestHandler<HelloQuery, ErrorOr<string>>
{
    public Task<ErrorOr<string>> Handle(HelloQuery request, CancellationToken cancellationToken)
    {
        var helloResult = "HelloWorld";

        return Task.FromResult(ErrorOrFactory.From(helloResult));
    }
}
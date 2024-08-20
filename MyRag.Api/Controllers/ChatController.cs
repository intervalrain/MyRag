using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using MyRag.Application.Chat.Queries;

namespace MyRag.Api.Controllers;

[Route("chat")]
[AllowAnonymous]
public class ChatController : ApiController
{
    private readonly IMediator _mediator;

    public ChatController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("hello")]
    public async Task<IActionResult> Hello()
    {
        var query = new HelloQuery();

        var result = await _mediator.Send(query);

        return result.Match(Ok, Problem);
    }
}


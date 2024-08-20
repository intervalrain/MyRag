using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using MyRag.Application.Chat.Commands.EmbeddingPdfs;
using MyRag.Application.Chat.Queries.SendMessages;

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

    [HttpGet("send")]
    public async Task<IActionResult> SendMessage(string message, int topK)
    {
        var query = new SendMessagesQuery(message, topK);

        var result = await _mediator.Send(query);

        return result.Match(Ok, Problem);
    }

    [HttpPost("update")]
    public async Task<IActionResult> UpdateEmbeddings(string filePath, bool forceUpdate)
    {
        var command = new EmbeddingPdfsCommand(filePath, forceUpdate);

        var result = await _mediator.Send(command);

        return result.Match(unit => Ok(unit), Problem);
    }
}


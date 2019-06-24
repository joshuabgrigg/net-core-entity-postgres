using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

public class MessagesController : BaseController
{
    private readonly IMessageRepository _messageRepository;

    public MessagesController(IMessageRepository messageRepository)
    {
        this._messageRepository = messageRepository;
    }

    // GET: api/Messages
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Message>>> Get()
    {
        var messages = await this._messageRepository.FindAll();
        return Ok(messages);
    }

    // GET: api/Messages/<id>
    [HttpGet("{id}")]
    public async Task<ActionResult<Message>> Get(Guid id)
    {
        var message = await this._messageRepository.Find(id);

        if (message == null)
        {
            return NotFound();
        }
        else
        {
            return Ok(message);
        }
    }

    // POST: api/Messages
    [HttpPost]
    public async Task<ActionResult<Message>> Post(string text)
    {
        return Ok(await this._messageRepository.Add(new Message() { Text = text }));
    }

    // PUT: api/Messages/<id>
    [HttpPut("{id}")]
    public async Task<ActionResult<Message>> Put(Guid id, string text)
    {
        var message = await this._messageRepository.Find(id);
        if (message == null)
        {
            return NotFound();
        }
        else
        {
            message.Text = text;
            await this._messageRepository.Update(message);
            return Ok(message);
        }
    }

    // DELETE: api/Messages/<id>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var message = await _messageRepository.Find(id);
        if (message == null)
        {
            return NotFound();
        }
        else
        {
            await _messageRepository.Remove(message);
            return NoContent();
        }
    }

    [HttpGet]
    [Route("echo/{content}")]
    public IActionResult Echo(string content)
    {
        return Ok(content);
    }
}
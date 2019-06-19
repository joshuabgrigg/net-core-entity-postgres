using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

public class MessagesController : BaseController
{
    private readonly IMessageRepository _messageRepository;

    public MessagesController(IMessageRepository messageRepository)
    {
        this._messageRepository = messageRepository;
    }

    [HttpPost]
    [Route("[controller]")]
    public Message Post(string text)
    {
        var message = this._messageRepository.Add(new Message()
        {
            Text = text
        });

        return message;
    }

    [HttpGet]
    [Route("[controller]/")]
    public List<Message> Index()
    {
        return this._messageRepository.FindAll().ToList(); ;
    }


    [HttpGet]
    [Route("[controller]/{id}")]
    public Message Get(Guid id)
    {
        return this._messageRepository.Find(id);
    }

    [HttpPut]
    [Route("[controller]/{id}")]
    public Message Update(Guid id, string text)
    {
        var message = this._messageRepository.Find(id);

        if(message != null) {
            message.Text = text;
            message = this._messageRepository.Update(message);
        }

        return message;
    }

    [HttpDelete]
    [Route("[controller]/{id}")]
    public void Delete(Guid id)
    {
        this._messageRepository.Remove(id);
    }
}
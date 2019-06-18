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
    public List<Message> Index()
    {
        return this._messageRepository.FindAll().ToList();
    }

    [HttpPost]
    public Message Index(string text)
    {
        var message = this._messageRepository.Add(new Message()
        {
            Text = text
        });

        return message;
    }
}
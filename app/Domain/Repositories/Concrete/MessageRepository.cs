using System;
using System.Linq;

public class MessageRepository : Repository<Message>, IMessageRepository
{
    public MessageRepository(AppPostgresContext context) : base(context)
    {
    }

    public Message Find(Guid id)
    {
        return this.FindAll(m => m.MessageID == id).FirstOrDefault();
    }

    public void Remove(Guid id)
    {
        var message = this.FindAll(m => m.MessageID == id).FirstOrDefault();
        if(message != null) {
            this.Remove(message);
        }
    }
}
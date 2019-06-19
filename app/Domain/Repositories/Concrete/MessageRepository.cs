using System;
using System.Linq;
using System.Threading.Tasks;

public class MessageRepository : Repository<Message>, IMessageRepository
{
    public MessageRepository(AppPostgresContext context) : base(context)
    {
    }

    public async Task<Message> Find(Guid id)
    {
        return (await this.FindAll(m => m.MessageID == id)).FirstOrDefault();
    }

    public async Task Remove(Guid id)
    {
        var message = await this.Find(id);
        if(message != null) {
            await this.Remove(message);
        }
    }
}
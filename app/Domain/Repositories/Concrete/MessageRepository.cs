public class MessageRepository : Repository<Message>, IMessageRepository
{
    public MessageRepository(AppPostgresContext context) : base(context)
    {
    }
}
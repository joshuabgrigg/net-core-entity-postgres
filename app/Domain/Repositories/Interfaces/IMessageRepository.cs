using System;

public interface IMessageRepository: IRepository<Message>, IPrimaryIDRepository<Guid, Message>
{
    
}
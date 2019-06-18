using System;

public class Message {
    public Guid MessageID { get; set;}
    public string Text { get; set; }
    public DateTime CreatedTime { get; set; }

    public Message() {
        this.MessageID = Guid.NewGuid();
        this.CreatedTime = DateTime.Now;
    }
}
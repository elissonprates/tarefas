namespace Contracts;

public class MessageInQueue
{
    public DateTime event_date { get; set; }
    public string event_name { get; set; }
}
public class MessageInQueue<T> : MessageInQueue
{
    public MessageInQueue(string event_name, T event_content)
    {
        this.event_date = DateTime.Now;
        this.event_name = event_name;
        this.event_content = event_content;
    }
    public T event_content { get; set; }
}

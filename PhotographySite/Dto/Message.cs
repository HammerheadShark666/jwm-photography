namespace PhotographySite.Dto;

public class Message
{
    public string Text { get; set; }
    public string Severity { get; set; }

    public Message() { }

    public Message(string text, string serverity)
    {
        Text = text;
        Severity = serverity;
    }
}
public class SendGridOptions
{
    public bool IsEnabled { get; set; }
    public string ApiKey { get; set; } = "";
    public string FromName { get; set; } = "";
    public string FromAddress { get; set; } = "";
    public string ToName { get; set; } = "";
    public string ToAddress { get; set; } = "";
}

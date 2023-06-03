namespace UCABPagaloTodoMS.Infrastructure.Options
{
    public class EmailOptions
    {
        public string? FromName { get; set; }
        public string? FromAddress { get; set; }
        public string? SmtpServer { get; set; }
        public int SmtpPort { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
    }
}
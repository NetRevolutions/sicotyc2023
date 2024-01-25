namespace Entities.Models
{
    public class EmailMetadata
    {
        public List<EmailItem>? ToAddress { get; set; }
        public List<EmailItem>? ToCC { get; set; }
        public List<EmailItem>? ToBcc { get; set; }
        public string Subject { get; set; }
        public string? Body { get; set; }
        public bool IsHtml { get; set; }
        public string? AttachmentPath { get; set; }

        public EmailMetadata(List<EmailItem>? toAddress, List<EmailItem>? toCC, List<EmailItem>? toBcc, string subject, string? body="", bool isHtml=false, string? attachmentPath = "")
        {
            ToAddress = toAddress;
            ToCC = toCC;
            ToBcc = toBcc;
            Subject = subject;
            Body = body;
            IsHtml = isHtml;
            AttachmentPath = attachmentPath;
        }
    }

    public class EmailItem
    { 
        public string? Email { get; set; }
        public string? Name { get; set; } = null;
    }
}

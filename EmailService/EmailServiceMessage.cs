using System.Net.Mail;
using System.Net;

namespace EmailServices;

public class EmailServiceMessage
{
    public string EmailTo { get; set; }
    public string EmailFrom { get; set; }
    public string MessageBody { get; set; }
}

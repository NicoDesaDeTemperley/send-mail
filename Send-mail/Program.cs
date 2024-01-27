using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Utils;


List<MimeMessage> mimeMessages = new List<MimeMessage>();

var contentId = MimeUtils.GenerateMessageId();

var message = new MimeMessage();
message.From.Add(new MailboxAddress("Joey", "soyunsoftware@friends.com"));
message.To.Add(new MailboxAddress("Nico", "nicouzu96@hotmail.es"));
message.Subject = "How you doin?";

var builder = new BodyBuilder();
 
builder.TextBody = @"Hey Alice,

What are you up to this weekend? Monica is throwing one of her parties on
Saturday and I was hoping you could make it.

Will you be my +1?

-- Joey
";
 
builder.HtmlBody = string.Format(@"<p>Hey Alice,<br>
<p>What are you up to this weekend? Monica is throwing one of her parties on
Saturday and I was hoping you could make it.<br>
<p>Will you be my +1?<br>
<p>-- Joey<br>
", contentId);

// Since selfie.jpg is referenced from the html text, we'll need to add it
// to builder.LinkedResources and then set the Content-Id header value
//builder.LinkedResources.Add(@"C:\Users\Joey\Documents\Selfies\selfie.jpg");
 

// We may also want to attach a calendar event for Monica's party...
builder.Attachments.Add(@"C:\Users\vamoooooo\Downloads\NicoAcostaCV.pdf");

// Now we just need to set the message body and we're done
message.Body = builder.ToMessageBody();

mimeMessages.Add(message);

using (var client = new SmtpClient())
{
        client.Connect("smtp.gmail.com", 465, SecureSocketOptions.SslOnConnect);

        client.Authenticate("nicolas.ksablancas@gmail.com", "Nicolas123");

        foreach (var msg in mimeMessages)
        {
            client.Send(msg);
        }

        client.Disconnect(true);
}
 
using System.Net.Mail;
using System.Net;
using SendGrid;
using SendGrid.Helpers.Mail;

public class EmailService
{
    private readonly string _apiKey;

    public EmailService(string apiKey)
    {
        _apiKey = apiKey;
    }
    public async Task SendEmailAsync(string email, string subject, string htmlMessage, List<string> attachmentPaths)
    {
        var client = new SendGridClient(_apiKey);
        var from = new EmailAddress("noreply@example.com");
        var to = new EmailAddress(email);
        var msg = MailHelper.CreateSingleEmail(from, to, subject, null, htmlMessage);

        foreach (var attachmentPath in attachmentPaths)
        {
            var bytes = await File.ReadAllBytesAsync(attachmentPath);
            var file = Convert.ToBase64String(bytes);
            msg.AddAttachment(Path.GetFileName(attachmentPath), file);
        }

        try
        {
            await client.SendEmailAsync(msg);
        }
        catch (Exception ex)
        {
           
            Console.WriteLine($"An error occurred when sending the email: {ex.Message}");
        }
    }

}

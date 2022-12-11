using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using Serilog;

try
{
    var config = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json")
        .AddUserSecrets(typeof(Program).Assembly)
        .Build();

    Log.Logger = new LoggerConfiguration()
        .ReadFrom.Configuration(config)
        .CreateLogger();

    var sendGridOptions = config.GetSection("SendGrid").Get<SendGridOptions>()
        ?? throw new Exception("Missing SendGrid appsettings");

    Log.Information("Starting sample app.");

    using var message = new MimeMessage();
    message.From.Add(new MailboxAddress(
        sendGridOptions.FromName,
        sendGridOptions.FromAddress
    ));
    message.To.Add(new MailboxAddress(
        sendGridOptions.ToName,
        sendGridOptions.ToAddress
    ));
    message.Subject = "Demo from SampleMail.App";
    message.Body = new BodyBuilder
    {
        TextBody = "This should work, as it's an email via SendGrid.\n\nThe plain text version, that is. The HTML contains far more details.",
        HtmlBody = @$"
            <div style=""border: 1px solid #1F618D; background: #EAF2F8; max-width: 720px;"">
                <div style=""background: #154360; border: 10px solid #154360; color: #FEF9E7; text-align: center; font-size: 18px; font-weight: bold;"">
                    SampleMail.App Announcement
                </div>
                <div style=""background: #EAF2F8; border: 10px solid #EAF2F8; font-size: 15px"">
                    <p>Hi folks,</p>
                    <p>
                        Let's see if the HTML version of SendGrid email works, shall we?
                        It should present a rather <strong>strong</strong> message, generated at UTC: <code>{DateTime.UtcNow}</code>.
                    </p>
                    <p>
                        You can check out <a href=""https://github.com/jeroenheijmans/sample-sendgrid-mailkit-app"">the source code on GitHub</a> (note that we expect that link to include click tracking from SendGrid).
                        Cloning that and adding User Secrets to override the appsettings allows you to check it for yourself.
                        Note that you need your own SendGrid account and API key to test with.
                    </p>
                    <p>Thanks for reading this!</p>
                    <p>Kind regards,<br>SampleMail.App</p>
                </div>
            </div>
            <p>&nbsp;</p>
            <p><small>This e-mail contains no unsubscribe or similar, because you should be sending it only to yourself, not other people.</small></p>",
    }.ToMessageBody();

    using var client = new SmtpClient();

    Log.Information("Connecting with SendGrid SMTP server...");
    await client.ConnectAsync("smtp.sendgrid.net", 587, SecureSocketOptions.StartTls);
    await client.AuthenticateAsync(
        userName: "apikey", // must be exactly this string, see SendGrid docs
        password: sendGridOptions.ApiKey
    );
    Log.Information("Successfully connected!");

    Log.Information("Preparing to send e-mail...");
    if (sendGridOptions.IsEnabled)
    {
        Log.Information("Sending e-mail with SendGrid...");
        await client.SendAsync(message);
    }
    else
    {
        Log.Warning("Sending suppressed with settings. Mail message was:\n{Message}", message);
    }
    Log.Information("Successfully sent!");

    await client.DisconnectAsync(true);
}
catch (Exception exception)
{
    Log.Error("Crashed while running app.", exception);
    throw;
}

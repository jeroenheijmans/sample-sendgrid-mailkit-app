# Sample SendGrid and MailKit Application

Demonstrates how to send SMTP e-mail with SendGrid and Mailkit. Uses Configuration, UserSecrets, and Serilog as well.

## Details

⚠ Warning: this sample is provided as is. There will likely be no (security) updates, and you should consider this example frozen in time.

This example is based off [the SendGrid (Twillio) blog](https://www.twilio.com/blog/send-email-in-csharp-dotnet-using-smtp-and-sendgrid) on the same subject.
It explains how to set up an example like this.
The main extra details it has is on setting up an account, which mostly speaks for itself.
One thing to note is that you need an API Key, which can be restricted to only:

- "Send Mails" custom / restricted access

At least for the SMTP sending of emails as with this example.

## Running

To use this sample:

1. Clone the repository;
2. Create UserSecrets for the csproj file, duplicate the "SendGrid" section in its entirety and use your own settings;
3. Run the application;

That's it!

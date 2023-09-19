# Sample SendGrid and MailKit Application

Demonstrates how to send SMTP e-mail with SendGrid and Mailkit. Uses Configuration, UserSecrets, and Serilog as well.

## Details

⚠ Warning: this sample is provided as is. There will likely be no (security) updates, and you should consider this example frozen in time.

## Steps to use

### SendGrid setup

This example is based off [the SendGrid (Twillio) blog](https://www.twilio.com/blog/send-email-in-csharp-dotnet-using-smtp-and-sendgrid) on the same subject.
The condensed version of all steps is:

1. Create a Sendgrid account
1. Complete onboarding (a "Single Sender Domain" account is good enough)
1. Create an API key with permissions: "Restricted Access" and at least "Mail Send" Full Access

### Running

To use this sample:

1. Clone the repository;
1. Create UserSecrets for the csproj file (or change the `appsettings.json` but _be sure not to commit those changes!!_), duplicate the "SendGrid" section in its entirety and use your own settings;
1. Run the application;

That's it!

using Twilio;
using Twilio.Rest.Api.V2010.Account;
using System;
using System.Threading.Tasks;
using stockManagement.Application.Interfaces;
using stockManagementClean.API.Utilities;

public class TwilioSmsService : ISmsService
{
    public async Task SendSmsAsync(string phoneNumber, string message)
    {
        try
        {
            TwilioClient.Init(TwilioConfigurations.AccountSid, TwilioConfigurations.AuthToken);

            var result = await MessageResource.CreateAsync(
                body: message,
                from: new Twilio.Types.PhoneNumber(TwilioConfigurations.FromNumber),
                to: new Twilio.Types.PhoneNumber(phoneNumber)
            );

            Console.WriteLine($"🔹 SMS Sent Successfully: {result.Sid}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ SMS Sending Failed: {ex.Message}");
        }
    }
}

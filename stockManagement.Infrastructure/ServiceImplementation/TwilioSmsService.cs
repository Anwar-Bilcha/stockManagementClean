using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using stockManagement.Application.Interfaces;
using stockManagementClean.API.Utilities;
namespace stockManagement.Infrastructure.ServiceImplementation
{
    

    public class TwilioSmsService : ISmsService
    {
        private readonly IConfiguration _configuration;

        public TwilioSmsService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendSmsAsync(string phoneNumber, string message)
        {
            string accountSid = TwilioConfigurations.AccountSid;
            var authToken = TwilioConfigurations.AuthToken;
            var fromPhoneNumber = TwilioConfigurations.FromNumber;

            TwilioClient.Init(accountSid, authToken);

            await MessageResource.CreateAsync(
                body: message,
                from: new Twilio.Types.PhoneNumber(fromPhoneNumber),
                to: new Twilio.Types.PhoneNumber(TwilioConfigurations.OwnersNumber)
            );
        }
    }
}

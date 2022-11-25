using CityInfo.SERVICE.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityInfo.SERVICE.Services
{
    public class CloudMailServices: IMailServices
    {
        private readonly string _mailFrom = string.Empty;
        private readonly string _mailTo = string.Empty; 
        public CloudMailServices(IConfiguration configuration)
        {

            _mailFrom = configuration["SendMailConfig:MailFrom"];
            _mailTo = configuration["SendMailConfig:MailTo"];
        }
        public void Send(string subject, string msg)
        {
            // sed  mail simulation into console
            Console.WriteLine($"Mail from {_mailFrom} to {_mailTo}  With {nameof(CloudMailServices)}.\n  Subject: {subject}. \n Message: {msg}");
        }
    }
}

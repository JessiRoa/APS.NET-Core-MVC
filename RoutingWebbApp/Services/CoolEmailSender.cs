using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoutingWebbApp.Services
{
    public class CoolEmailSender
    {
        public class CoolEmailSeeder
        {
            private ILogger<CoolEmailSeeder> logger;
            public CoolEmailSeeder(ILogger<CoolEmailSeeder> _logger)
            {
                logger = _logger;
            }
            public Task SendEmailAsync(string email, string subject, string message)
            {
                return Task.CompletedTask;
            }
        }
    }
}

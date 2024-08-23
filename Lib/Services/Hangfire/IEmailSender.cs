using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WdS.ElioPlus.Lib.Services.Hangfire
{
    public interface IEmailSender
    {
        System.Threading.Tasks.Task SendEmailAsync(string companyName, string email, string phone, string subject, string message, string lang);
    }
}

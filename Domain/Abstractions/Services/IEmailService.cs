using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories.Services;

public interface IEmailService
{
    Task SendEmailAsync(string recipient, string subject, string body);
    Task SendEmailAsync(List<string> recipients, string subject, string body);
}

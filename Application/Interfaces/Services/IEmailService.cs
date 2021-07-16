using System;
using System.Threading.Tasks;
using Application.DTOs.Email;

namespace Application.Interfaces.Services
{
    public interface IEmailService : IDisposable
    {
        Task SendAsync(EmailRequest request);
    }
}

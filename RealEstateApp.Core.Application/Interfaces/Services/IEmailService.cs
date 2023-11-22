
using RealEstateApp.Core.Application.Dtos.Email;
using RealEstateApp.Core.Domain.Settings;

namespace RealEstateApp.Core.Application.Interfaces.Services
{
    public interface IEmailService
    {
        public EmailSettings EmailSettings { get; }
        Task SendAsync(EmailRequest emailRequest);
    }
}

using System;
using System.Threading.Tasks;
using Application.Common.Wrappers;
using Application.DTOs.Account;

namespace Application.Interfaces.Services
{
    public interface IAccountService : IDisposable
    {
        Task<Response<LoginResponse>> LoginAsync(LoginRequest request, string ipAddress);
        Task<Response<string>> RegisterAsync(RegisterRequest request, string origin);
        Task<Response<string>> ConfirmEmailAsync(string userId, string code);
        Task ForgotPassword(ForgotPasswordRequest model, string origin);
        Task<Response<string>> ResetPassword(ResetPasswordRequest model);
        Task<Response<string>> ChangePassword(ChangePasswordRequest model);
    }
}

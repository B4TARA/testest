using Bank.Domain.Response;
using Bank.Domain.ViewModels.Account;
using System.Security.Claims;

namespace Bank.Service.Interfaces
{
    public interface IAccountService
    {
        Task<BaseResponse<ClaimsIdentity>> Login(LoginViewModel model);

        Task<BaseResponse<bool>> ChangePassword(ChangePasswordViewModel model);
    }
}

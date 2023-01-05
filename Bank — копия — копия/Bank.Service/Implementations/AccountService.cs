using Bank.DAL.Interfaces;
using Bank.Domain.Enum;
using Bank.Domain.Helpers;
using Bank.Domain.Models;
using Bank.Domain.Response;
using Bank.Domain.ViewModels.Account;
using Bank.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace Bank.Service.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly IBaseRepository<UserInfo> _userInfoRepository;
        private readonly ILogger<AccountService> _logger;

        public AccountService(IBaseRepository<UserInfo> userInfoRepository,
            ILogger<AccountService> logger)
        {
            _userInfoRepository = userInfoRepository;
            _logger = logger;
        }

        public async Task<BaseResponse<ClaimsIdentity>> Login(LoginViewModel model)
        {
            try
            {
                var userInfo = await _userInfoRepository.GetAll().FirstOrDefaultAsync(x => x.login == model.Login);
                if (userInfo == null)
                {
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Description = "Пользователь не найден"
                    };
                }
                //userInfo.rememberMe = model.RememberMe;
                if (userInfo.pass != //HashPasswordHelper.HashPassowrd
                                 (model.Password))
                {
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Description = "Неверный пароль или логин"
                    };
                }
                var result = Authenticate(userInfo);

                return new BaseResponse<ClaimsIdentity>()
                {
                    Data = result,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[Login]: {ex.Message}");
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<bool>> ChangePassword(ChangePasswordViewModel model)
        {
            try
            {
                var user = await _userInfoRepository.GetAll().FirstOrDefaultAsync(x => x.login == model.Login);
                if (user == null)
                {
                    return new BaseResponse<bool>()
                    {
                        StatusCode = StatusCode.UserNotFound,
                        Description = "Пользователь не найден"
                    };
                }

                user.pass = //HashPasswordHelper.HashPassowrd(
                            model.NewPassword;
                            //);
                await _userInfoRepository.Update(user);

                return new BaseResponse<bool>()
                {
                    Data = true,
                    StatusCode = StatusCode.OK,
                    Description = "Пароль обновлен"
                };

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[ChangePassword]: {ex.Message}");
                return new BaseResponse<bool>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        private ClaimsIdentity Authenticate(UserInfo userInfo)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userInfo.login),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, userInfo.user_role.ToString()),
            };
            return new ClaimsIdentity(claims, "ApplicationCookie",
                ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
        }
    }
}

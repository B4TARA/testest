using Bank.DAL.Interfaces;
using Bank.Domain.Enum;
using Bank.Domain.Helpers;
using Bank.Domain.Models;
using Bank.Domain.Response;
using Bank.Domain.ViewModels.Account;
using Bank.Domain.ViewModels.UserInfo;
using Bank.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace Bank.Service.Implementations
{
    public class UserInfoService : IUserInfoService
    {
        private readonly IBaseRepository<UserInfo> _userInfoRepository;
        private readonly ILogger<UserInfoService> _logger;
        public UserInfoService(ILogger<UserInfoService> logger, IBaseRepository<UserInfo> userInfoRepository)
        {
            _logger = logger;
            _userInfoRepository = userInfoRepository;
        }

        public async Task<BaseResponse<IEnumerable<UserInfo>>> GetAllUserInfo()
        {
            try
            {
                var allUserInfo = await _userInfoRepository.GetAll().ToListAsync();

                _logger.LogInformation($"[UserService.GetUsers] получено элементов {allUserInfo.Count}");
                return new BaseResponse<IEnumerable<UserInfo>>()
                {
                    Data = allUserInfo,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[UserSerivce.GetUsers] error: {ex.Message}");
                return new BaseResponse<IEnumerable<UserInfo>>()
                {
                    Description = $"[GetAllUserInfo] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
        public async Task<BaseResponse<UserInfo>> GetUserInfo(string login)
        {
            try
            {
                var userInfo = await _userInfoRepository.GetAll().FirstOrDefaultAsync(x => x.login == login);
                if (userInfo == null)
                {
                    return new BaseResponse<UserInfo>()
                    {
                        Description = "User not found",
                        StatusCode = StatusCode.UserNotFound
                    };
                }
                var data = new UserInfoViewModel()
                {
                    service_number = userInfo.service_number,
                    fullname = userInfo.fullname,
                    position_name = userInfo.position_name,
                    position_date = userInfo.position_date,
                    hire_date = userInfo.hire_date,
                    dismiss_date = userInfo.dismiss_date,
                    department = userInfo.department,
                    division_name = userInfo.division_name,
                    sector_name = userInfo.sector_name,
                    status = userInfo.status,
                    workday_balance = userInfo.workday_balance,
                    list_number = userInfo.list_number,
                    start_date = userInfo.start_date,
                    pass = userInfo.pass,
                    end_date = userInfo.end_date,
                    user_role = userInfo.user_role,
                    login = userInfo.login
                };
                return new BaseResponse<UserInfo>()
                {
                    StatusCode = StatusCode.OK,
                    Data = userInfo
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<UserInfo>()
                {
                    Description = $"[GetUserInfo] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
        public async Task<IBaseResponse<bool>> DeleteUserInfo(int service_number)
        {
            try
            {
                var userInfo = await _userInfoRepository.GetAll().FirstOrDefaultAsync(x => x.service_number == service_number);
                if (userInfo == null)
                {
                    return new BaseResponse<bool>()
                    {
                        StatusCode = StatusCode.UserNotFound,
                        Data = false
                    };
                }
                await _userInfoRepository.Delete(userInfo);
                _logger.LogInformation($"[UserService.DeleteUser] пользователь удален");

                return new BaseResponse<bool>()
                {
                    Data = true,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[UserSerivce.DeleteUser] error: {ex.Message}");
                return new BaseResponse<bool>()
                {
                    Description = $"[DeleteUserInfo] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
        public async Task<IBaseResponse<UserInfo>> CreateUserInfo(UserInfoViewModel model)
        {
            try
            {
                var user = await _userInfoRepository.GetAll().FirstOrDefaultAsync(x => x.service_number == model.service_number);
                if (user != null)
                {
                    return new BaseResponse<UserInfo>()
                    {
                        Description = "Пользователь с таким service_number уже есть",
                        StatusCode = StatusCode.UserAlreadyExists
                    };
                }
                var userInfo = new UserInfo()
                {
                    service_number = model.service_number,
                    fullname = model.fullname,
                    position_name = model.position_name,
                    position_date = model.position_date,
                    hire_date = model.hire_date,
                    dismiss_date = model.dismiss_date,
                    department = model.department,
                    division_name = model.division_name,
                    sector_name = model.sector_name,
                    status = model.status,
                    workday_balance = model.workday_balance,
                    list_number = model.list_number,
                    start_date = model.start_date,
                    pass = model.pass,
                    end_date = model.end_date,
                    user_role = model.user_role,
                    login = model.login
                };

                await _userInfoRepository.Create(user);
                _logger.LogInformation($"[UserService.CreateUser] пользователь добавлен");

                return new BaseResponse<UserInfo>()
                {
                    Data = user,
                    Description = "Пользователь добавлен",
                    StatusCode = StatusCode.OK
                };
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, $"[UserService.Create] error: {ex.Message}");
                return new BaseResponse<UserInfo>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"Внутренняя ошибка: {ex.Message}"
                };
            }
        }
        public async Task<IBaseResponse<UserInfo>> EditUserInfo(int service_number, UserInfoViewModel model)
        {
            try
            {
                var userInfo = await _userInfoRepository.GetAll().FirstOrDefaultAsync(x => x.service_number == service_number);
                if (userInfo == null)
                {
                    _logger.LogInformation($"[UserService.EditUser] пользователь не найден");
                    return new BaseResponse<UserInfo>()
                    {
                        Description = "UserInfo not found",
                        StatusCode = StatusCode.UserNotFound
                    };
                }

                userInfo.service_number = model.service_number;
                userInfo.fullname = model.fullname;
                userInfo.position_name = model.position_name;
                userInfo.position_date = model.position_date;
                userInfo.hire_date = model.hire_date;
                userInfo.dismiss_date = model.dismiss_date;
                userInfo.department = model.department;
                userInfo.division_name = model.division_name;
                userInfo.sector_name = model.sector_name;
                userInfo.status = model.status;
                userInfo.workday_balance = model.workday_balance;
                userInfo.list_number = model.list_number;
                userInfo.start_date = model.start_date;
                userInfo.pass = model.pass;
                userInfo.end_date = model.end_date;
                userInfo.user_role = model.user_role;
                userInfo.login = model.login;

                await _userInfoRepository.Update(userInfo);
                _logger.LogInformation($"[UserService.EditUser] пользователь изменен");
                return new BaseResponse<UserInfo>()
                {
                    Data = userInfo,
                    StatusCode = StatusCode.OK,
                };
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"[UserService.EditUser] error: {ex.Message}");
                return new BaseResponse<UserInfo>()
                {
                    Description = $"[EditUserInfo] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
        public async Task<IBaseResponse<bool>> ImportUserInfo(UserInfoViewModel model) //not done with logger
        {
            try
            {
                var userInfo = await _userInfoRepository.GetAll().FirstOrDefaultAsync(x => x.service_number == model.service_number);
                if (userInfo != null)
                {
                    await EditUserInfo(model.service_number, model);
                }
                else
                {
                    await CreateUserInfo(model);
                }
                return new BaseResponse<bool>()
                {
                    StatusCode = StatusCode.OK,
                    Data = true
                };

            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[ImportUser] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

    }
}

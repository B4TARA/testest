using Bank.DAL.Interfaces;
using Bank.Domain.Enum;
using Bank.Domain.Models;
using Bank.Domain.Response;
using Bank.Domain.ViewModels.UserInfo;
using Bank.Models;
using Bank.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Bank.Service.Implementations
{
    public class UserInfoService : IUserInfoService
    {
        private readonly IBaseRepository<UserInfo> _userInfoRepository;
        public UserInfoService(IBaseRepository<UserInfo> userInfoRepository)
        {
            _userInfoRepository = userInfoRepository;
        }

        public async Task<IBaseResponse<UserInfo>> GetUserInfo(int id)
        {
            try
            {
                var userInfo = await _userInfoRepository.GetAll().FirstOrDefaultAsync(x=>x.service_number == id);
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

        public async Task<IBaseResponse<bool>> DeleteUserInfo(int id)
        {
            try
            {
                var userInfo = await _userInfoRepository.GetAll().FirstOrDefaultAsync(x=>x.service_number == id);
                if (userInfo == null)
                {
                    return new BaseResponse<bool>()
                    {
                        Description = "User not found",
                        StatusCode = StatusCode.UserNotFound,
                        Data = false
                    };
                }
                await _userInfoRepository.Delete(userInfo);
                return new BaseResponse<bool>()
                {
                    Data = true,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
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
                await _userInfoRepository.Create(userInfo);
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
                    Description = $"[Create] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public IBaseResponse<List<UserInfo>> GetAllUserInfo()
        {
            try
            {
                var allUserInfo = _userInfoRepository.GetAll().ToList();
                if(!allUserInfo.Any())
                {
                    return new BaseResponse<List<UserInfo>>()
                    {
                        Description = "Найдено 0 элементов",
                        StatusCode = StatusCode.OK
                    };
                }
                return new BaseResponse<List<UserInfo>>()
                {
                    Data = allUserInfo,
                    StatusCode = StatusCode.OK
                };
            }
            catch(Exception ex)
            {
                return new BaseResponse<List<UserInfo>>()
                {
                    Description = $"[GetAllUserInfo] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<UserInfo>> EditUserInfo(int id, UserInfoViewModel model)
        {
            try
            {
                var userInfo = await _userInfoRepository.GetAll().FirstOrDefaultAsync(x=>x.service_number == id);
                if(userInfo == null)
                {
                    return new BaseResponse<UserInfo>()
                    {
                        Description = "Car not found",
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
                return new BaseResponse<UserInfo>()
                {
                    Data = userInfo,
                    StatusCode = StatusCode.OK,
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<UserInfo>()
                {
                    Description = $"[EditUserInfo] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}

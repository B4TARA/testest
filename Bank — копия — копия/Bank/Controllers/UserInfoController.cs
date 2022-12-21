using Bank.DAL.Interfaces;
using Bank.Domain.Models;
using Bank.Domain.ViewModels.UserInfo;
using Bank.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Controllers
{
    public class UserInfoController : Controller
    {
        private readonly IUserInfoService _userInfoService;

        public UserInfoController(IUserInfoService userInfoService)
        {
            _userInfoService = userInfoService;
        }

        public IActionResult GetAllUserInfo()
        {
            var response = _userInfoService.GetAllUserInfo();
            if(response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }
            return RedirectToAction("Error");
        }

        public async Task<IActionResult> GetUserInfo(int id)
        {
            var response = await _userInfoService.GetUserInfo(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }
            return RedirectToAction("Error");
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUserInfo(int id)
        {
            var response = await _userInfoService.DeleteUserInfo(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }
            return RedirectToAction("Error");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> CreateUserInfo(int id)
        {
           if(id == 0)
            {
                return View();
            }
           var response = await _userInfoService.GetUserInfo(id);
            if(response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }
            return RedirectToAction("GetAlluserInfo");
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserInfo(UserInfoViewModel model)
        {
            if(ModelState.IsValid)
            {
                if(model.service_number == 0)
                {
                    await _userInfoService.CreateUserInfo(model);
                }
                else
                {
                    await _userInfoService.EditUserInfo(model.service_number, model);
                }
                return View();
            }
            return RedirectToAction("Error");
        }
    }
}

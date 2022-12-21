using Bank.DAL.Interfaces;
using Bank.Domain.Models;
using Bank.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Bank.DAL.Repositories
{
    public class UserInfoRepository: IBaseRepository<UserInfo>
    {
        private readonly ApplicationDbContext _db;
        public UserInfoRepository(ApplicationDbContext db)
        {
            _db = db;
        }


        public async Task Create(UserInfo entity)
        {
            await _db.UserInfo.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public IQueryable<UserInfo> GetAll()
        {
            return _db.UserInfo;
        }
        public async Task Delete(UserInfo entity)
        {
            _db.UserInfo.Remove(entity);
            await _db.SaveChangesAsync();            ////ИЗМЕНЕНИЯ СОХРАНЯЕТ
        }
        public async Task<UserInfo> Update(UserInfo entity)
        {
            _db.UserInfo.Update(entity);
            //_db.SaveChangesAsync(); //
            return entity;
        }
    }
}

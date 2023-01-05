using Bank.DAL.Interfaces;
using Bank.Domain.Models;

namespace Bank.DAL.Repositories
{
    public class UserInfoRepository : IBaseRepository<UserInfo>
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
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}

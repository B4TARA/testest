using Bank.DAL.Interfaces;
using Bank.Models;

namespace Bank.DAL.Repositories
{
    internal class UserStructureRepository : IBaseRepository<UserStructure>
    {
        public Task Create(UserStructure entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(UserStructure entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<UserStructure> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<UserStructure> Update(UserStructure entity)
        {
            throw new NotImplementedException();
        }
    }
}

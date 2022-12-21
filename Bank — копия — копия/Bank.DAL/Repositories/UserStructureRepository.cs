using Bank.DAL.Interfaces;
using Bank.Domain.Models;
using Bank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

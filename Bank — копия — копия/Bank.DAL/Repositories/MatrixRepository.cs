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
    internal class MatrixRepository : IBaseRepository<Matrix>
    {
        public Task Create(Matrix entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Matrix entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Matrix> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Matrix> Update(Matrix entity)
        {
            throw new NotImplementedException();
        }
    }
}

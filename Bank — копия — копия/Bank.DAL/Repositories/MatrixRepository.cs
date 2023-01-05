using Bank.DAL.Interfaces;
using Bank.Models;

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

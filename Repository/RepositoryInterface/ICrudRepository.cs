using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository
{
    public interface ICrudRepository<TEntity, T, TMessageModel>
    {
        IEnumerable<TEntity> GetAll();

        Task<TMessageModel> Update(T id, TEntity entity);

        Task<TMessageModel> Delete(T id);

        Task<TEntity> GetById(T id);
    }
}

namespace ForthAssignment.Core.Aplication.Core
{
	public interface IBaseRepository<TEntity> where TEntity : class
	{
		Task<bool> Exits(Func<TEntity, bool> filter);
		Task<List<TEntity>> GetAll();
		Task<TEntity> GetById(Guid id);
		Task<TEntity> Save(TEntity entity);
		Task<bool> Update(TEntity entity);
	//	Task<bool> Delete(TEntity entity);
	}
}

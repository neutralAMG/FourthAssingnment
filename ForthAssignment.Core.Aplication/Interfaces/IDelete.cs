

using ForthAssignment.Core.Aplication.Core;

namespace ForthAssignment.Core.Aplication.Interfaces
{
	public interface IDelete<TEntity> where TEntity : class
	{
		Task<bool> Delete(TEntity entity);
	}

	public interface IDeleteService<TEntity> where TEntity : class
	{
		Task<Result<TEntity>> Delete(Guid id);
	}
}

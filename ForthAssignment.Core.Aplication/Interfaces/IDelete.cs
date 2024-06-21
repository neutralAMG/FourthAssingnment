

namespace ForthAssignment.Core.Aplication.Interfaces
{
	public interface IDelete<TEntity> where TEntity : class
	{
		Task<bool> Delete(TEntity entity);
	}
}

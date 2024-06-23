
namespace ForthAssignment.Core.Aplication.Core
{
	public interface IBaseService<TSaveModel, TModel, TEntity> 
		where TSaveModel : class
		where TModel : class
		where TEntity : class
	{
		Task<Result<List<TModel>>> GetAll();
		Task<Result<TModel>> GetById(Guid id);
		Task<Result<TSaveModel>> Save(TSaveModel entity);
		Task<Result<TSaveModel>> Update(TSaveModel entity);
	}
}

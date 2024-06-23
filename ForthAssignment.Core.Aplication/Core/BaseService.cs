

using AutoMapper;

namespace ForthAssignment.Core.Aplication.Core
{
	public class BaseService<TSaveModel, TModel, TEntity> : IBaseService<TSaveModel, TModel, TEntity>
		where TSaveModel : class
		where TModel : class
		where TEntity : class
	{
		private readonly IBaseRepository<TEntity> _baseRepository;
		private readonly IMapper _mapper;

		public BaseService(IBaseRepository<TEntity> baseRepository, IMapper mapper)
        {
			_baseRepository = baseRepository;
			_mapper = mapper;
		}
        public virtual async Task<Result<List<TModel>>> GetAll()
		{
			Result<List<TModel>> result = new();
			try
			{
				List<TEntity> EntitiesGetted = await _baseRepository.GetAll();

				if (EntitiesGetted is null)
				{
					result.IsSuccess = false;	
					result.Message = "Error getting the entities";
					return result;
				}

				result.Data = _mapper.Map<List<TModel>>(EntitiesGetted);

				if (result.Data is null)
				{
					result.IsSuccess = false;
					result.Message = "Error getting the entities";
					return result;
				}

				result.Message = "Entities getted with succsess";
				return result;
			}
			catch
			{
				result.IsSuccess = false;
				result.Message = "Critical error getting the entities";
				return result;
				throw;
			}
		}

		public virtual async Task<Result<TModel>> GetById(Guid id)
		{
			Result<TModel> result = new();
			try
			{
				TEntity EntityGetted = await _baseRepository.GetById(id);

				if (EntityGetted is null)
				{
					result.IsSuccess = false;
					result.Message = "Error getting the entity";
					return result;
				}

				result.Data = _mapper.Map<TModel>(EntityGetted);

				if (result.Data is null)
				{
					result.IsSuccess = false;
					result.Message = "Error getting the entity";
					return result;
				}

				result.Message = "Entity getted with succsess";
				return result;
			}
			catch
			{
				result.IsSuccess = false;
				result.Message = "Critical error getting the entity";
				return result;
				throw;
			}
		}

		public virtual async Task<Result<TSaveModel>> Save(TSaveModel saveModel)
		{
			Result<TSaveModel> result = new();
			try
			{
				if (saveModel is null)
				{
					result.IsSuccess = false;
					result.Message = "Entity to be saved was null";
					return result;
				}
				TEntity EntityToBeSaved = _mapper.Map<TEntity>(saveModel);

				if (EntityToBeSaved is null)
				{
					result.IsSuccess = false;
					result.Message = "Error saving the entity";
					return result;
				}

				TEntity EntitySaved = await _baseRepository.Save(EntityToBeSaved);

				if (EntitySaved is null)
				{
					result.IsSuccess = false;
					result.Message = "Error saving the entity";
					return result;
				}

				result.Data = _mapper.Map<TSaveModel>(EntitySaved);

				if (result.Data is null)
				{
					result.IsSuccess = false;
					result.Message = "Error saving the entity";
					return result;
				}

				result.Message = "Entity saved with succsess";
				return result;
			}
			catch
			{
				result.IsSuccess = false;
				result.Message = "Critical error saving the entity";
				return result;
				throw;
			}
		}

		public async Task<Result<TSaveModel>> Update(TSaveModel updateModel)
		{
			Result<TSaveModel> result = new();
			try
			{
                if (updateModel is null)
                {
                    result.IsSuccess = false;
					result.Message = "Entitiy to be updated was null";
					return result;
                }
				TEntity EntityToBeUpdated = _mapper.Map<TEntity>(updateModel);

				if (EntityToBeUpdated is null) { 

				    result.IsSuccess = false;
					result.Message = "Error updating the entity";
					return result;
				}

				bool UpdatedSucces = await _baseRepository.Update(EntityToBeUpdated);

				if (!UpdatedSucces) {
					result.IsSuccess = false;
					result.Message = "Error Updating the entity";
					return result;
				}

				result.Message = "Entity Updated with success";
				return result;

            }
			catch
			{
				result.IsSuccess = false;
				result.Message = "Critical error updating the entity";
				return result;
				throw;
			}
		}
	}
}



using ForthAssignment.Core.Aplication.Core;
using ForthAssignment.Infraestructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace ForthAssignment.Infraestructure.Persistence.Core
{

	public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
	{
		private readonly ForthAssignmentContext _context;
		private readonly DbSet<TEntity> _entity;
		public BaseRepository(ForthAssignmentContext context)
        {
			_context = context;
			_entity = _context.Set<TEntity>();
		}

        public virtual async Task<bool> Exits(Func<TEntity, bool> filter)
		{
			return await Task.FromResult( _entity.Any(filter));
		}

		public virtual async Task<List<TEntity>> GetAll()
		{
			return await _entity.ToListAsync();
		}

		public virtual async Task<TEntity> GetById(Guid id)
		{
			return await _entity.FindAsync(id);
		}

		public virtual async Task<TEntity> Save(TEntity entity)
		{
			await _entity.AddAsync(entity);
			await _context.SaveChangesAsync();
			return entity;

		}

		public virtual async Task<bool> Update(TEntity entity)
		{
			try
			{
			    _entity.Attach(entity);
			    _context.Entry(entity).State = EntityState.Modified;
			    await _context.SaveChangesAsync();
				return true;
			}
			catch
			{
				return false;
			}

			
		}		
		
	
	}
}

using AutoMapper;
using G4S.DataAccess.Enums;
using G4S.Entities.Enums;
using G4S.Entities.Helpers;
using G4S.Entities.HistoryPocos;
using G4S.Entities.Pocos;
using G4S.Entities.SearchPocos;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace G4S.DataAccess.Repositories
{
    public class Repository<TEntity> : RepositoryBase<EntityContext>, IRepository<TEntity> where TEntity : EntityBase
    {
        private readonly IEntityFilter<TEntity> _entityFilter;

        public Repository(IEntityFilter<TEntity> entityFilter, IEntityContext context) : base(context)
        {
            _entityFilter = entityFilter;
        }


        #region Get
        public async Task<TEntity> GetAsync(int id, IEnumerable<string> includes = null, DeleteOption includeDeleted = DeleteOption.NotDeleted, bool NoTracking = false)
        {
            try
            {
                IQueryable<TEntity> query = GetDBSet();
                query = HandleDeleteOption(includeDeleted, query);
                query = SetIncludes(includes, query);
                query = query.Where(x => x.Id == id);
                if (NoTracking) query = query.AsNoTracking();
                return await query.SingleOrDefaultAsync();
            }
            catch (Exception)
            {
                //TODO
                return null;
            }
        }



        public async Task<IList<TEntity>> GetAllAsync(IEnumerable<string> includes = null, DeleteOption includeDeleted = DeleteOption.NotDeleted)
        {
            try
            {
                IQueryable<TEntity> query = GetDBSet();
                query = query.Where(x => x.SoftDelete == false);
                query = SetIncludes(includes, query);
                return await query.ToListAsync<TEntity>();
            }
            catch (Exception ex)
            {
                //TODO
                return null;
            }
        }
        #endregion



        #region Fields
        public IEnumerable<string> GetIncludeList()
        {
            Type entityType = typeof(TEntity);

            var objectContext = ((IObjectContextAdapter)Context).ObjectContext;
            var entityMetadata = objectContext.CreateObjectSet<TEntity>().EntitySet.ElementType;

            List<string> properties = new List<string>();
            foreach (var navigationProperty in entityMetadata.NavigationProperties)
            {
                properties.Add(navigationProperty.Name);
            }

            return properties;

        }
        #endregion



        #region Search
        public async Task<IList<TEntity>> Search(System.Linq.Expressions.Expression<Func<TEntity, bool>> searchExpression, IEnumerable<string> includes = null, DeleteOption includeDeleted = DeleteOption.NotDeleted)
        {
            try
            {
                IQueryable<TEntity> query = GetDBSet();
                query = HandleDeleteOption(includeDeleted, query);
                query = SetIncludes(includes, query);
                query = query.Where(searchExpression);
                return await query.ToListAsync<TEntity>();
            }
            catch (Exception ex)
            {
                //TODO
                return null;
            }
        }

        public async Task<bool> Any(System.Linq.Expressions.Expression<Func<TEntity, bool>> anyExpression, DeleteOption includeDeleted = DeleteOption.NotDeleted)
        {
            try
            {
                IQueryable<TEntity> query = GetDBSet();
                query = HandleDeleteOption(includeDeleted, query);
                return await query.AnyAsync(anyExpression);
            }
            catch (Exception ex)
            {
                //TODO
                return false;
            }
        }

        public async Task<TEntity> FirstOrDefault(System.Linq.Expressions.Expression<Func<TEntity, bool>> fodExpression, DeleteOption includeDeleted = DeleteOption.NotDeleted)
        {
            try
            {
                IQueryable<TEntity> query = GetDBSet();
                query = HandleDeleteOption(includeDeleted, query);
                return await query.FirstOrDefaultAsync(fodExpression);
            }
            catch (Exception ex)
            {
                //TODO
                return null;
            }
        }

        public async Task<IList<TEntity>> Search(SearchBase<TEntity> searchCriteria, DeleteOption includeDeleted = DeleteOption.NotDeleted, IEnumerable<string> includes = null)
        {
            try
            {
                IQueryable<TEntity> query = GetDBSet();
                if (includeDeleted != searchCriteria.Deleted) throw new ArgumentException("Deleted in model and parameter does not match");
                query = HandleDeleteOption(includeDeleted, query);
                query = SetIncludes(includes, query);
                query = await _entityFilter.FilterAsync(query, searchCriteria);
                query = _entityFilter.Order(query, searchCriteria);
                query = _entityFilter.DoPaging(query, searchCriteria);
                return await query.ToListAsync<TEntity>();
            }
            catch (Exception ex)
            {
                //TODO
                return null;
            }
        }

        public async Task<int> SearchCount(SearchBase<TEntity> searchCriteria, DeleteOption includeDeleted = DeleteOption.NotDeleted)
        {
            try
            {
                IQueryable<TEntity> query = GetDBSet();
                //if (includeDeleted != searchCriteria.Deleted) throw new ArgumentException("Deleted in model and parameter does not match");
                query = HandleDeleteOption(includeDeleted, query);
                query = await _entityFilter.FilterAsync(query, searchCriteria);
                return await query.CountAsync();
            }
            catch (Exception ex)
            {
                //TODO
                return 0;
            }
        }
        #endregion



        #region Create
        public TEntity Create(TEntity entity, string userName = null)
        {
            if (entity == null) throw new InvalidOperationException("Unable to add a null entity");

            //Initialise CreatedAtUtc && SoftDelete prop to false @ creation
            entity.CreatedAtUtc = System.DateTimeOffset.UtcNow;
            entity.SoftDelete = false;

            entity = GetDBSet().Add(entity);
            Log(Context, entity, RepositoryAction.Create, userName);
            return entity;

            //using (var context = new EntityContext())
            //{
            //    GetDBSet(context).Add(entity);
            //    Log(context, entity, RepositoryAction.Create, userName);
            //    await context.SaveChangesAsync();
            //    return entity;
            //}
        }
        #endregion



        #region Update
        public async Task<TEntity> UpdateAsync(TEntity entity, string userName = null)
        {

            var set = GetDBSet();
            var existingEntity = await set.FindAsync(entity.Id);
            var originalCreateDate = existingEntity.CreatedAtUtc;
            if (existingEntity == null) throw new Exception("NotFound"); //TODO: notfoundexception maken
            Context.Entry(existingEntity).CurrentValues.SetValues(entity);
            existingEntity.CreatedAtUtc = originalCreateDate;

            //var entityType = entity.GetType();
            //var elementType = ((IObjectContextAdapter)Context).ObjectContext.CreateObjectSet<TEntity>().EntitySet.ElementType;
            //foreach (var navProp in elementType.NavigationProperties.Select(np => entityType.GetProperty(np.Name)).ToList())
            //{
            //    navProp.SetValue(entity, null);
            //    //var value = navProp.GetValue(entity);
            //    //if (value != null) Context.Entry(value).State = EntityState.;
            //} 

            Log(Context, entity, RepositoryAction.Update, userName);
            return entity;
        }
        #endregion

        #region Delete
        public async Task DeleteAsync(int id, string UserName = null)
        {
            var existingEntity = await GetDBSet().FindAsync(id);
            if (existingEntity == null) throw new Exception("NotFound"); //TODO: notfoundexception maken
            await DeleteAsync(existingEntity, null);
        }

        public async Task<TEntity> DeleteAsync(TEntity entity, string UserName = null)
        {
            if (entity == null) throw new Exception("NotFound"); //TODO: notfoundexception maken
                                                                 //Patch Delete tot SoftDelete && intialize DeletedAtUtc
            entity.SoftDelete = true;
            entity.DeletedAtUtc = System.DateTimeOffset.UtcNow;
            await UpdateAsync(entity);
            return entity;
        }
        #endregion



        #region Restore
        public async Task<TEntity> RestoreAsync(int id, string UserName = null)
        {
            TEntity toRestoreEntity = await GetAsync(id, null, DeleteOption.OnlyDeleted);
            toRestoreEntity.DeletedAtUtc = null;
            toRestoreEntity.SoftDelete = false;
            await UpdateAsync(toRestoreEntity, null);
            return toRestoreEntity;
        }

        public async Task<TEntity> RetoreAsync(TEntity entity, string UserName = null)
        {
            int iD = entity.Id;
            await RestoreAsync(iD, null);
            return await GetAsync(iD, null);
        }
        #endregion



        #region Privates
        private static IQueryable<TEntity> HandleDeleteOption(DeleteOption includeDeleted, IQueryable<TEntity> query)
        {
            switch (includeDeleted)
            {
                case DeleteOption.NotDeleted:
                    query = query.Where(x => !x.SoftDelete);
                    break;
                case DeleteOption.OnlyDeleted:
                    query = query.Where(x => x.SoftDelete);
                    break;
                case DeleteOption.Both:
                default:
                    break;
            }

            return query;
        }

        private void Log(EntityContext context, TEntity entity, RepositoryAction action, string userName = "SYSTEM")
        {
            var hisoryType = GetHistoryType();
            if (hisoryType == null) return;
            var historyEntity = Mapper.Map(entity, typeof(TEntity), hisoryType);
            if (historyEntity is HistoryEntityBase)
            {
                var baseEntity = historyEntity as HistoryEntityBase;
                baseEntity.HistoryAction = action.ToString();
                baseEntity.HistoryDate = DateTime.Now;
                baseEntity.HistoryUserName = userName;
            }
            GetLogDBSet().Add(historyEntity);
        }


        private DbSet<TEntity> GetDBSet()
        {
            return Context.Set<TEntity>();
        }

        private DbSet GetLogDBSet()
        {
            return Context.Set(GetHistoryType());
        }

        private Type GetHistoryType()
        {
            var type = typeof(TEntity);
            var attributes = type.GetCustomAttributes(typeof(HistoryAttribute), false);
            if (attributes == null || !attributes.Any()) return null; //throw new HistoryException($"Entityt {type.Name} does not have a linked History entity, use the HistoryAttribute to link the 2 enities");
            object attr = attributes.FirstOrDefault();
            if (attr == null) throw new HistoryException("No attributes found on class");
            HistoryAttribute historyAttribute = (HistoryAttribute)attr;
            if (historyAttribute == null) throw new HistoryException("Found attributes but could not match it to a history attribute");
            return historyAttribute.HistoryType;
        }

        private static IQueryable<TEntity> SetIncludes(IEnumerable<string> includes, IQueryable<TEntity> query)
        {
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    if (string.IsNullOrWhiteSpace(include)) continue;
                    try
                    {
                        query = query.Include(include);
                    }
                    catch (Exception)
                    {
                        //TODO:: log
                        //TODO:: return in warning
                    }
                }
            }

            return query;
        }

        #endregion


    }
}

using AutoMapper;
using G4S.Entities.Pocos;
using G4S.Entities.SearchPocos;

namespace G4S.Models
{
    public class ModelBase<TEntity> where TEntity : EntityBase
    {
        public int Id { get; set; }
        public bool SoftDelete { get; set; }
        public System.DateTimeOffset CreatedAtUtc { get; set; }
    }

    public class PostModelBase<TEntity> where TEntity : EntityBase
    {
        public int Id { get; set; }
    }

    public class SearchModelBase<TEntity> where TEntity : EntityBase
    {
        public int? Id { get; set; }
        public int? CurrentPage { get; set; }
        public int? ItemsPerPage { get; set; }
        public string SortField { get; set; }
        public bool? SortDescending { get; set; }
        public bool IncludeDeleted { get; set; }

        public virtual SearchBase<TEntity> Map()
        {
            return Mapper.Map<SearchBase<TEntity>>(this);
        }
    }
}
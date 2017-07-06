using AutoMapper;
using G4S.Entities.Pocos;
using G4S.Entities.SearchPocos;

namespace G4S.Models
{
    public class CsvListItemModelBase<TEntity> : ModelBase<TEntity> where TEntity : EntityBase
    {
        public string CsvSynonyms { get; set; }
    }

    public class CsvListItemPostModelBase<TEntity> : PostModelBase<TEntity> where TEntity : EntityBase
    {
        public string CsvSynonyms { get; set; }
    }
}
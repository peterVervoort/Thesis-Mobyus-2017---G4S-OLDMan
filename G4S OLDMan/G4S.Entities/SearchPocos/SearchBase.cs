using G4S.Entities.Enums;
using G4S.Entities.Pocos;

namespace G4S.Entities.SearchPocos
{
    public class SearchBase<TEntity> where TEntity : EntityBase
    {
        public int? Id { get; set; }
        public int? CurrentPage { get; set; }
        public int? ItemsPerPage { get; set; }
        public string SortField { get; set; }
        public bool? SortDescending { get; set; }
        public DeleteOption? Deleted { get; set; } 
    }
}
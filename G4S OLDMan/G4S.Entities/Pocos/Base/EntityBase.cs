using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace G4S.Entities.Pocos
{
    public class EntityBase
    {
        [Key]
        [Column(Order = 0)]
        public int Id { get; set; }

        public bool SoftDelete { get; set; }

        public System.DateTimeOffset? DeletedAtUtc { get; set; }

        public System.DateTimeOffset CreatedAtUtc { get; set; }

        public EntityBase()
        {
            //get last Id of EntityBase add one //TODO handeling in G4S.DataAcces - Repositories - RepositoryLogBase

            //SoftDelete && CreatedAtUtc are initialized and handeled in G4S.DataAcces - Repositories - RepositoryLogBase
        }
    }
}

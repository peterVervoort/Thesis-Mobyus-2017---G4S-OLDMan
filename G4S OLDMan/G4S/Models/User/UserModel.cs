using G4S.Entities.Pocos;
using System.ComponentModel.DataAnnotations;

namespace G4S.Models
{
    public class UserModel: ModelBase<User>
    {
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int LanguageId { get; set; }
        public string Language { get; set; }
        public int RoleGroupId { get; set; }
        public string RoleGroup { get; set; }
    }
}
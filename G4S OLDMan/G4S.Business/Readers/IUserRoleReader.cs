using System.Threading.Tasks;

namespace G4S.Business.Readers
{
    public interface IUserRoleReader
    {
        Task<bool> DoesUserHaveRole(string roleName, string Email);
    }
}
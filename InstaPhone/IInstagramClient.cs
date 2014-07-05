using System.Threading.Tasks;

namespace InstaPhone
{
    public interface IInstagramClient
    {
        Task<bool> Login();
        Task Logout();
    }
}
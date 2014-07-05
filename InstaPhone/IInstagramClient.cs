using System;
using System.Threading.Tasks;

namespace InstaPhone
{
    public interface IInstagramClient
    {
        bool ParseAuthResult(Uri authResult);
        Task GetPopularPhotosAsync();
    }
}
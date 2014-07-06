using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using InstaPhone.Model;

namespace InstaPhone
{
    public interface IInstagramClient
    {
        bool ParseAuthResult(Uri authResult);
        Task<IEnumerable<PopularMedia>> GetPopularPhotosAsync(int count);

        Task<Stream> DownloadImage(Uri imageUri);
    }
}
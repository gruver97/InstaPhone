using System;

namespace InstaPhone
{
    public interface IInstagramClient
    {
        bool ParseAuthResult(Uri authResult);
    }
}
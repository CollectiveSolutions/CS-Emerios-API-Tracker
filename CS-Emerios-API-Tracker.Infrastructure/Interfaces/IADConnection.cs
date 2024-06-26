using CS_Emerios_API_Tracker.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_Emerios_API_Tracker.Infrastructure.Interfaces
{
    public interface IADConnection
    {
        Task<bool> CheckStatus();
        Task<bool> CheckUserExist(string username);
        Task<bool> CheckUserActive(string username);
        Task<bool> CheckUserCredential(string username, string password);
        Task<UserActiveDirectoryDTO> GetUserInfo(string username);
        string Encrypt(string clearText, string EncryptionKey);
    }
}

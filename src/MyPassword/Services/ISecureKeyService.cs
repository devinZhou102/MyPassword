using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyPassword.Services
{
    public interface ISecureKeyService
    {
        string SecureKey { get;}

        Task LoadSecureKeyAsync();

        Task<bool> SaveAsync(string value);
    }
}

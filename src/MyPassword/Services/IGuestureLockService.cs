using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyPassword.Services
{
    public interface IGuestureLockService
    {
        string GuestureLock { get; }

        bool IsLockValid(List<int> indexList);

        string GeneratePassword(List<int> indexList);

        Task<bool> SaveAsync(string value);
    }
}

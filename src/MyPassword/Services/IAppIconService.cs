using MyPassword.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyPassword.Services
{
    public interface IAppIconService
    {
        List<FontIcon> FontIcons { get; }

        Task<bool> LoadAssets();

    }
}

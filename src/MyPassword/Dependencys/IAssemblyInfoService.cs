using System;
using System.Collections.Generic;
using System.Text;

namespace MyPassword.Dependencys
{
    public interface IAssemblyInfoService
    {
        string GetProductVersion();
        string GetCodeVersion();
    }
}

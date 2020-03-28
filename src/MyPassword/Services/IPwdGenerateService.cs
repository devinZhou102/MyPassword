using MyPassword.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyPassword.Services
{
    public interface IPwdGenerateService
    {
        string Generate(PwdGeneratorParams param);
    }
}

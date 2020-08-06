using MyPassword.Dependencys;
using MyPassword.iOS.Helpers;
using Xamarin.Forms;

[assembly: Dependency(typeof(MyPassword.iOS.Dependencys.ImpiOSAssemblyInfoService))]
namespace MyPassword.iOS.Dependencys
{
    class ImpiOSAssemblyInfoService:IAssemblyInfoService
    {
        public string GetCodeVersion()
        {
            return AssemblyHelper.CodeVersion;
        }

        public string GetProductVersion()
        {
            return AssemblyHelper.ProductVersion;
        }
    }
}
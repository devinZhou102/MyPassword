using MyPassword.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace MyPassword.Pages
{
	public class MyNavigationPage : NavigationPage
	{
		public MyNavigationPage(Page page):base(page)
		{
            this.Popped += MyNavigationPagePage_Popped;
		}
        

        private void MyNavigationPagePage_Popped(object sender, NavigationEventArgs e)
        {

            if(e.Page is BaseContentPage)
            {
                (e.Page as IPoppedOut).OnPoppedOut();
            }
        }

        
    }
}
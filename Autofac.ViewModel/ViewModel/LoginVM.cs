using Autofac.IViewModel;
using MyAutofac.IViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAutofac.ViewModel.ViewModel
{
    public class LoginVM : ILoginVM
    {
        

        public bool Login(string name, string passwd)
        {
            Console.WriteLine($"{name}登录,密码{passwd}");
            return true;
        }
    }
}

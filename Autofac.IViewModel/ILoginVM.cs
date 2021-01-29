using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAutofac.IViewModel
{
    public interface ILoginVM
    {
        bool Login(string name,string passwd);
    }
}

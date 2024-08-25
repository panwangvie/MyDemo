using MyAutofac.IModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAutofac.Model
{
    public class LoginUser: ILoginUser
    {
        public string UserName { get; set; }

        public string Passwd { get; set; }
    }
}

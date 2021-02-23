using System;
using System.Collections.Generic;
using System.Text;
using WPF.IOC.NetCore.Interfaces;

namespace WPF.IOC.NetCore.Services
{
    public class ScopedText : IScopedText
    {
        public string GetText()
        {
            return "asp.net core ScopedText";
        }
    }
}

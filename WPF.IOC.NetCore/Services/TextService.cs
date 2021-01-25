using System;
using System.Collections.Generic;
using System.Text;
using WPF.IOC.NetCore.Interfaces;

namespace WPF.IOC.NetCore.Services
{
    public class TextService : ITextService
    {
        private string _text;
        public  TextService(string text)
        {
            _text = text;

        }

        public string GetText()
        {
            return _text;

        }
    }
}

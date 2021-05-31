
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BcdeditDemo
{
    /// <summary>
    ///  
    /// </summary>
    public class BcdElement
    {
        public string Id { get; set; }
        public string Device { get; set; }
        public string Path { get; set; }
        public string Description { get; set; }
        public string Locale { get; set; }
        public string Inherit { get; set; }
        public string Default { get; set; }
        public string Resumeobject { get; set; }
        public string Displayorder { get; set; }
        public string Toolsdisplayorder { get; set; }
        public string Timeout { get; set; }

    }
}

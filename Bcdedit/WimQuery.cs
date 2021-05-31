
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using MessageBox = System.Windows.MessageBox;

namespace Bcdedit
{
    /// <summary>
    ///  
    /// </summary>
    public class WMIQuery
    {
        public static void Query()
        {
            try
            {
                ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher("root\\cimv2",
                        "SELECT * FROM Win32_BootConfiguration");

                var searchers = searcher.Get();

                foreach (ManagementObject queryObj in searcher.Get())
                {
                    Console.WriteLine("-----------------------------------");
                    Console.WriteLine("Win32_BootConfiguration instance");
                    Console.WriteLine("-----------------------------------");
                    Console.WriteLine("Description: {0}", queryObj["Description"]);
                }
            }
            catch (ManagementException e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }


}

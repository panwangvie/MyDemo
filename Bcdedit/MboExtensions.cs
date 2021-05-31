
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace BcdeditDemo
{
    /// <summary>
    ///  
    /// </summary>
    public static class MboExtensions
    {
        public static List<PropertyData> GetNameValue(this ManagementBaseObject mbo)
        {
            List<PropertyData> propertyDatas = new List<PropertyData>();

            foreach (var aa in mbo.Properties)
            {
                propertyDatas.Add(aa);
            }
            return propertyDatas;
        }
    }
}

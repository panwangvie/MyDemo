
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using BcdeditDemo;

namespace Bcdedit
{
    
    /// <summary>
    ///  
    /// </summary>
    public class BCDWMI
    {
        public static readonly UInt32 BCDE_STANDARD_OS_ENTRY = 0x10200003;
        public static readonly UInt32 BCDE_LEGACY_OS_ENTRY = 0x10300006;
        public static readonly UInt32 BcdLibraryElementTypeString_Description = 0x12000004;


        public static Dictionary<string, string> EnumerateObjectsByType(uint bcdType, string storePath)
        {

            Dictionary<string, string> dictEntries = null;

            ConnectionOptions options = new ConnectionOptions();
            options.Impersonation = ImpersonationLevel.Impersonate;
            options.EnablePrivileges = true;
            ManagementScope MgmtScope = new ManagementScope("root\\WMI", options);
            ManagementPath MgmtPath = new ManagementPath("root\\WMI:BcdStore.FilePath='" + storePath + "'");
            ManagementObject bcdStore = new System.Management.ManagementObject(MgmtScope, MgmtPath, null);
            ManagementBaseObject[] mboArray;

            bool success = EnumerateObjects(bcdStore, bcdType, out mboArray);
            if (success)
            {
                dictEntries = new Dictionary<string, string>();

                foreach (ManagementBaseObject mbo in mboArray)
                {
                    ManagementPath BcdObjectPath = new ManagementPath("root\\WMI:BcdObject.Id=\"" + mbo.GetPropertyValue("Id") + "\",StoreFilePath='" + storePath + "'");
                    foreach (var VARIABLE in mbo.Properties)
                    {
                        
                    }
                    ManagementObject BcdObject = new ManagementObject(MgmtScope, BcdObjectPath, null);
                    ManagementBaseObject Element;
                    String Description = String.Empty;
                    try
                    {
                        bool getDescripStatus = GetElement(BcdObject, BcdLibraryElementTypeString_Description, out Element);
                        var  aa=Element.GetNameValue();

                        if (getDescripStatus)
                            Description = Element.GetPropertyValue("String").ToString();
                    }
                    catch (Exception)
                    {
                    }
                    dictEntries.Add((string)mbo.GetPropertyValue("Id"), String.Format("Type: {0:X8} {1}", mbo.GetPropertyValue("Type"), Description));
                   
                }
            }
            return dictEntries;
        }

        public static bool EnumerateObjects(ManagementObject bcdStore, uint Type, out System.Management.ManagementBaseObject[] Objects)
        {
            System.Management.ManagementBaseObject inParams = null;
            inParams = bcdStore.GetMethodParameters("EnumerateObjects");
            inParams["Type"] = ((uint)(Type));
            System.Management.ManagementBaseObject outParams = bcdStore.InvokeMethod("EnumerateObjects", inParams, null);
            Objects = ((System.Management.ManagementBaseObject[])(outParams.Properties["Objects"].Value));
            
            return System.Convert.ToBoolean(outParams.Properties["ReturnValue"].Value);
        }

        public static bool EnumerateElements(ManagementObject bcdStore, uint Type, out System.Management.ManagementBaseObject[] Objects)
        {
            System.Management.ManagementBaseObject inParams = null;
            inParams = bcdStore.GetMethodParameters("EnumerateElements");
            inParams["Type"] = ((uint)(Type));
            System.Management.ManagementBaseObject outParams = bcdStore.InvokeMethod("EnumerateElements", inParams, null);
            Objects = ((System.Management.ManagementBaseObject[])(outParams.Properties["Objects"].Value));
            return System.Convert.ToBoolean(outParams.Properties["ReturnValue"].Value);
        }

        public static bool GetElement(ManagementObject bdcObject, uint Type, out System.Management.ManagementBaseObject Element)
        {
            
            System.Management.ManagementBaseObject inParams = null;
            inParams = bdcObject.GetMethodParameters("GetElement");
            inParams["Type"] = ((uint)(Type));
            System.Management.ManagementBaseObject outParams = bdcObject.InvokeMethod("GetElement", inParams, null);
            Element = ((System.Management.ManagementBaseObject)(outParams.Properties["Element"].Value));



            return System.Convert.ToBoolean(outParams.Properties["ReturnValue"].Value);
        }

        
    }


    public class BcdStoreAccessor
    {

        public const int BcdOSLoaderInteger_SafeBoot = 0x25000080;

        public enum BcdLibrary_SafeBoot
        {
            SafemodeMinimal = 0,
            SafemodeNetwork = 1,
            SafemodeDsRepair = 2
        }

        private ConnectionOptions connectionOptions;
        private ManagementScope managementScope;
        private ManagementPath managementPath;

        public BcdStoreAccessor()
        {
            

            connectionOptions = new ConnectionOptions();
            connectionOptions.Impersonation = ImpersonationLevel.Impersonate;
            connectionOptions.EnablePrivileges = true;

            managementScope = new ManagementScope("root\\WMI", connectionOptions);

            managementPath = new ManagementPath("root\\WMI:BcdObject.Id=\"{fa926493-6f1c-4193-a414-58f0b2456d1e}\",StoreFilePath=\"\"");
        }

        public void SetSafeboot()
        {
            ManagementObject currentBootloader = new ManagementObject(managementScope, managementPath, null);
            currentBootloader.InvokeMethod("SetIntegerElement", new object[] { BcdOSLoaderInteger_SafeBoot, BcdLibrary_SafeBoot.SafemodeMinimal });
        }

        public void RemoveSafeboot()
        {
            ManagementObject currentBootloader = new ManagementObject(managementScope, managementPath, null);
            currentBootloader.InvokeMethod("DeleteElement", new object[] { BcdOSLoaderInteger_SafeBoot });
        }
    }


}

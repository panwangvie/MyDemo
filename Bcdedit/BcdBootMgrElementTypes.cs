
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bcdedit
{
    /// <summary>
    ///  
    /// </summary>
    enum BcdBootMgrElementTypes
    {
        BcdBootMgrObjectList_DisplayOrder = 0x24000001,
        BcdBootMgrObjectList_BootSequence = 0x24000002,
        BcdBootMgrObject_DefaultObject = 0x23000003,
        BcdBootMgrInteger_Timeout = 0x25000004,
        BcdBootMgrBoolean_AttemptResume = 0x26000005,
        BcdBootMgrObject_ResumeObject = 0x23000006,
        BcdBootMgrObjectList_ToolsDisplayOrder = 0x24000010,
        BcdBootMgrBoolean_DisplayBootMenu = 0x26000020,
        BcdBootMgrBoolean_NoErrorDisplay = 0x26000021,
        BcdBootMgrDevice_BcdDevice = 0x21000022,
        BcdBootMgrString_BcdFilePath = 0x22000023,
        BcdBootMgrBoolean_ProcessCustomActionsFirst = 0x26000028,
        BcdBootMgrIntegerList_CustomActionsList = 0x27000030,
        BcdBootMgrBoolean_PersistBootSequence = 0x26000031
    }
}

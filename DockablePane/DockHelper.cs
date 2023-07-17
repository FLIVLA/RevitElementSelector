using Autodesk.Revit.UI;
using RevitAddin.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAddin.DockablePane
{
    public static class DockHelper
    {
        public static readonly DockablePaneId Pane_Id = new DockablePaneId(new Guid("3490B5A8-9029-4FD0-92C2-EF88DAF714B8"));
        public static Addin_DockablePane dp { get; set; }

        public static ExternalEvent EVT_IsolateElements { get; set; }
        public static Evt_IsolateElements ExtEvt_Handler_Iso { get; set; }


        public static void Create_Dockable_Pane(UIControlledApplication uiapp)
        {
            dp = new Addin_DockablePane();
            uiapp.RegisterDockablePane(Pane_Id, "Addin DockablePane", dp as IDockablePaneProvider);

            // SetUp Event
            ExtEvt_Handler_Iso = new Evt_IsolateElements();
            EVT_IsolateElements = ExternalEvent.Create(ExtEvt_Handler_Iso);
        }

        public static void Isolate(List<int> ids)
        {
            // Pass ids as List<int> to ext_event
            ExtEvt_Handler_Iso.id_IN = ids;
            EVT_IsolateElements.Raise();
        }
    }
}

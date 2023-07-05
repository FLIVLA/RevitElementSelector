using Autodesk.Revit.UI;
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

        public static void Create_Dockable_Pane(UIControlledApplication uiapp)
        {
            dp = new Addin_DockablePane();
            uiapp.RegisterDockablePane(Pane_Id, "Addin DockablePane", dp as IDockablePaneProvider);
        }
    }
}

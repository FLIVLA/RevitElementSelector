using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAddin.ExternalCommands
{
    [Transaction(TransactionMode.Manual)]
    public class ShowDockablePane : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Autodesk.Revit.UI.DockablePane dp = commandData.Application.GetDockablePane(RevitAddin.DockablePane.DockHelper.Pane_Id);
            dp.Show();
            return Result.Succeeded;
        }
    }
}

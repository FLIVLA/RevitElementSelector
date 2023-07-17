using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RevitAddin.ExternalCommands
{
    [Transaction(TransactionMode.Manual)]
    public class ShowDockablePane : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Autodesk.Revit.UI.DockablePane dp = commandData.Application.GetDockablePane(RevitAddin.DockablePane.DockHelper.Pane_Id);
            dp.Show();
            //var col = new FilteredElementCollector(App.Document)
            //    .WhereElementIsNotElementType()
            //    .WhereElementIsViewIndependent()
            //    .Where(x => IsPhysicalElement(x))
            //    .Select(x => x).ToList();

            //string[] ids = col.Select(x => x.Id.IntegerValue.ToString()).ToArray();

            //using (StreamWriter w = new StreamWriter(@"C:\Users\frede\Desktop\test"))
            //{
            //    for (int i = 0; i < ids.Length; i++)
            //    {
            //        w.WriteLine(ids[i]);
            //    }
            //}

            return Result.Succeeded;
        }

        public static bool IsPhysicalElement(Element e)
        {
            if (e.Category == null) return false;
            if (e.ViewSpecific) return false;
            if (((BuiltInCategory)e.Category.Id.IntegerValue) == BuiltInCategory.OST_DetailComponents) return false;
            return e.Category.CategoryType == CategoryType.Model && e.Category.CanAddSubcategory;
        }
    }
}

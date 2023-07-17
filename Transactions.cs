using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace RevitAddin
{
    public class Transactions
    {
        public static bool IsPhysicalElement(Element e)
        {
            if (e.Category == null) return false;
            if (e.ViewSpecific) return false;
            if (((BuiltInCategory)e.Category.Id.IntegerValue) == BuiltInCategory.OST_DetailComponents) return false;
            return e.Category.CategoryType == CategoryType.Model && e.Category.CanAddSubcategory;
        }

        public static void Isolate(UIDocument uidoc,List<int> ids)
        {
            try
            {
                using (Transaction t = new Transaction(App.Document, "isolateStuff"))
                {
                    t.Start();

                    // Collect elements from the current model
                    var currentModelCollector = new FilteredElementCollector(App.Document)
                        .WhereElementIsNotElementType()
                        .WhereElementIsViewIndependent()
                        .Where(x => IsPhysicalElement(x))
                        .Where(x => ids.Contains(x.Id.IntegerValue))
                        .ToList();

                    // Create a temporary list to store the element IDs to be hidden
                    List<ElementId> elementsToHide = new List<ElementId>();

                    // Collect elements from linked models
                    var linkedModelCollector = new FilteredElementCollector(App.Document);
                    var linkModelIds = linkedModelCollector.OfClass(typeof(RevitLinkInstance)).ToElementIds();

                    foreach (var id in linkModelIds)
                    {
                        RevitLinkInstance linst = App.Document.GetElement(id) as RevitLinkInstance;
                        Document linkDoc = linst.GetLinkDocument();

                        var linkedElements = new FilteredElementCollector(linkDoc)
                            .WhereElementIsNotElementType()
                            .WhereElementIsViewIndependent()
                            .Where(x => IsPhysicalElement(x))
                            .Where(x => ids.Contains(x.Id.IntegerValue))
                            .ToList();

                        // Hide elements from linked models temporarily
                        foreach (var e in linkedElements)
                        {
                            var v = e.LookupParameter("Visibility");
                            if (v != null) { v.Set(0); }
                            else MessageBox.Show("param not found");
                        }
                    }             

                    t.Commit();
                }
            } catch(Exception ex) {
                MessageBox.Show($"ERROR! \n {ex.Message}");
            }
        }
    }
}

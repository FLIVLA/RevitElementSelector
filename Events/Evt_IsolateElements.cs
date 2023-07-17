using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RevitAddin.Events
{
    public class Evt_IsolateElements : IExternalEventHandler
    {
        public List<int> id_IN { get; set; }

        public void Execute(UIApplication app)
        {
            try
            {
                Transactions.Isolate(app.ActiveUIDocument, id_IN);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public string GetName()
        {
            return "handler_Isolate";
        }
    }
}

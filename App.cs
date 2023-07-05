using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using RevitAddin.DockablePane;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace RevitAddin
{
    public class App : IExternalApplication
    {
        public static UIControlledApplication Application { get; private set; }
        public static Document Document { get; set; }

        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }

        public Result OnStartup(UIControlledApplication application)
        {
            try
            {
                Application = application;
                DockHelper.Create_Dockable_Pane(Application);
                AddComponents("Basic-Revit-Addin", "Addin-Ribbon");
                return Result.Succeeded;
            }
            catch (Exception ex)
            {
                return Result.Failed;
            }
        }

        private void AddComponents(string tabName, string ribbonPanelName)
        {
            Application.CreateRibbonTab(tabName); // Add tab
            Application.CreateRibbonPanel(tabName, ribbonPanelName); // Add ribbonPanel

            // Sample Button (Set to "ShowDockablePane" command as default).
            AddButton(
                assembly: Assembly.GetExecutingAssembly(),
                entryCommand: "RevitAddin.ExternalCommands.ShowDockablePane",
                tabName: tabName,
                ribbonName: ribbonPanelName,
                buttonName: "Show Dock",
                iconPath: "iconShow.png",
                toolTip: "Sample Button. Show Dockable Pane");
        }

        private void AddButton(Assembly assembly, string tabName, string ribbonName, string buttonName, string entryCommand, string iconPath, string toolTip)
        {
            PushButtonData btnData = new PushButtonData(string.Format("btn_{0}", entryCommand), buttonName, assembly.Location, entryCommand);
            btnData.ToolTip = toolTip;
            Uri uri = new Uri(string.Format("pack://application:,,,/{0};component/Resources/{1}", assembly.GetName(), iconPath));
            BitmapImage bitmap = new BitmapImage(uri);
            PushButton btn = GetRibbonPanel(Application, tabName, ribbonName).AddItem(btnData) as PushButton;
            btn.LargeImage = bitmap;
        }

        private RibbonPanel GetRibbonPanel(UIControlledApplication uiapp, string tabName, string ribbonName)
        {
            var panel = uiapp.GetRibbonPanels(tabName).Where(p => p.Name == ribbonName).FirstOrDefault();
            return panel;
        }
    }
}

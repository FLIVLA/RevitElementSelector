using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Events;
using RevitAddin.UI_Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace RevitAddin.DockablePane
{
    public class Addin_DockablePane : IDockablePaneProvider, IFrameworkElementCreator
    {
        private DockPage Page = null;

        public FrameworkElement CreateFrameworkElement()
        {
            try
            {
                if (Page == null)
                    Page = new DockPage();
                return Page;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SetupDockablePane(DockablePaneProviderData data)
        {
            data.FrameworkElementCreator = this as IFrameworkElementCreator;
            data.InitialState = new DockablePaneState();
            data.InitialState.MinimumWidth = 400;
            data.VisibleByDefault = false;
            data.EditorInteraction = new EditorInteraction(EditorInteractionType.KeepAlive);
            data.InitialState.DockPosition = DockPosition.Right;
            data.InitialState.TabBehind = DockablePanes.BuiltInDockablePanes.ProjectBrowser;
        }
    }
}

using RevitAddin.DockablePane;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RevitAddin.UI_Components
{
    /// <summary>
    /// Interaction logic for DockPage.xaml
    /// </summary>
    public partial class DockPage : Page
    {
        public DockPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if (ID_BOX.Text != "")
            {
                var ids = ID_BOX.Text.Split(',').Select(x => int.Parse(x.Trim())).ToList();
                DockHelper.Isolate(ids);
            }
            else
            {
                return;
            }
        }
    }
}

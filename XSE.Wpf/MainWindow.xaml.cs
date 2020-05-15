using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace XSE.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public const string PathNotas = "notas.rtb";
        int idTab = 1;
        Timer timerGuardar;
        public MainWindow()
        {
            InitializeComponent();
            if (File.Exists(PathNotas))
                rtbNotas.TextWithFormat=File.ReadAllText(PathNotas);
            
            timerGuardar = new Timer();
            timerGuardar.Interval = 100;
            timerGuardar.Elapsed += (s, e) => SaveNotas();
            tabScripts.SelectionChanged += (s, e) => (tabScripts.SelectedContent as ZonaScript).Refresh();
            MenuItemNuevaPestañaClick();
            rtbNotas.TextChanged += rtbNotas_TextChanged;
        }

        void SaveNotas()
        {
            string notas = rtbNotas.TextWithFormat;
            if (string.IsNullOrEmpty(notas))
                File.Delete(PathNotas);
            else File.WriteAllText(PathNotas, notas);
            timerGuardar.Stop();
        }


        private void MenuItemNuevaPestañaClick(object sender=null, RoutedEventArgs e=null)
        {
            ContextMenu context = new ContextMenu();
            MenuItem itemClose = new MenuItem();
            TabItem pestaña = new TabItem();
            pestaña.Content = new ZonaScript();
            tabScripts.Items.Add(pestaña);
            pestaña.IsSelected = true;
            pestaña.Header = $"Script{idTab++}";
            context.Items.Add(itemClose);
            itemClose.Header = "Close "+pestaña.Header;
            itemClose.Tag = pestaña;
            itemClose.Click += (s, m) =>
            {
                MenuItem menuItem = ((MenuItem)s);
                ZonaScript zona = ((TabItem)menuItem.Tag).Content as ZonaScript;
                if(zona.CanClearContentWithOutProblems())
                   tabScripts.Items.Remove(menuItem.Tag);
            };
            pestaña.ContextMenu = context;

        }

        private void rtbNotas_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!timerGuardar.Enabled)
                timerGuardar.Start();
        }
    }
}

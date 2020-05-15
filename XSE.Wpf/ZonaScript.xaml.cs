using Gabriel.Cat.S.Extension;
using Gabriel.Cat.S.Utilitats;
using Microsoft.Win32;
using PokemonGBAFramework.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace XSE.Wpf
{
    /// <summary>
    /// Lógica de interacción para ZonaScript.xaml
    /// </summary>
    public partial class ZonaScript : UserControl
    {
        class RomItem:IComparable
        {
            public string Path { get; set; }

            public int CompareTo(object obj)
            {
                RomItem other = obj as RomItem;
                int compareTo = other != null ? 0 : -1;
               
                if(compareTo==0)
                   compareTo= Path.CompareTo(other.Path);
                return compareTo;
            }
            public override bool Equals(object obj)
            {
                return CompareTo(obj)==0;
            }

            public override string ToString()
            {
                return System.IO.Path.GetFileName(Path);
            }
        }

        static List<RomItem> lstRoms;
        static ZonaScript()
        {
            lstRoms = new List<RomItem>();
            lstRoms.Add(new RomItem() { Path = "" });
        }
        public ZonaScript()
        {
            InitializeComponent();

            cmbRomsCargadas.ItemsSource = lstRoms;
            cmbRomsCargadas.SelectionChanged += ChangeRom;
            
        }
        public RomGba Rom { get; set; }
        private void ChangeRom(object sender, SelectionChangedEventArgs e)
        {
            RomItem romItem;
            if (cmbRomsCargadas.SelectedIndex > 0)
            {
                romItem = ((RomItem)cmbRomsCargadas.SelectedItem);
                Rom = new RomGba(romItem.Path);

            }

            btnCheck.IsEnabled = Rom != default;
            btnCompilar.IsEnabled = Rom != default;
            btnGuardar.IsEnabled = Rom != default;
            btnReadScript.IsEnabled = Rom != default;
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAbrir_Click(object sender=null, RoutedEventArgs e=null)
        {
            RomItem romItem;

            OpenFileDialog opnFile = new OpenFileDialog();
            opnFile.Filter = "GBA|*.gba;*.bak";
            if ( opnFile.ShowDialog().GetValueOrDefault())
            {
                try
                {
                    romItem = new RomItem() { Path =  opnFile.FileName};
                    if (lstRoms.IndexOf(romItem)< 0)
                    {
                        lstRoms.Add(romItem);
                        Refresh();
                    }
                    cmbRomsCargadas.SelectedIndex = lstRoms.IndexOf(romItem);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ha habido un problema al cargar :" + ex.Message);
                }

            }
        }

        public void Refresh()
        {
            cmbRomsCargadas.SelectionChanged -= ChangeRom;
            cmbRomsCargadas.ItemsSource = lstRoms.ToArray();//no les gusta compartir...
            cmbRomsCargadas.SelectionChanged += ChangeRom;
        }

        private void btnCompilar_Click(object sender, RoutedEventArgs e)
        {
            IList<Script> scripts = Script.FromXSE(txtScript.Text.Split('\n'));
            txtOffsetHex.Text = (Hex)scripts.First().SetScript(Rom);
            LoadScript();
        }

        private void btnCheck_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnReadScript_Click(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrEmpty(txtOffsetHex.Text) || string.IsNullOrWhiteSpace(txtOffsetHex.Text))
            {
                MessageBox.Show("Se requiere un offset para leer el script");
            }
            else if(CanClearContentWithOutProblems())
            {
                 LoadScript();

            }
        }

        private Script LoadScript()
        {
            Script script = new Script(Rom, (int)(Hex)txtOffsetHex.Text);
            txtScript.Text = script.GetAllDeclaracionXSE(Rom, null);
            return script;
        }

        public bool ComprobarSiSeHaGuardadoElScript()
        {
            return true;
        }
        public bool CanClearContentWithOutProblems()
        {
            bool cancontinue = ComprobarSiSeHaGuardadoElScript();
            if (!cancontinue)
            {
                cancontinue = MessageBox.Show("¿Hay datos sin guardar, desas continuar igualmente?", "Atención", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.Yes;
            }
            return cancontinue;
        }
    }
    
}

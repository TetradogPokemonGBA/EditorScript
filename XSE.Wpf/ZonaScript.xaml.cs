using Gabriel.Cat.S.Utilitats;
using Microsoft.Win32;
using PokemonGBAFramework.Core;
using System;
using System.Collections.Generic;
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
        public ZonaScript()
        {
            InitializeComponent();
        }
        public RomGba Rom { get; set; }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAbrir_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog opnFile = new OpenFileDialog();
            opnFile.Filter = "GBA|*.gba";
            if (opnFile.ShowDialog().GetValueOrDefault())
            {
                try
                {
                    Rom = new RomGba(opnFile.FileName);
                    if(cmbRomsCargadas.Items.IndexOf(opnFile.FileName)<0)
                       cmbRomsCargadas.Items.Add(opnFile.FileName);
                    cmbRomsCargadas.SelectedIndex = cmbRomsCargadas.Items.IndexOf(opnFile.FileName);
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Ha habido un problema al cargar :"+ex.Message);
                }
            }
        }

        private void btnCompilar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnCheck_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnReadScript_Click(object sender, RoutedEventArgs e)
        {
            Script script;
            if (string.IsNullOrEmpty(txtOffsetHex.Text) || string.IsNullOrWhiteSpace(txtOffsetHex.Text))
            {
                MessageBox.Show("Se requiere un offset para leer el script");
            }
            else
            {
                ComprobarSiSeHaGuardadoElScript();
                //try
                //{
                    script= new Script(Rom, (int)(Hex)txtOffsetHex.Text);
                    txtScript.Text = script.GetAllDeclaracionXSE(Rom,null);
                //}
                //catch (Exception ex)
                //{
                //    MessageBox.Show(ex.Message);
                //}
            }
        }

        private void ComprobarSiSeHaGuardadoElScript()
        {

        }
    }
}

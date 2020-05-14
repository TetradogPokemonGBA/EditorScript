using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
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

        Timer timerGuardar;
        public MainWindow()
        {
            InitializeComponent();
            if (File.Exists(PathNotas))
                rtbNotas.AppendText(File.ReadAllText(PathNotas));
            timerGuardar = new Timer();
            timerGuardar.Interval = 500;
            timerGuardar.Elapsed += (s, e) => SaveNotas();
        }

        void SaveNotas()
        {
            string notas = rtbNotas.GetText();
            if (string.IsNullOrEmpty(notas))
                File.Delete(PathNotas);
            else File.WriteAllText(PathNotas, notas);
            timerGuardar.Stop();
        }

        private void rtbNotas_KeyUp(object sender, KeyEventArgs e)
        {
            if (!timerGuardar.Enabled)
                timerGuardar.Start();
        }
    }
}

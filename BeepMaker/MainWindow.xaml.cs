using BeepMaker.Conrols;
using System;
using System.Diagnostics;
using System.IO;
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
using System.Windows.Forms;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BeepMaker.Classes;

namespace BeepMaker
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddSound_Click(object sender, RoutedEventArgs e)
        {
            SoundValue value = new SoundValue(SoundsList) { Height=120};
        }

        private void AddPause_Click(object sender, RoutedEventArgs e)
        {
            PauseValue value = new PauseValue(SoundsList) { Height = 90 };
        }

        private void Play_Click(object sender, RoutedEventArgs e)
        {
            foreach(IMyControls control in SoundsList.Children)
            {
                control.Play();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Utility.ShowQuestion("Вы уверены что хотите создать новый файл?\n")) ;
            {
                FileName.Text = "";
                SoundsList.Children.Clear();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            StringBuilder build = new StringBuilder($"public static void {FileName.Text}_Play()" + Environment.NewLine);
            build.Append("{"+Environment.NewLine);

            foreach(IMyControls control in SoundsList.Children)
            {
                build.Append(control.Code);
            }

            build.Append("}");
            FolderBrowserDialog dialog = new FolderBrowserDialog()
            {
                ShowNewFolderButton = true,
                RootFolder = Environment.SpecialFolder.Desktop,
                Description = "Выберите папку для сохранения"
            };

            dialog.ShowDialog();
            
            string path = $"{dialog.SelectedPath}\\{FileName.Text} Code.txt";
            File.Create(path).Close();

            File.WriteAllText(path, build.ToString());

            Process.Start(path);
        }
    }
}

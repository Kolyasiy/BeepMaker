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
        private FolderBrowserDialog folderDialog = new FolderBrowserDialog()
        {
            ShowNewFolderButton = true,
            RootFolder = Environment.SpecialFolder.Desktop,
            Description = "Выберите папку для сохранения"
        };

        private OpenFileDialog fileDialog = new OpenFileDialog()
        {
            InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
            Multiselect = false,
            Title = "Выберите файл для загрузки",
            Filter = "сохранения (*.stxt)|*.stxt"
        };

        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddSound_Click(object sender, RoutedEventArgs e)
        {
            SoundValue value = new SoundValue(SoundsList);
        }

        private void AddPause_Click(object sender, RoutedEventArgs e)
        {
            PauseValue value = new PauseValue(SoundsList);
        }

        private void Play_Click(object sender, RoutedEventArgs e)
        {
            foreach(IMyControls control in SoundsList.Children)
            {
                control.Play();
            }
        }

        private void NewWork(object sender, RoutedEventArgs e)
        {
            if (Utility.ShowQuestion("Вы уверены что хотите создать новый файл?\n"))
            {
                FileName.Text = "";
                SoundsList.Children.Clear();
            }
        }

        /// <summary>
        /// Generate finished method to .txt file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GenerateMethod(object sender, RoutedEventArgs e)
        {
            StringBuilder build = new StringBuilder($"public static void {FileName.Text.Replace(" ","")}_Play()" + Environment.NewLine);
            build.Append("{"+Environment.NewLine);

            foreach(IMyControls control in SoundsList.Children)
            {
                build.Append(control.Code);
            }

            build.Append("}");
            DialogResult res = folderDialog.ShowDialog();

            if (res != System.Windows.Forms.DialogResult.Cancel)
            {
                string path = $"{folderDialog.SelectedPath}\\{FileName.Text} Code.txt";
                File.Create(path).Close();

                File.WriteAllText(path, build.ToString());

                Process.Start(path);
            }
        }

        /// <summary>
        /// Hotkeys
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.OemTilde:
                    PauseValue value = new PauseValue(SoundsList) { Height = 90 };
                    break;

                case Key.Q:
                    SoundValue soundQ = new SoundValue(SoundsList, 261, 1000);
                    break;

                case Key.D2:
                    SoundValue sound2 = new SoundValue(SoundsList, 277, 1000);
                    break;

                case Key.W:
                    SoundValue soundW = new SoundValue(SoundsList, 293, 1000);
                    break;

                case Key.D3:
                    SoundValue sound3 = new SoundValue(SoundsList, 311, 1000);
                    break;

                case Key.E:
                    SoundValue soundE = new SoundValue(SoundsList, 329, 1000);
                    break;

                case Key.R:
                    SoundValue soundR = new SoundValue(SoundsList, 349, 1000);
                    break;

                case Key.D5:
                    SoundValue sound5 = new SoundValue(SoundsList, 369, 1000);
                    break;

                case Key.T:
                    SoundValue soundT = new SoundValue(SoundsList, 392, 1000);
                    break;

                case Key.D6:
                    SoundValue sound6 = new SoundValue(SoundsList, 415, 1000);
                    break;

                case Key.Y:
                    SoundValue soundY = new SoundValue(SoundsList, 440, 1000);
                    break;

                case Key.D7:
                    SoundValue sound7 = new SoundValue(SoundsList, 466, 1000);
                    break;

                case Key.U:
                    SoundValue soundU = new SoundValue(SoundsList, 493, 1000);
                    break;
            }
        }

        
        /// <summary>
        /// Create special-formated file(*.stxt) and saves data in it
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveFile(object sender, RoutedEventArgs e)
        {
            StringBuilder build = new StringBuilder();
            foreach (IMyControls control in SoundsList.Children)
            {
                build.Append(control.Code);
            }

            DialogResult res = folderDialog.ShowDialog();

            if (res != System.Windows.Forms.DialogResult.Cancel)
            {
                string path = $"{folderDialog.SelectedPath}\\{FileName.Text}.stxt";
                File.Create(path).Close();
                File.WriteAllText(path, build.ToString());
            }
        }

        /// <summary>
        /// Load data from special-formated file(*.stxt)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoadFile(object sender, RoutedEventArgs e)
        {
            DialogResult res = fileDialog.ShowDialog();
            if (res != System.Windows.Forms.DialogResult.Cancel)
            {
                SoundsList.Children.Clear();
                string path = fileDialog.FileName;

                string[] vs = File.ReadAllLines(path);

                for (int i = 0; i < vs.Length - 1; i++)
                {
                    string[] values = vs[i].Split(',');

                    if (values.Length > 1)
                    {
                        SoundValue value = new SoundValue(SoundsList, Utility.GetDigits(values[0]), Utility.GetDigits(values[1]));
                    }
                    else
                    {
                        PauseValue value = new PauseValue(SoundsList, Utility.GetDigits(values[0]));
                    }
                }

                FileName.Text = fileDialog.SafeFileName.Replace(".stxt", "");
            }
        }

    }
}

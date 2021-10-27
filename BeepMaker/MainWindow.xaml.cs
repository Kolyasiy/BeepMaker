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
        public string FileName { get; set; }
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
            App.HotkeysOn = true;
        }


        private void AddSound_Click(object sender, RoutedEventArgs e) => _ = new SoundValue(SoundsList);

        private void AddPause_Click(object sender, RoutedEventArgs e) => _ = new PauseValue(SoundsList);

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
                NewFileDialog dialog = new NewFileDialog();
                if (dialog.ShowDialog().Value)
                {
                    FileName = dialog.FileName.Text;
                    SoundsList.Children.Clear();
                }
            }
        }

        /// <summary>
        /// Generate finished method to .txt file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GenerateMethod(object sender, RoutedEventArgs e)
        {
            StringBuilder build = new StringBuilder($"public static void {FileName.Replace(" ","")}_Play()" + Environment.NewLine);
            build.Append("{"+Environment.NewLine);

            foreach(IMyControls control in SoundsList.Children)
            {
                build.Append(control.Code);
            }

            build.Append("}");
            DialogResult res = folderDialog.ShowDialog();

            if (res != System.Windows.Forms.DialogResult.Cancel)
            {
                string path = $"{folderDialog.SelectedPath}\\{FileName} Code.txt";
                File.Create(path).Close();

                File.WriteAllText(path, build.ToString());

                Process.Start(path);
            }
        }

        private void HotkeysCall(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (App.HotkeysOn)
            {
                switch (e.Key)
                {
                    case Key.OemTilde:
                        _ = new PauseValue(SoundsList) { Height = 90 };
                        break;

                    case Key.Q:
                        _ = new SoundValue(SoundsList, 261, 1000);
                        break;

                    case Key.D2:
                        _ = new SoundValue(SoundsList, 277, 1000);
                        break;

                    case Key.W:
                        _ = new SoundValue(SoundsList, 293, 1000);
                        break;

                    case Key.D3:
                        _ = new SoundValue(SoundsList, 311, 1000);
                        break;

                    case Key.E:
                        _ = new SoundValue(SoundsList, 329, 1000);
                        break;

                    case Key.R:
                        _ = new SoundValue(SoundsList, 349, 1000);
                        break;

                    case Key.D5:
                        _ = new SoundValue(SoundsList, 369, 1000);
                        break;

                    case Key.T:
                        _ = new SoundValue(SoundsList, 392, 1000);
                        break;

                    case Key.D6:
                        _ = new SoundValue(SoundsList, 415, 1000);
                        break;

                    case Key.Y:
                        _ = new SoundValue(SoundsList, 440, 1000);
                        break;

                    case Key.D7:
                        _ = new SoundValue(SoundsList, 466, 1000);
                        break;

                    case Key.U:
                        _ = new SoundValue(SoundsList, 493, 1000);
                        break;
                }
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
                string path = $"{folderDialog.SelectedPath}\\{FileName}.stxt";
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
                        _ = new SoundValue(SoundsList, Utility.GetDigits(values[0]), Utility.GetDigits(values[1]));
                    }
                    else
                    {
                        _ = new PauseValue(SoundsList, Utility.GetDigits(values[0]));
                    }
                }

                FileName = fileDialog.SafeFileName.Replace(".stxt", "");
            }
        }
    }
}

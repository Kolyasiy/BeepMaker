using BeepMaker.Classes;
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

namespace BeepMaker.Conrols
{
    /// <summary>
    /// Логика взаимодействия для SoundValue.xaml
    /// </summary>
    public partial class SoundValue : UserControl, IMyControls
    {
        public StackPanel Owner { get; set; }
        public int ID { get; set; }
        public string Code { get => $"\tConsole.Beep({Frequency.Text}, {Duration.Text});{Environment.NewLine}"; }


        public SoundValue()
        {
            InitializeComponent();
        }

        public SoundValue(StackPanel owner)
        {
            InitializeComponent();
            ID = owner.Children.Count;
            owner.Children.Add(this);

            Owner = owner;
            MainPart.Header = $"Звук №{ID + 1}";
        }

        private void PlaySound_Click(object sender, RoutedEventArgs e) => Play();

        public void Play()
        {
            try
            {
                Console.Beep(Convert.ToInt32(Frequency.Text), Convert.ToInt32(Duration.Text));
            }
            catch
            {
                Utility.ShowError($"Некорректные данные для воспроизведения в звуке №{ID + 1}");
            }
        }

        public void PlayToHere_Click(object sender, RoutedEventArgs e)
        {
            for(int i = 0; i <= this.ID; i++)
            {
                if (Owner.Children[i] is IMyControls)
                    (Owner.Children[i] as IMyControls).Play();
            }            
        }

        public void Delete_Click(object sender, RoutedEventArgs e) => Utility.DeleteItem(Owner, this);
    }
}

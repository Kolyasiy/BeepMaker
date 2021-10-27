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
        public string Code { get => $"\tConsole.Beep({Frequency.Text}, {Duration.Text});{Environment.NewLine}"; }
        private int id;
        public int ID 
        { 
            get => id;
            set
            {
                id = value;
                MainPart.Header = $"Звук №{id + 1}";
            } 
        }

        public SoundValue() => InitializeComponent();

        public SoundValue(StackPanel owner) => BaseConstruction(owner);

        public SoundValue(StackPanel owner, int freq, int duration)
        {
            BaseConstruction(owner);
            Frequency.Text = freq.ToString();
            Duration.Text = duration.ToString();
        }

        private void BaseConstruction(StackPanel owner)
        {
            InitializeComponent();
            ID = owner.Children.Count;
            owner.Children.Add(this);

            Owner = owner;
            MainPart.Header = $"Звук №{ID + 1}";
            Utility.TextBoxSetEvents(Duration);
            Utility.TextBoxSetEvents(Frequency);
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
                (Owner.Children[i] as IMyControls).Play();
            }         
        }

        public void PlayFromHere_Click(object sender, RoutedEventArgs e)
        {
            for(int i = this.ID; i < Owner.Children.Count; i++)
            {
                (Owner.Children[i] as IMyControls).Play();
            }
        }

        public void Delete_Click(object sender, RoutedEventArgs e) => Utility.DeleteItem(Owner, this);

        private void UserControl_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.LeftShift:
                    IncreaseFrequency();
                    break;

                case Key.LeftCtrl:
                    DecreaseFrequency();
                    break;
            }
        }

        private void DecreaseFrequency()
        {
            try
            {
                Frequency.Text = (Convert.ToInt32(Frequency.Text) / 2).ToString();
            }
            catch
            {
                Utility.ShowError("Некорректные данные в поле \"Частота\"");
            }
        }

        private void IncreaseFrequency()
        {
            try
            {
                Frequency.Text = (Convert.ToInt32(Frequency.Text) * 2).ToString();
            }
            catch
            {
                Utility.ShowError("Некорректные данные в поле \"Частота\"");
            }
        }

        private void CopySound_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _ = new SoundValue(Owner, Convert.ToInt32(Frequency.Text), Convert.ToInt32(Duration.Text));
            }
            catch
            {
                Utility.ShowError("Некорректные данные звука");
            }
        }

        private void Mult2_Click(object sender, RoutedEventArgs e) => IncreaseFrequency();

        private void Div_Click(object sender, RoutedEventArgs e) => DecreaseFrequency();
    }
}
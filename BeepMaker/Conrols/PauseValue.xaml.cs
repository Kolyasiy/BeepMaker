using System;
using System.Threading;
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
using BeepMaker.Classes;

namespace BeepMaker.Conrols
{
    /// <summary>
    /// Логика взаимодействия для PauseValue.xaml
    /// </summary>
    public partial class PauseValue : UserControl, IMyControls
    {
        public StackPanel Owner { get; set; }
        public int ID { get; set; }
        public string Code { get => $"\tThread.Sleep({Duration.Text});{Environment.NewLine}"; }

        public PauseValue()
        {
            InitializeComponent();
        }

        public PauseValue(StackPanel owner)
        {
            InitializeComponent();
            ID = owner.Children.Count;
            owner.Children.Add(this);

            Owner = owner;
            MainPart.Header = $"Пауза №{ID + 1}";
        }

        public void Play()
        {
            try
            { 
                Thread.Sleep(Convert.ToInt32(Duration.Text));
            }
            catch
            {
                Utility.ShowError($"Некорректные данные в паузе №{ID + 1}");
            }
        }

        public void PlayToHere_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i <= this.ID; i++)
            {
                if (Owner.Children[i] is IMyControls)
                    (Owner.Children[i] as IMyControls).Play();
            }
        }

        public void Delete_Click(object sender, RoutedEventArgs e) => Utility.DeleteItem(Owner, this);
    }
}

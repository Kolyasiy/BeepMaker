using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BeepMaker.Conrols
{
    interface IMyControls
    {
        StackPanel Owner { get; set; }
        int ID { get; set; }
        string Code { get; }

        void Play();
        void PlayToHere_Click(object sender, RoutedEventArgs e);
        void PlayFromHere_Click(object sender, RoutedEventArgs e);
        void Delete_Click(object sender, RoutedEventArgs e);
    }
}

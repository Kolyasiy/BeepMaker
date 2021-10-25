using System;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeepMaker.Conrols;

namespace BeepMaker.Classes
{
    class Utility
    {
        public static void DeleteItem(StackPanel panel, UserControl control)
        {
            panel.Children.Remove(control);
            for(int i = 0; i < panel.Children.Count; i++)
            {
                if(panel.Children[i] is IMyControls)
                    (panel.Children[i] as IMyControls).ID = i;
            }
        }

        public static void ShowError(string val) => MessageBox.Show(val, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

        public static bool ShowQuestion(string val)
        {
            MessageBoxResult res = MessageBox.Show(val, "", MessageBoxButton.YesNo, MessageBoxImage.Question);
            return res == MessageBoxResult.Yes;
        }
    }
}

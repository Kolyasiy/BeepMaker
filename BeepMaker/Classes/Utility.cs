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
                if (panel.Children[i] is IMyControls)
                {
                    (panel.Children[i] as IMyControls).ID = i;
                }
            }
        }

        public static int GetDigits(string val)
        {
            StringBuilder build = new StringBuilder();
            char[] digits = val.Where(x => char.IsDigit(x)).ToArray();

            foreach (char c in digits)
                build.Append(c);

            return Convert.ToInt32(build.ToString());
        }

        public static void ShowError(string val) => MessageBox.Show(val, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

        public static bool ShowQuestion(string val)
        {
            MessageBoxResult res = MessageBox.Show(val, "", MessageBoxButton.YesNo, MessageBoxImage.Question);
            return res == MessageBoxResult.Yes;
        }

        public static void TextBoxSetEvents(TextBox box)
        {
            box.GotFocus += TextBox_GotFocus;
            box.LostFocus += TextBox_LostFocus;
        }

        private static void TextBox_GotFocus(object sender, RoutedEventArgs e) => App.HotkeysOn = false;

        private static void TextBox_LostFocus(object sender, RoutedEventArgs e) => App.HotkeysOn = true;
    }
}

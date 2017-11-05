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

namespace SmartBlame
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        Encryptor encryptor;

        public MainWindow()
        {
            InitializeComponent();

            encryptor = new Encryptor();
        }

        private void ExcuteButton_Click(object sender = null, RoutedEventArgs e = null)
        {
            OutputTextBox.Text = encryptor.Encrypt(InputTextBox.Text);
        }

        private void YaminButton_Click(object sender, RoutedEventArgs e)
        {
            encryptor.Yamin = !encryptor.Yamin;
            if (encryptor.Yamin)
                YaminButton.Background = Brushes.Red;
            else
                YaminButton.Background = Brushes.Gray;
        }

        private void RemoveIeungButton_Click(object sender, RoutedEventArgs e)
        {
            encryptor.RemoveIeung = !encryptor.RemoveIeung;
            if (encryptor.RemoveIeung)
                RemoveIeungButton.Background = Brushes.Red;
            else
                RemoveIeungButton.Background = Brushes.Gray;
        }
    }
}

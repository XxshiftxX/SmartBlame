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
            encryptor.Encrypt("안");
        }
    }
}

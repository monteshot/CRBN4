using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;

namespace VrachMedcentr.View
{
    /// <summary>
    /// Логика взаимодействия для UpdateView.xaml
    /// </summary>
    public partial class UpdateView : Window,INotifyPropertyChanged
    {
        public string TitleString { get; set; }

        public UpdateView()
        {
            InitializeComponent();
           // Title += " до версії " + TitleString;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = this.PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

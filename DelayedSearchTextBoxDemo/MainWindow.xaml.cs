using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace DelayedSearchTextBoxDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private Guid _updateID;
        public Guid UpdateID
        {
            get { return _updateID; }
            set
            {
                _updateID = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        protected void OnPropertyChanged([CallerMemberName]string sourcePropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(sourcePropertyName));
        }

        private void MySearchTextBox_OnDelayedSearch(object sender, RoutedEventArgs e)
        {
            // Search logic here.

            // GUID to demonstrate UI updates when the keystroke stops.
            UpdateID = Guid.NewGuid();
        }
    }
}

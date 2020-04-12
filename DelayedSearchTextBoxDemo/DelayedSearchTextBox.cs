using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace DelayedSearchTextBoxDemo
{
    public class DelayedSearchTextBox : TextBox
    {
        private const double _defaultDelay = 300d;
        private const string DelayedSearchTextBoxName = "TxtBxDelayedSearch";

        private DispatcherTimer _delayTimer;

        public double KeystrokeDelayMilliseconds
        {
            get { return (double)GetValue(KeystrokeDelayMillisecondsProperty); }
            set { SetValue(KeystrokeDelayMillisecondsProperty, value); }
        }
        
        public static readonly DependencyProperty KeystrokeDelayMillisecondsProperty =
            DependencyProperty.Register(nameof(KeystrokeDelayMilliseconds), typeof(double), typeof(DelayedSearchTextBox), 
                new PropertyMetadata(_defaultDelay));

        public event RoutedEventHandler OnDelayedSearch;

        static DelayedSearchTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DelayedSearchTextBox),
                new FrameworkPropertyMetadata(typeof(DelayedSearchTextBox)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            var txtBxDelayedSearch = GetTemplateChild(DelayedSearchTextBoxName) as TextBox;

            txtBxDelayedSearch.TextChanged += TxtBxDelayedSearch_TextChanged;

            // @Ivan
            // It is important to use a DispatcherTimer instead of a regular Timer so that
            // objects created by the UI thread is accessible using the Dispatcher queue
            // provided by the DispatcherTimer.
            _delayTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(KeystrokeDelayMilliseconds)
            };
            _delayTimer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            OnDelayedSearch?.Invoke(sender, null);

            _delayTimer.Stop();
        }

        private void TxtBxDelayedSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            _delayTimer.Stop();
            _delayTimer.Start();
        }
    }
}

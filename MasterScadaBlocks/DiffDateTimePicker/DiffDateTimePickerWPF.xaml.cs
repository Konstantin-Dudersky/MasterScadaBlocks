using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace MasterScadaBlocks.DiffDateTimePicker
{
    public class ChangedEventArgs : EventArgs
    {
        private readonly DateTime beginTime;
        private readonly DateTime endTime;
        private readonly bool lastDay;


        public ChangedEventArgs(bool ld, DateTime bt, DateTime et)
        {
            lastDay = ld;
            beginTime = bt;
            endTime = et;
        }

        public DateTime BeginTime => beginTime;
        public DateTime EndTime => endTime;

        public bool LastDay => lastDay;
    }

    /// <summary>
    /// Interaction logic for DiffDateTimePickerWPF.xaml
    /// </summary>
    public partial class DiffDateTimePickerWPF : UserControl
    {
        private DispatcherTimer timer = null;
        Random random = new Random();

        public DiffDateTimePickerWPF()
        {
            InitializeComponent();

            timer = new DispatcherTimer
            {
                Interval = new TimeSpan(0, 0, 0, 10, 0),
            };
            timer.Tick += Timer_Tick;

            LastDay.IsChecked = true;
            OnCheckboxChanged(true);
        }

        public event EventHandler<ChangedEventArgs> CheckboxChanged;

        public virtual void OnCheckboxChanged(bool lastDay)
        {
            DateTime beginTime, endTime;

            if (lastDay)
            {
                timer.Start();

                beginTime = DateTime.Now.AddDays(-1);
                endTime = DateTime.Now;

                BeginYear.Text = beginTime.Year.ToString();
                BeginYear.IsEnabled = false;
                BeginMonth.Text = beginTime.Month.ToString();
                BeginMonth.IsEnabled = false;
                BeginDay.Text = beginTime.Day.ToString();
                BeginDay.IsEnabled = false;
                BeginHour.Text = beginTime.Hour.ToString();
                BeginHour.IsEnabled = false;
                BeginMinute.Text = beginTime.Minute.ToString();
                BeginMinute.IsEnabled = false;

                EndYear.Text = endTime.Year.ToString();
                EndYear.IsEnabled = false;
                EndMonth.Text = endTime.Month.ToString();
                EndMonth.IsEnabled = false;
                EndDay.Text = endTime.Day.ToString();
                EndDay.IsEnabled = false;
                EndHour.Text = endTime.Hour.ToString();
                EndHour.IsEnabled = false;
                EndMinute.Text = endTime.Minute.ToString();
                EndMinute.IsEnabled = false;

                CheckboxChanged?.Invoke(this, new ChangedEventArgs(lastDay, beginTime, endTime));
            }
            else
            {
                timer.Stop();

                BeginYear.IsEnabled = true;
                BeginMonth.IsEnabled = true;
                BeginDay.IsEnabled = true;
                BeginHour.IsEnabled = true;
                BeginMinute.IsEnabled = true;
                EndYear.IsEnabled = true;
                EndMonth.IsEnabled = true;
                EndDay.IsEnabled = true;
                EndHour.IsEnabled = true;
                EndMinute.IsEnabled = true;

                try
                {
                    beginTime = new DateTime(
                        Convert.ToInt32(BeginYear.Text),
                        Convert.ToInt32(BeginMonth.Text),
                        Convert.ToInt32(BeginDay.Text),
                        Convert.ToInt32(BeginHour.Text),
                        Convert.ToInt32(BeginMinute.Text),
                        random.Next(60));
                    endTime = new DateTime(
                        Convert.ToInt32(EndYear.Text),
                        Convert.ToInt32(EndMonth.Text),
                        Convert.ToInt32(EndDay.Text),
                        Convert.ToInt32(EndHour.Text),
                        Convert.ToInt32(EndMinute.Text),
                        0);
                    CheckboxChanged?.Invoke(this, new ChangedEventArgs(lastDay, beginTime, endTime));
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message);
                }
            }
        }

        private void _KeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                OnCheckboxChanged(false);
            }
        }

        private void BeginDay_KeyDown(object sender, KeyEventArgs e)
        {
            _KeyDown(e);
        }

        private void BeginHour_KeyDown(object sender, KeyEventArgs e)
        {
            _KeyDown(e);
        }

        private void BeginMinute_KeyDown(object sender, KeyEventArgs e)
        {
            _KeyDown(e);
        }

        private void BeginMonth_KeyDown(object sender, KeyEventArgs e)
        {
            _KeyDown(e);
        }

        private void BeginYear_KeyDown(object sender, KeyEventArgs e)
        {
            _KeyDown(e);
        }

        private void EndDay_KeyDown(object sender, KeyEventArgs e)
        {
            _KeyDown(e);
        }

        private void EndHour_KeyDown(object sender, KeyEventArgs e)
        {
            _KeyDown(e);
        }

        private void EndMinute_KeyDown(object sender, KeyEventArgs e)
        {
            _KeyDown(e);
        }

        private void EndMonth_KeyDown(object sender, KeyEventArgs e)
        {
            _KeyDown(e);
        }

        private void EndYear_KeyDown(object sender, KeyEventArgs e)
        {
            _KeyDown(e);
        }

        private void LastDay_Checked(object sender, RoutedEventArgs e)
        {
            OnCheckboxChanged(true);
        }

        private void LastDay_Unchecked(object sender, RoutedEventArgs e)
        {
            OnCheckboxChanged(false);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            OnCheckboxChanged(true);
        }
    }
}
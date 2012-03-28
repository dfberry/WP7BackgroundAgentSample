using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using SampleShared;

namespace SampleApp1
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Set the data context of the listbox control to the sample data
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);
        }

        // Load data for the ViewModel Items
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            App.ViewModel.ReadData();
            UpdateUI();
        }
        // stop agent
        private void AgentStopButton_Click(object sender, RoutedEventArgs e)
        {
            App.ViewModel.RemoveAgent();
            AgentRunNowButton.IsEnabled = false;
        }
        // start agent
        private void AgentStartButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                App.ViewModel.AddAgent();
                AgentRunNowButton.IsEnabled = true;
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("Can't schedule agent; either there are too many other agents scheduled or you have disabled this agent in Settings.");
            }

        }
        // start agent
        private void AgentRunNow_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                App.ViewModel.RunDebugAgent();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void UpdateUI()
        {
            textBlock1.Text = App.ViewModel.MutexedData.LastProcessToTouchFile;
            textBlock2.Text = App.ViewModel.MutexedData.LastTimeFileTouched.ToString();

            CheckboxInfiniteAgentCycle.IsChecked = App.ViewModel.MutexedData.CycleAgentEveryMinute;

            Focus();
        }
        // read mutexed data
        private void IsoReadButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                App.ViewModel.ReadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


            UpdateUI();
        }
        // write mutexed data
        private void IsoWriteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                App.ViewModel.WriteData();
                App.ViewModel.ReadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            UpdateUI();
        }
        // update tile by calling non-ui thread to do the work
        private void TileUpdateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                App.ViewModel.UpdateTile();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CheckboxInfiniteAgentCycle_Checked(object sender, RoutedEventArgs e)
        {
            App.ViewModel.CycleAgentEveryMinute = true;
            App.ViewModel.WriteData();
            UpdateUI();
        }

        private void CheckboxInfiniteAgentCycle_Unchecked(object sender, RoutedEventArgs e)
        {
            App.ViewModel.CycleAgentEveryMinute = false;
            App.ViewModel.WriteData();
            UpdateUI();
        }
    }
}
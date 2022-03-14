﻿using CGTOnboardingTool.ViewModels;
using MahApps.Metro.Controls;
using System.Windows;
using System.Windows.Controls;

namespace CGTOnboardingTool.Views
{
    /// <summary>
    /// Interaction logic for StartUpView.xaml
    /// </summary>
    public partial class StartUpView : Page
    {
        private MetroWindow startUpWindow;
        private Frame startUpFrame;
        
        public StartUpView(MetroWindow startUpWindow, Frame startUpFrame)
        {
            InitializeComponent();
            this.startUpWindow = startUpWindow;
            this.startUpFrame = startUpFrame;
        }

        private void StartUpNew_Click(object sender, RoutedEventArgs e)
        {
            ConstructReportViewModel constructReportViewModel = new ConstructReportViewModel();
            startUpFrame.Navigate(new ConstructReportView(startUpWindow,constructReportViewModel));
        }
    }
}
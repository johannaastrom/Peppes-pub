﻿using System;
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
using System.Runtime.InteropServices;
using System.Threading;

namespace Labb6
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CancellationTokenSource cts = new CancellationTokenSource(); //property för att stoppa trådar.
        bool isBarOpen = false;
        int numberofGuests;
        private Queue<Glass> shelf = new Queue<Glass>();

        public void PlaceGlassOnShelf(Glass cleanGlass)
        {
            shelf.Enqueue(cleanGlass);
        }
        public Glass GetGlassFromShelf()
        {
            return shelf.Dequeue();
            
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void printBouncerInfo(string bInfo)
        {
            Dispatcher.Invoke(() =>
            {
                BouncerListBox.Items.Insert(0, bInfo);
            });

            if (!isBarOpen)
            {
                Dispatcher.Invoke(() =>
                {
                    BouncerListBox.Items.Insert(0, "Bouncern går hem");
                });
            }

        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            isBarOpen = false;
            OpenButton.IsEnabled = true;
        }

        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            if (cts.IsCancellationRequested)
            {
                cts = new CancellationTokenSource();
            }
            CancellationToken ct = cts.Token;

            OpenButton.IsEnabled = false;
            isBarOpen = true;
            if (isBarOpen == true)
            {
                Bouncer b = new Bouncer();
                Task.Run(() =>
               {
                   while (isBarOpen)
                   {
                       b.CreateGuest(printBouncerInfo);
                       Queue<Bouncer> guestList = new Queue<Bouncer>();
                       guestList.Enqueue();

                       Dispatcher.Invoke(() =>
                       {
                           NumberOfGuests.Content = "Number of guests: " + ++numberofGuests;
                       });
                       if (!isBarOpen)
                       {
                           cts.Cancel();
                       }
                   }
               });
            }
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(Environment.ExitCode);
        }
    }
}


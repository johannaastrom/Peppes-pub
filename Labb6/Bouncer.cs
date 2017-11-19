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
using System.Threading;
using System.Windows.Threading;
using System.Collections.Concurrent;


namespace Labb6
{
    public class Bouncer
    {
        CancellationTokenSource cts = new CancellationTokenSource();

        private BlockingCollection<Patron> BartenderQueue;
        private BlockingCollection<Patron> PubQueue;

        public Func<bool> isBarOpen { get; set; }

        public Bouncer(BlockingCollection<Patron> barqueue)
        {
            this.BartenderQueue = barqueue;
        }

        public Bouncer() { }

        //Creates patrons by random time and name from list
        public Patron CreatePatron() 
        {
            List<string> guestList = new List<string>();
            guestList.Add("KulmageKarl");
            guestList.Add("GamleGreta");
            guestList.Add("KarateKarin");
            guestList.Add("HelikopterHerbert");
            guestList.Add("RundaRobin");
            guestList.Add("Samuel Adams");
            guestList.Add("NyktreNiklas");
            guestList.Add("OnyktreOlle");
            guestList.Add("GalneGunnar");
            guestList.Add("Johan");
            guestList.Add("AlagarAnders");
            guestList.Add("Erik");
            guestList.Add("ElakaElin");
            guestList.Add("Molly");
            guestList.Add("PackadePatrik");
            guestList.Add("VingligaVictoria");
            guestList.Add("Isabella");
            guestList.Add("Gustav");
            guestList.Add("Erika");
            guestList.Add("Jaqueline");
            guestList.Add("TörstigaTina");
            guestList.Add("HalstorreHans");
            guestList.Add("ÖlsugneÖrjan");
            guestList.Add("FulleFelix");
            guestList.Add("DrunkenDennis");
            guestList.Add("WobblyWilly");
            guestList.Add("CrazyCynthia");
            guestList.Add("SoberSandra");
            guestList.Add("DräggigeDan");
            guestList.Add("SnurrigeSamuel");

            Random rGuest = new Random();
            int randomGuestPosition = rGuest.Next(guestList.Count);
            string randomName = guestList[randomGuestPosition];

            var patron = new Patron();
            patron.Name = randomName;

            return patron;
        }

        //Creates a patron
        public void Work(Action<string> callback, Action<string> printNumberOfGuests) 
        {
            Random rTime = new Random();
            int numberOfGuests = 0;
           
            while (isBarOpen())
            {
                    Patron p = CreatePatron();
                    callback($"{p.Name} gets into the bar.");
                    BartenderQueue.Add(p); //Patron goes to the bar.
                    int randomTimePosition = rTime.Next(3, 10) * 1000;               //ändra tillbaka till 3, 10 sedan
                    Thread.Sleep(randomTimePosition);

                    printNumberOfGuests("Number of guests: " + ++numberOfGuests);
            }
            if (!isBarOpen())
                callback("The bouncer goes home.");
        }
    }
}

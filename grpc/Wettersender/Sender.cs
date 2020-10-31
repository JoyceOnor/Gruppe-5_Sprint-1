using System;
using System.Collections.Generic;
using System.Text;

namespace Wettersender
{
    class Sender
    {
        private readonly string seriennummer;
        private int temperatur;
        private int luftfeuchtigkeit;
        private int wetter;
        public Sender()
        {
            var rand = new Random();
            seriennummer = rand.Next(100000000, 999999999) + "";
            luftfeuchtigkeit = rand.Next(1, 100);
            temperatur = rand.Next(-10, 40);
            wetter = rand.Next(0, 5);
        }
        public string Seriennummer { get => seriennummer; }
        public int Temperatur
        {
            get => temperatur;
            set
            {
                if (value < -10)
                {
                    temperatur = -10;
                }
                else
                {
                    if (value > 40)
                    {
                        temperatur = 40;
                    }
                    else
                    {
                        temperatur = value;
                    }
                }
            }
        }
        public int Luftfeuchtigkeit
        {
            get => luftfeuchtigkeit;
            set
            {
                if (value < 0)
                {
                    luftfeuchtigkeit = 0;
                }
                else
                {
                    if (value > 100)
                    {
                        luftfeuchtigkeit = 100;
                    }
                    else
                    {
                        luftfeuchtigkeit = value;
                    }
                }
            }
        }
        /* Werte fürs Wetter         
         *0=sonnig      
         *1=bewölkt
         *2=schneefall
         *3=hagel
         *4=regen
         *5=gewitter
        */
        public int Wetter
        {
            get => wetter;
            set
            {
                if (value < 0)
                {
                    wetter = 0;
                }
                else
                {
                    if (value > 5)
                    {
                        wetter = 5;
                    }
                    else
                    {
                        wetter = value;
                    }
                }
            }
        }
        public override string ToString()
        {
            return "Seriennummer: " + Seriennummer + "; Luftfeuchtigkeit: " + Luftfeuchtigkeit + "; Temperatur: " + Temperatur + "; Wetter: " + Wetter;
        }
        public string nachrichtErstellen()
        {
            var rand = new Random();
            Luftfeuchtigkeit = Luftfeuchtigkeit + rand.Next(-5, 5);
            Temperatur = Temperatur + rand.Next(-2, 2);
            Wetter = Wetter + rand.Next(-1, 2);
            return Seriennummer + ";" + Luftfeuchtigkeit + ";" + Temperatur + ";" + Wetter;
        }
    }
}

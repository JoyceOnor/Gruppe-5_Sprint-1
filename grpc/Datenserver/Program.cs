using System;
using System.Collections.Generic;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using Datenserver.grpc;

namespace Datenserver
{
    class Program
    {
        static int zaehler = 1;
        static Dictionary<string, Daten> Sensoren = new Dictionary<string, Daten>();
        static List<string> seriennummern = new List<string>();
        static void Main(string[] args)
        {
            // erstelle Client
            MqttClient client = new MqttClient("localhost");
            // regestriere Reciever 
            client.MqttMsgPublishReceived += Client_recievedMessage;
            string clientId = Guid.NewGuid().ToString();
            client.Connect(clientId);
            Console.WriteLine("Datenserver: Wetterdaten/");
            client.Subscribe(new String[] { "Wetterdaten/" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
        }
        static void Client_recievedMessage(object sender, MqttMsgPublishEventArgs e)
        {
            //Um angekommene Nachricht kümmern.
            var message = System.Text.Encoding.Default.GetString(e.Message);
            String[] elemente = message.Split(';');
            Sensoren[elemente[0]] = new Daten(Convert.ToInt16(elemente[1]), Convert.ToInt16(elemente[2]), Convert.ToInt16(elemente[3]));
            if(!seriennummern.Contains(elemente[0]))
                seriennummern.Add(elemente[0]);
            try
            {
                if(zaehler % 3 == 0)
                    Vorhersagen();
                zaehler++;
            }
            catch(Grpc.Core.RpcException ex)
            {
                Console.WriteLine("[ERROR](" + DateTime.Now + "): " + ex.Message);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        static List<string> RelevanteDatenFiltern()
        {
            string aktuell_1 = "B";
            string aktuell_2 = "B";
            DateTime neu_1 = DateTime.MinValue;
            DateTime neu_2 = DateTime.MinValue;
            foreach(string sn in seriennummern)
            {
                Daten akt = Sensoren[sn];
                if ((DateTime.Now - akt.Erhalten).TotalSeconds > 60)
                {
                    seriennummern.Remove(sn);
                    break;
                }
                if (DateTime.Compare(neu_1,akt.Erhalten) < 0)
                {
                    neu_2 = neu_1;
                    aktuell_2 = aktuell_1;
                    neu_1 = akt.Erhalten;
                    aktuell_1 = sn;
                }
                else
                {
                    if (DateTime.Compare(neu_2, akt.Erhalten) < 0)
                    {
                        neu_2 = akt.Erhalten;
                        aktuell_2 = sn;
                    }
                }
            }
            /*if (neu_1 == DateTime.MinValue || neu_2 == DateTime.MinValue)
            {
                aktuell_1 = "B";
                aktuell_2 = "B";
            }*/

            return new List<string>() { aktuell_1, aktuell_2 };
        }
        static void Vorhersagen()
        {
            List<string> daten = RelevanteDatenFiltern();
            if (!daten.Contains("B"))
            {
                Console.WriteLine("WETTERVORHERSAGE(" + DateTime.Now + "):");
                Console.WriteLine("Temperatur: " + gRPC.temperaturBerechnen(Sensoren[daten[0]].Temperatur, Sensoren[daten[1]].Temperatur));
                Console.WriteLine("Luftfeuchtigkeit: " + gRPC.luftfeuchtigkeitBerechnen(Sensoren[daten[0]].Luftfeuchtigkeit, Sensoren[daten[1]].Luftfeuchtigkeit));
                Console.WriteLine("Wetter: " + gRPC.wetterBerechnen(Sensoren[daten[0]].Wetter, Sensoren[daten[1]].Wetter));
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("Temperatur: " + gRPC.temperaturBerechnen(int.MinValue, int.MinValue));
            }
        }
    }
}
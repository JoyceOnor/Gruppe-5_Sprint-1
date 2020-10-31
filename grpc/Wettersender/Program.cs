using System;
using System.Threading;
using Wettersender;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

class Program
{
    static void Main(string[] args)
    {
        MqttClient client = new MqttClient("localhost");
        string clientId = Guid.NewGuid().ToString();
        client.Connect(clientId);
        Sender send = new Sender();
            while(true)
            {
            Console.WriteLine("GESENDET(" + DateTime.Now + "): " + send);
            client.Publish( "Wetterdaten/", StringToByteArray(send.nachrichtErstellen()));
                Thread.Sleep(10000);
            }        
    }
    private static byte[] StringToByteArray(string str)
    {
        System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
        return enc.GetBytes(str);
    }
}

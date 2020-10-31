using Calculator;
using Grpc.Core;
using System;
using System.Threading.Tasks;

namespace Datenserver.grpc
{
    class gRPC
    {
        const string target = "127.0.0.1:5001";
        public static int temperaturBerechnen(int a, int b)
        {
            Channel channel = new Channel(target, ChannelCredentials.Insecure);

            channel.ConnectAsync().ContinueWith((task) =>
            {
                if (task.Status == TaskStatus.RanToCompletion)
                    Console.Write("");
            });
            var client = new CalculatorService.CalculatorServiceClient(channel);
            var request = new Request()
            {
                A = a,
                B = b
            };
            var response = client.Sum(request);
            channel.ShutdownAsync().Wait();
            return (response.Result);
        }
        public static int wetterBerechnen(int a, int b)
        {
            Channel channel = new Channel(target, ChannelCredentials.Insecure);

            channel.ConnectAsync().ContinueWith((task) =>
            {
                if (task.Status == TaskStatus.RanToCompletion)
                    Console.Write("");
            });
            var client = new CalculatorService.CalculatorServiceClient(channel);
            var request = new Request()
            {
                A = a,
                B = b
            };
            var response = client.Dif(request);
            channel.ShutdownAsync().Wait();
            return (response.Result);
        }
        public static int luftfeuchtigkeitBerechnen(int a, int b)
        {
            Channel channel = new Channel(target, ChannelCredentials.Insecure);

            channel.ConnectAsync().ContinueWith((task) =>
            {
                if (task.Status == TaskStatus.RanToCompletion)
                    Console.Write("");
            });
            var client = new CalculatorService.CalculatorServiceClient(channel);
            var request = new Request()
            {
                A = a,
                B = b
            };
            var response = client.Avg(request);
            channel.ShutdownAsync().Wait();
            return (response.Result);
        }
    }
}

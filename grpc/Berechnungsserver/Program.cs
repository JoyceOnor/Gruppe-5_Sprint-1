using System;
using Grpc.Core;
using System.IO;
using Calculator;

namespace server
{
    class Program
    {
        const int Port = 5001;
        static void Main(string[] args)
        {
            Server server = null;
            try
            {
                server = new Server()
                {
                    Services = { CalculatorService.BindService(new CalculatorServiceImpl())},
                    Ports = { new ServerPort("localhost", Port, ServerCredentials.Insecure) }
                };
                server.Start();
                Console.WriteLine("Server ist online. Erreichbar unter: localhost:" + Port);
                Console.ReadKey();
            }
            catch (IOException e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                if (server != null)
                    server.ShutdownAsync().Wait();
            }
        }
    }
}

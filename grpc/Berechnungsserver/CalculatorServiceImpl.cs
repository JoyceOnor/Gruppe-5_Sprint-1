using Calculator;
using Grpc.Core;
using System;
using System.Threading.Tasks;
using static Calculator.CalculatorService;

namespace server
{
    public class CalculatorServiceImpl : CalculatorServiceBase
    {
        public override async Task<Response> Sum(Request request, ServerCallContext context)
        {
            if(request.A == int.MinValue || request.B == int.MinValue)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Unausreichende Datengrundlage!"));
            }
            int result = request.A + request.B;
            Console.WriteLine("[INFO]("+DateTime.Now+"): Temperatur erfolgreich berechnet!");
            return await Task.FromResult(new Response() { Result = result });
        }

        public override async Task<Response> Avg(Request request, ServerCallContext context)
        {
            if (request.A == int.MinValue || request.B == int.MinValue)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Unausreichende Datengrundlage!"));
            }
            int result = (request.A + request.B)/2;
            Console.WriteLine("[INFO](" + DateTime.Now + "): Luftfeuchtigkeit erfolgreich berechnet!");
            return await Task.FromResult(new Response() { Result = result });
        }

        public override async Task<Response> Dif(Request request, ServerCallContext context)
        {
            if (request.A == int.MinValue || request.B == int.MinValue)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Unausreichende Datengrundlage!"));
            }
            int result = Math.Abs(request.A - request.B);
            Console.WriteLine("[INFO](" + DateTime.Now + "): Wetter erfolgreich berechnet!");
            return await Task.FromResult(new Response() { Result = result });
        }
    }
}

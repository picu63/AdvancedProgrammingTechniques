using Grpc.Net.Client;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcUserClient
{
    public class Program
    {
       static async Task Main(string[] args)
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:5002");
            var client = new User.UserClient(channel);
            var reply = await client.SayHelloAsync(
                new HelloRequest { Name = "Adam Nowok" });
            Console.WriteLine($"Odpowiedü: {reply.Message}");
        }

        // Additional configuration is required to successfully run gRPC on macOS.
        // For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682
    }
}

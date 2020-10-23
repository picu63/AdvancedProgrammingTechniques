using System;
using System.IO;
using System.Threading.Tasks;
using FluentEmail.Core;
using FluentEmail.Core.Defaults;
using FluentEmail.Razor;
using NLog.Fluent;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "EmailTest");

            Email.DefaultSender = new SaveToDiskSender(path);
            Email.DefaultRenderer = new RazorRenderer();

            var template = "Dear @Model.Name, You are totally @Model.Compliment.";

            var email = Email
                .From("john@email.com")
                .To("bob@email.com", "bob")
                .UsingTemplate(template, new {Name = "Luke", Compliment = "stupid"})
                .Send();
            Console.WriteLine(email.Successful);
        }
    }
}

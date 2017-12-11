using System;
using System.Collections.Generic;
using System.Linq;
using CatsApp.Dto;
using CatsApp.Service;
using StructureMap;

namespace CatsApp.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = Container.For<ConsoleRegistry>();

            var app = container.GetInstance<Application>();
            app.Run();
            Console.ReadLine();
        }
    }
}
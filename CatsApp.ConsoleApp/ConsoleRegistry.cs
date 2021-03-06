﻿using System.Net.Http;
using CatsApp.Data;
using StructureMap;

namespace CatsApp.ConsoleApp
{
    public class ConsoleRegistry : Registry
    {
        public ConsoleRegistry()
        {
            Scan(scan =>
            {
                scan.LookForRegistries();
                scan.Assembly("CatsApp.Data");
                scan.Assembly("CatsApp.Repository");
                scan.Assembly("CatsApp.Service");
                scan.WithDefaultConventions();
            });
            For<IDataContext>().Use<JsonDataContext>();
            For<HttpMessageHandler>().Use<HttpClientHandler>();
        }
    }
}

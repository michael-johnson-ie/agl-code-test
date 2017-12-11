using System;
using System.Collections.Generic;
using System.Text;
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
                scan.Assembly("CatsApp.Service");
                scan.Assembly("CatsApp.Repository");
                scan.Assembly("CatsApp.Data");
                scan.WithDefaultConventions();
            });
        }
    }
}

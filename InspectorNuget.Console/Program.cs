using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InspectorNuget.Core;

namespace InspectorNuget.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            VsSolution solution = new VsSolution(@"C:\Arbeid\etoto\code\trunk\services\AgentService\Solution\AgentService.sln");
            Console.Read();
        }
    }


}

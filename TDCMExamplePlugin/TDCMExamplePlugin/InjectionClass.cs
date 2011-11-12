using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDCMExamplePlugin
{
    public class InjectionClass
    {
        public static void OnFindSpawn(dynamic Player)
        {
            Console.WriteLine("{0} has called FindSpawn!", Player.name);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TDCM.Plugin;
using TDCM;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace TDCMExamplePlugin
{
    public class Example : Plugin
    {
        public TDCM.EProgram.InjectData SpawnInjection;

        public Example()
        {
            base.Name = "Example";
            base.Description = "TDCM Example Plugin with Injections";
            base.Author = "DeathCradle";
            base.TDCMBuild = 1;
        }

        public override void Load()
        {
            //Load Data
        }

        public override void Enable()
        {
            //Apply Hooks
            SpawnInjection = new EProgram.InjectData(ExtraInjection);
            EProgram.InjectionHandler += SpawnInjection;
        }

        public override void Disable()
        {
            //Remove Hooks
            EProgram.InjectionHandler -= SpawnInjection;
        }
        
        bool ExtraInjection()
        {
            string Method = "FindSpawn";
            string InsertionMethod = "OnFindSpawn";
            string Class = "Terraria.Player";

            Console.Write("[Plugin] Modifying {0}...", Class);

            TypeDefinition type = Inject.TerrariaModule.Types[Class]; //Find the Terraria Class
            MethodDefinition oldData = Inject.GetFunction(type, Method); //Extract the method we need
            MethodReference ourMethod = Inject.TerrariaModule.Import(typeof(InjectionClass).GetMethod(InsertionMethod)); //Import our data into the module

            CilWorker cil = oldData.Body.CilWorker;

            //Insert our method
            cil.InsertBefore(oldData.Body.Instructions[0], cil.Create(OpCodes.Call, ourMethod)); //Our method
            cil.InsertBefore(oldData.Body.Instructions[0], cil.Create(OpCodes.Ldarg_0)); //since it's not static, there is a class loaded, Thus a Player class, "Our Player"

            Console.WriteLine("Ok");

            return true;
        }
    }
}

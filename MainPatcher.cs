using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using VehicleFramework.VehicleTypes;
using System.Collections;
using VehicleFramework;
using PrecBlade;
using BepInEx;
using HarmonyLib;
using Nautilus.Utility;

namespace PrecBlade
{
    public static class Logger
    {
        public static void Log(string message)
        {
            UnityEngine.Debug.Log("[PrecBlade]:" + message);
        }
        public static void Output(string msg)
        {
            BasicText message = new BasicText(500, 0);
            message.ShowMessage(msg, 5);
        }
    }
    [BepInPlugin("com.royalty.subnautica.PrecBlade.mod", "PrecBlade", "2.0.2")]
    [BepInDependency("com.mikjaw.subnautica.vehicleframework.mod")]
    [BepInDependency("com.snmodding.nautilus")]

    public class MainPatcher : BaseUnityPlugin
    {
        public void Start()
        {
            var harmony = new Harmony("com.royalty.subnautica.PrecBlade.mod");
            harmony.PatchAll();
            UWE.CoroutineHost.StartCoroutine(PrecBlade.Register());
        }
    }
}

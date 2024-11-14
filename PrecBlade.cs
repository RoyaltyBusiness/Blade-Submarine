using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VehicleFramework;
using VehicleFramework.VehicleParts;
using VehicleFramework.VehicleTypes;
using System.IO;
using System.Reflection;
using UnityEngine.U2D;
using System.Collections;
using UWE;
using static Nautilus.Assets.PrefabTemplates.FabricatorTemplate;
using UnityEngine.Assertions;
using VehicleFramework.Assets;
using VehicleFramework.Engines;

namespace PrecBlade
{
    public class PrecBlade : Submarine
    {
        public static IEnumerator Register()
        {
            GetAssets();
            Submarine PrecBlade = assets.model.EnsureComponent<PrecBlade>();
            PrecBlade.name = "Blade";
            yield return CoroutineHost.StartCoroutine(VehicleRegistrar.RegisterVehicle(PrecBlade));
            yield break;
        }
        public static VehicleFramework.Assets.VehicleAssets assets;
        public static void GetAssets()
        {
            assets = AssetBundleInterface.GetVehicleAssetsFromBundle("assets/oblade", "blade_vehicle", "SpriteAtlas", "BladePing", "BladeCrafter");
        }
        public override Atlas.Sprite CraftingSprite => VehicleFramework.Assets.SpriteHelper.GetSprite("assets/BladeCrafter.png");
        public override Atlas.Sprite PingSprite => VehicleFramework.Assets.SpriteHelper.GetSprite("assets/BladePing.png");
        public override string vehicleDefaultName
        {
            get
            {
                Language main = Language.main;
                bool flag = !(main != null);
                string result;
                if (flag)
                {
                    result = "Blade";
                }
                else
                {
                    result = main.Get("Blade");
                }
                return result;
            }
        }
        public override List<VehiclePilotSeat> PilotSeats
        {
            get
            {
                var list = new List<VehicleFramework.VehicleParts.VehiclePilotSeat>();
                VehicleFramework.VehicleParts.VehiclePilotSeat vps = new VehicleFramework.VehicleParts.VehiclePilotSeat();
                Transform mainSeat = transform.Find("PilotSeat");
                vps.Seat = mainSeat.gameObject;
                vps.SitLocation = mainSeat.Find("SitLocation").gameObject;
                vps.LeftHandLocation = mainSeat;
                vps.RightHandLocation = mainSeat;
                vps.ExitLocation = mainSeat.Find("ExitLocation");
                list.Add(vps);
                return list;
            }
        }

        public override List<VehicleHatchStruct> Hatches
        {
            get
            {
                var list = new List<VehicleFramework.VehicleParts.VehicleHatchStruct>();

                VehicleFramework.VehicleParts.VehicleHatchStruct interior_vhs = new VehicleFramework.VehicleParts.VehicleHatchStruct();
                Transform intHatch = transform.Find("Hatches/InsideHatch");
                interior_vhs.Hatch = intHatch.gameObject;
                interior_vhs.EntryLocation = intHatch.Find("Entry");
                interior_vhs.ExitLocation = intHatch.Find("Exit");
                interior_vhs.SurfaceExitLocation = intHatch.Find("SurfaceExit");

                VehicleFramework.VehicleParts.VehicleHatchStruct exterior_vhs = new VehicleFramework.VehicleParts.VehicleHatchStruct();
                Transform extHatch = transform.Find("Hatches/OutsideHatch");
                exterior_vhs.Hatch = extHatch.gameObject;
                exterior_vhs.EntryLocation = interior_vhs.EntryLocation;
                exterior_vhs.ExitLocation = interior_vhs.ExitLocation;
                exterior_vhs.SurfaceExitLocation = interior_vhs.SurfaceExitLocation;

                list.Add(interior_vhs);
                list.Add(exterior_vhs);
                return list;
            }
        }

        public override GameObject VehicleModel
        {
            get
            {
                return assets.model;
            }
        }

        public override GameObject CollisionModel
        {
            get
            {
                return transform.Find("Collider").gameObject;
            }
        }
        public override List<GameObject> TetherSources
        {
            get
            {
                var list = new List<GameObject>();
                foreach (Transform child in transform.Find("TetherSources"))
                {
                    list.Add(child.gameObject);
                }
                return list;
            }
        }
        public override GameObject BoundingBox
        {
            get
            {
                return transform.Find("BoundingBoxCollider").gameObject;
            }
        }
        public override GameObject StorageRootObject
        {
            get
            {
                return transform.Find("StorageRoot").gameObject;
            }
        }

        public override GameObject ModulesRootObject
        {
            get
            {
                return transform.Find("ModulesRoot").gameObject;
            }
        }
        public override List<GameObject> WaterClipProxies
        {
            get
            {
                var list = new List<GameObject>();
                foreach (Transform child in transform.Find("WaterClipProxies"))
                {
                    list.Add(child.gameObject);
                }
                return list;
            }
        }
        public override List<VehicleUpgrades> Upgrades
        {
            get
            {
                var list = new List<VehicleFramework.VehicleParts.VehicleUpgrades>();
                VehicleFramework.VehicleParts.VehicleUpgrades vu = new VehicleFramework.VehicleParts.VehicleUpgrades();
                vu.Interface = transform.Find("Upgrades").gameObject;
                vu.Flap = vu.Interface;
                list.Add(vu);
                return list;
            }
        }

        public override List<VehicleBattery> Batteries
        {
            get
            {
                var list = new List<VehicleFramework.VehicleParts.VehicleBattery>();

                VehicleFramework.VehicleParts.VehicleBattery vb1 = new VehicleFramework.VehicleParts.VehicleBattery();
                vb1.BatterySlot = transform.Find("Batteries/Battery1").gameObject;
                list.Add(vb1);

                VehicleFramework.VehicleParts.VehicleBattery vb2 = new VehicleFramework.VehicleParts.VehicleBattery();
                vb2.BatterySlot = transform.Find("Batteries/Battery2").gameObject;
                list.Add(vb2);

                VehicleFramework.VehicleParts.VehicleBattery vb3 = new VehicleFramework.VehicleParts.VehicleBattery();
                vb3.BatterySlot = transform.Find("Batteries/Battery3").gameObject;
                list.Add(vb3);

                VehicleFramework.VehicleParts.VehicleBattery vb4 = new VehicleFramework.VehicleParts.VehicleBattery();
                vb4.BatterySlot = transform.Find("Batteries/Battery4").gameObject;
                list.Add(vb4);
                return list;
            }
        }
        public override List<GameObject> CanopyWindows
        {
            get
            {
                var list = new List<GameObject>();
                //list.Add(transform.Find("Model/LightCanopyOutside_transparent_emissive").gameObject);
                //list.Add(transform.Find("Model/CanopyInside").gameObject);
                return list;
            }
        }
        public override List<VehicleStorage> InnateStorages
        {
            get
            {
                var list = new List<VehicleFramework.VehicleParts.VehicleStorage>();

                Transform innate1 = transform.Find("InnateStorages/Storage1");
                Transform innate2 = transform.Find("InnateStorages/Storage2");

                VehicleFramework.VehicleParts.VehicleStorage IS1 = new VehicleFramework.VehicleParts.VehicleStorage();
                IS1.Container = innate1.gameObject;
                IS1.Height = 8;
                IS1.Width = 8;
                list.Add(IS1);
                VehicleFramework.VehicleParts.VehicleStorage IS2 = new VehicleFramework.VehicleParts.VehicleStorage();
                IS2.Container = innate2.gameObject;
                IS2.Height = 8;
                IS2.Width = 8;
                list.Add(IS2);

                return list;
            }
        }
        public override List<VehicleFloodLight> HeadLights
        {
            get
            {
                var list = new List<VehicleFramework.VehicleParts.VehicleFloodLight>();

                list.Add(new VehicleFramework.VehicleParts.VehicleFloodLight
                {
                    Light = transform.Find("lights_parent/headlights/L").gameObject,
                    Angle = 70,
                    Color = Color.green,
                    Intensity = 1.3f,
                    Range = 90f
                });
                list.Add(new VehicleFramework.VehicleParts.VehicleFloodLight
                {
                    Light = transform.Find("lights_parent/headlights/R").gameObject,
                    Angle = 70,
                    Color = Color.green,
                    Intensity = 1.3f,
                    Range = 90f
                });


                return list;
            }
        }
        public override Dictionary<TechType, int> Recipe
        {

            get
            {
                Dictionary<TechType, int> recipe = new Dictionary<TechType, int>();
                recipe.Add(TechType.TitaniumIngot, 2);
                recipe.Add(TechType.PrecursorIonCrystal, 1);
                recipe.Add(TechType.CopperWire, 4);
                recipe.Add(TechType.AdvancedWiringKit, 2);
                return recipe;

            }
        }
        public override ModVehicleEngine Engine
        {
            get
            {
                return gameObject.EnsureComponent<BladeSubEngine>();

            }

        }
        public override List<Transform> LavaLarvaAttachPoints
        {
            get
            {
                var list = new List<Transform>();
                foreach (Transform child in transform.Find("LavaLarvaAttachPoints"))
                {
                    list.Add(child);
                }
                return list;
            }
        }
        public override Sprite EncyclopediaImage => VehicleFramework.Assets.SpriteHelper.GetSpriteRaw("assets/BladeDatabank.png");
        public override string Description => "Blade Class submarine constructed from technology found on this planet and engineering simular to Precursors";
        public override string EncyclopediaEntry => "Blade Class submarine designed in mind of the alien architecture of precursors.\nThe Materials it is constructed with mimic the Precursor metal found in their buildings which gives it some benefits over regular submersibles.\nThis difference is highly visible after installing the mk3 depth upgrade with a sudden spike in max depth."
;
        public override int BaseCrushDepth => 500;

        public override int CrushDepthUpgrade1 => 400;

        public override int CrushDepthUpgrade2 => 400;

        public override int CrushDepthUpgrade3 => 3500;

        public override int MaxHealth => 10000;

        public override int Mass => 2400;
        public override int NumModules => 6;



    }
}

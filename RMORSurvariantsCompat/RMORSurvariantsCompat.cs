using UnityEngine;
using BepInEx;
using RoR2;
using Survariants;

namespace RMORSurvariantsCompat
{
    [BepInDependency(Survariants.Survariants.PluginGUID, BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency(HANDMod.HandPlugin.MODUID, BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency(RMORMod.RMORPlugin.MODUID, BepInDependency.DependencyFlags.HardDependency)]
    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]
    public class RMORSurvariantsCompat : BaseUnityPlugin
    {
        public const string PluginGUID = PluginAuthor + "." + PluginName;
        public const string PluginAuthor = "LordVGames";
        public const string PluginName = "RMORSurvariantsCompat";
        public const string PluginVersion = "1.0.0";

        public void Start()
        {
            RoR2Application.onLoad += AddRmorAsHandVariant;
        }

        private void AddRmorAsHandVariant()
        {
            SurvivorDef HANDSurvivorDef = SurvivorCatalog.GetSurvivorDef(SurvivorCatalog.GetSurvivorIndexFromBodyIndex(BodyCatalog.FindBodyIndex("HANDOverclockedBody")));
            SurvivorDef RMORSurvivorDef = SurvivorCatalog.GetSurvivorDef(SurvivorCatalog.GetSurvivorIndexFromBodyIndex(BodyCatalog.FindBodyIndex("RMORBody")));
            if (!HANDSurvivorDef || !RMORSurvivorDef)
            {
                Log.Init(Logger);
                Log.Error("Couldn't get HAN-D's and/or R-MOR's SurvivorDefs!");
                return;
            }

            SurvivorVariantDef RMORVariant = ScriptableObject.CreateInstance<SurvivorVariantDef>();
            (RMORVariant as ScriptableObject).name = RMORSurvivorDef.cachedName;
            RMORVariant.name = RMORSurvivorDef.displayNameToken;
            RMORVariant.VariantSurvivor = RMORSurvivorDef;
            RMORVariant.TargetSurvivor = HANDSurvivorDef;
            RMORVariant.RequiredUnlock = UnlockableCatalog.GetUnlockableDef("MoriyaRMORSurvivorUnlock");
            RMORVariant.Description = "MORIYA_RMOR_BODY_SUBTITLE";

            RMORSurvivorDef.hidden = true;
            SurvivorVariantCatalog.AddSurvivorVariant(RMORVariant);
        }
    }
}
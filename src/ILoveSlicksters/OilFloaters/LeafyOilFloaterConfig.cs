﻿using Klei.AI;
using System.Collections.Generic;
using UnityEngine;
using static TUNING.CREATURES;

namespace ILoveSlicksters
{
    public class LeafyOilfloaterConfig : IEntityConfig
    {
        public GameObject CreatePrefab()
        {
            GameObject gameObject = CreateOilfloater(ID, StringsPatch.VARIANT_LEAFY.NAME, StringsPatch.VARIANT_LEAFY.DESC, "custom_oilfloater_anim", false);

            gameObject.AddOrGetDef<CreatureLightMonitor.Def>();

            EntityTemplates.ExtendEntityToFertileCreature(
                gameObject, 
                EGG_ID, 
                StringsPatch.VARIANT_LEAFY.EGG_NAME, 
                StringsPatch.VARIANT_LEAFY.DESC, 
                "egg_oilfloater", 
                OilFloaterTuning.EGG_MASS,
                "LeafyOilfloaterBaby",
                60.0000038f, 20f, 
                EGG_CHANCES_LEAFY, 
                EGG_SORT_ORDER);

            return gameObject;
        }

        public static GameObject CreateOilfloater(string id, string name, string desc, string anim_file, bool is_baby)
        {
            GameObject prefab = BaseOilFloaterConfig.BaseOilFloater(id, name, desc, anim_file, BASE_TRAIT_ID, 263.15f, 313.15f, is_baby, "hot_");
            EntityTemplates.ExtendEntityToWildCreature(prefab, OilFloaterTuning.PEN_SIZE_PER_CREATURE, LIFESPAN.TIER3);
            Trait trait = Db.Get().CreateTrait(BASE_TRAIT_ID, name, name, null, false, null, true, true);
            trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.maxAttribute.Id, OilFloaterTuning.STANDARD_STOMACH_SIZE, name, false, false, true));
            trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.deltaAttribute.Id, -OilFloaterTuning.STANDARD_CALORIES_PER_CYCLE / 600f, name, false, false, true));
            trait.Add(new AttributeModifier(Db.Get().Amounts.HitPoints.maxAttribute.Id, HITPOINTS.TIER1, name, false, false, true));
            trait.Add(new AttributeModifier(Db.Get().Amounts.Age.maxAttribute.Id, LIFESPAN.TIER3, name, false, false, true));
            return BaseOilFloaterConfig.SetupDiet(prefab, CONSUME_ELEMENT.CreateTag(), EMIT_ELEMENT.CreateTag(), CALORIES_PER_KG_OF_ORE, CONVERSION_EFFICIENCY.GOOD_3, null, 0f, MIN_POOP_SIZE_IN_KG);
        }

        public void OnPrefabInit(GameObject inst)
        {
        }

        public void OnSpawn(GameObject inst)
        {
        }

        public static List<FertilityMonitor.BreedingChance> EGG_CHANCES_LEAFY = new List<FertilityMonitor.BreedingChance>
        {
            new FertilityMonitor.BreedingChance
            {
                egg = "LeafyOilfloaterEgg".ToTag(),
                weight = 0.66f
            },
            new FertilityMonitor.BreedingChance
            {
                egg = "OilfloaterEgg".ToTag(),
                weight = 0.34f
            }
        };


        public const string ID = "LeafyOilfloater";

        public const string BASE_TRAIT_ID = "LeafyOilfloaterBaseTrait";

        public const string EGG_ID = "LeafyOilfloaterEgg";

        public const SimHashes CONSUME_ELEMENT = SimHashes.CarbonDioxide;

        public const SimHashes EMIT_ELEMENT = SimHashes.LiquidOxygen;

        private static float KG_ORE_EATEN_PER_CYCLE = PHO_TUNING.OILFLOATER.KG_ORE_EATEN_PER_CYCLE.HIGH;

        private static float CALORIES_PER_KG_OF_ORE = OilFloaterTuning.STANDARD_CALORIES_PER_CYCLE / KG_ORE_EATEN_PER_CYCLE;

        private static float MIN_POOP_SIZE_IN_KG = 0.5f;

        public static int EGG_SORT_ORDER = OilFloaterConfig.EGG_SORT_ORDER + 2;

    }
}

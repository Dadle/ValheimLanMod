using System.Diagnostics;
using BepInEx;
using UnityEngine;
using Jotunn;
using Jotunn.Managers;
using Jotunn.Configs;
using Jotunn.Entities;
using Debug = UnityEngine.Debug;

namespace DadleLanModValheim
{
    [BepInPlugin("com.yourname.helloworld", "Hello World", "1.0.0")]
    public class VanillaCreatureTest : BaseUnityPlugin
    {
        private void Awake()
        {
            // Hook creature manager to get access to vanilla creature prefabs
            CreatureManager.OnVanillaCreaturesAvailable += ModifyAndCloneVanillaCreatures;
            Debug.Log("VanillaCreatureTest: Awake");
        }

        // Modify and clone vanilla creatures
        private void ModifyAndCloneVanillaCreatures()
        {
            // Clone a vanilla creature with and add new spawn information
            var lulzetonConfig = new CreatureConfig();
            lulzetonConfig.AddSpawnConfig(new SpawnConfig
            {
                Name = "Jotunn_SkelSpawn1",
                SpawnChance = 100,
                SpawnInterval = 20f,
                SpawnDistance = 1f,
                Biome = Heightmap.Biome.Meadows,
                MinLevel = 3
            });

            var lulzeton = new CustomCreature("Lulzeton", "Skeleton_NoArcher", lulzetonConfig);
            var lulzoid = lulzeton.Prefab.GetComponent<Humanoid>();
            lulzoid.m_walkSpeed = 0.1f;
            CreatureManager.Instance.AddCreature(lulzeton);

            // Get a vanilla creature prefab and change some values
            var skeleton = CreatureManager.Instance.GetCreaturePrefab("Skeleton_NoArcher");
            var humanoid = skeleton.GetComponent<Humanoid>();
            humanoid.m_walkSpeed = 2;

            // Unregister the hook, modified and cloned creatures are kept over the whole game session
            CreatureManager.OnVanillaCreaturesAvailable -= ModifyAndCloneVanillaCreatures;
        }
    }
}

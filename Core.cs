using System.Collections;
using Il2CppInterop.Runtime;
using MelonLoader;
using UnityEngine;
using UnityEngine.SceneManagement;

[assembly: MelonInfo(typeof(EveryoneHatesYou.EveryoneHatesYou), "EveryoneHatesYou", "1.0.0", "Soul", null)]
[assembly: MelonGame("TVGS", "Schedule I")]

namespace EveryoneHatesYou
{
    public class EveryoneHatesYou : MelonMod
    {

        public override void OnInitializeMelon()
        {

            Melon<EveryoneHatesYou>.Logger.Msg("Initialized!");
            SceneManager.add_sceneLoaded((Action<Scene, LoadSceneMode>)OnSceneLoaded);
        }
        private static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name.Equals("Main"))
            {
                Melon<EveryoneHatesYou>.Logger.Msg("Waiting!");
                MelonCoroutines.Start(Load());
            }

        }

        private static IEnumerator Load()
        {
            for (int i = 0; i < 40; i++)
                yield return new WaitForEndOfFrame();
            if(CheckFirstQuest())
            MakeEveryoneHateYou();
        }


        public static void MakeEveryoneHateYou()
        {
            int npcCount = 0;

            foreach (var npc in UnityEngine.Object.FindObjectsOfType<Il2CppScheduleOne.NPCs.NPC>())
            {
                if (npc == null || npc.RelationData == null) continue;

                npc.RelationData.RelationDelta = 0;
                if (npc.name == 
                npcCount++;
                Melon<EveryoneHatesYou>.Logger.Msg($"Set RelationDelta to 0 (hostile) for NPC: {npc.name}");
            }

            Melon<EveryoneHatesYou>.Logger.Msg($"Everyone hates you now. ({npcCount} NPCs updated)");
        }
        public static bool CheckFirstQuest()
        {
            const string questTitle = "Open your phone and read your messages";

            foreach (var entry in UnityEngine.Object.FindObjectsOfType<Il2CppScheduleOne.Quests.QuestEntry>())
            {
                if (entry?.Title?.Trim() == questTitle)
                {
                    bool shouldClear = entry.state != Il2CppScheduleOne.Quests.EQuestState.Completed;
                    Melon<EveryoneHatesYou>.Logger.Msg($"Quest '{questTitle}' is {entry.state} => shouldClear = {shouldClear}");
                    return shouldClear;
                }
            }

            Melon<EveryoneHatesYou>.Logger.Warning("First quest not found — defaulting to shouldClear = true");
            return true;
        }

        public static void DumpAllQuests()
        {
            var allQuests = UnityEngine.Object.FindObjectsOfType<Il2CppScheduleOne.Quests.QuestEntry>();
            Melon<EveryoneHatesYou>.Logger.Msg($"[QuestDump] Found {allQuests.Length} quest entries:");

            foreach (var entry in allQuests)
            {
                if (entry == null) continue;

                Melon<EveryoneHatesYou>.Logger.Msg($"[QuestDump] Title: '{entry.Title}' | State: {entry.state}");
            }
        }



    }




}
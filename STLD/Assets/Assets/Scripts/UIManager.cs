using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro
public class UIManager : MonoBehaviour
{
    [SerializeField] TMP_Text journalText;
    [SerializeField] GameObject journalPanel;

    // I used claude for all this I'm not writing out 9 different journals
    List<string> journalContents = new List<string>
    {
        // Journal 01 — Captain's Log, Entry 1 (Bridge)
        "CAPTAIN'S LOG — VERATH SYSTEM, DAY 1 POST-CRASH\n\nWe came down hard. The stabilisers gave out somewhere over the upper atmosphere — Lena thinks it was the ion storm that scrambled the nav array. I think it was worse than that, but I'm keeping that to myself for now.\n\nThe ship is in bad shape. Port engine is gone. The hull held in most places, but the med bay took damage and we lost two of the emergency ration caches in the crash.\n\nCrew status:\n  - Dr. Maren Solís (Medic): minor head wound, cleared for duty\n  - Chief Lena Vogt (Engineer): right arm fracture, working anyway\n  - Kofi Asante (Navigator): unaccounted for. Last seen aft section.\n  - Commander Rei Hara (Pilot): unconscious, stable\n\nI am keeping spirits up. That is my job.\n\nWe will repair the ship. We will go home.\n  — Capt. Y. Okafor",

        // Journal 02 — Engineer's Log, Entry 1 (Engine Room)
        "ENGINEERING LOG — DAY 2\n\nWriting this left-handed. The right arm is splinted but it throbs something awful when I try to grip. Doesn't matter. Someone has to assess the damage.\n\nStatus report:\n\n  PORT ENGINE: total loss. Fuel lines ruptured in the crash — there was a fire before the suppression systems kicked in. The whole assembly is slag.\n\n  STARBOARD ENGINE: structurally intact but the navigation chip was ejected during impact. The locking mechanism failed. Without it the engine won't initialise — it's a hardwired safety protocol I cannot override from here.\n\n  THE CHIP: I tracked the ejection trajectory. It should have landed somewhere in the terrain to the east. Past the jungle, maybe. I need someone to go look.\n\n  BLAST DOOR: sealed on emergency lockdown. It will only release once the chip is slotted and the engine reads as active. I can't bypass it.\n\nRei, if you're reading this — the chip is the key. Find it. Bring it back.\n  — Chief Vogt",

        // Journal 03 — Medic's Log, Entry 1 (Med Bay)
        "MEDICAL LOG — DAY 3\n\nKofi never came back.\n\nThe Captain sent me to search the aft section this morning. I found his helmet near the secondary airlock. The airlock was open — had been since the crash, from what I can tell. Atmosphere reads as breathable out there, which is something. But Kofi walked out into that jungle alone and didn't tell anyone.\n\nI found his personal log stuffed behind a panel in the corridor. I don't know if I should tell the Captain what's in it.\n\nMedical notes:\n  - Lena's arm: fracture holding, but she refuses to rest. Stubborn.\n  - Captain Okafor: stress indicators elevated. Hiding it well.\n  - Commander Hara: still unconscious. Vitals stable. I don't know when she'll wake up.\n  - Myself: functional. Scared.\n\nThe creatures outside have been quiet today. Yesterday they weren't.\n  — Dr. M. Solís",

        // Journal 04 — Navigator's Personal Log (Jungle Clearing)
        "KOFI'S LOG — I don't know what day it is anymore\n\nI know how this looks. I know they'll think I panicked.\n\nI didn't panic. I left because I found something in the aft sensor readings before the crash — something the Captain didn't see. Or chose not to see. We weren't pulled off course by the ion storm.\n\nSomething on this planet called us down.\n\nThe signal was in a frequency range we don't use. Old. Deliberate. I've been following it since I left the ship. It gets stronger the further east I go, toward those ruins I can see on the ridge.\n\nThe jungle is... I won't say beautiful. It watches you. There are creatures here that are too smart for animals. They haven't attacked me yet. I think they're deciding.\n\nIf someone finds this — don't follow the signal.\n\nI'm going to keep following it.\n  — Kofi",

        // Journal 05 — Captain's Log, Entry 2 (Jungle, near ruins archway)
        "CAPTAIN'S LOG — DAY 6\n\nI came after Kofi.\n\nLena argued. She said I should stay with the ship, that she and Maren could hold things together. She was right. I came anyway. He's my crew.\n\nI found his camp in the clearing — cold fire, empty rations. He was here recently. He's still heading east.\n\nI can see the ruins from here. Ancient. Massive. Nothing in the survey database. Whatever civilisation built those structures, they were not small.\n\nThe creatures circled my camp last night. I fired a warning shot and they scattered. But they came back. Smarter the second time — they came from different directions.\n\nI'm going to find Kofi and drag him back to the ship by the collar if I have to.\n\nLena, if something happens to me: the chip is east, in the ruins. Rei is the best pilot I've ever served with. Trust her to fly you home.\n  — Capt. Y. Okafor",

        // Journal 06 — Engineer's Log, Entry 2 (Jungle, fire plant zone)
        "ENGINEERING LOG — DAY 8\n\nThe Captain left four days ago. He's not back.\n\nMaren and I are alone on the ship. The creatures have started testing the airlock — pressing against the seal, feeling for weakness. I've reinforced it with what I had. It'll hold. Probably.\n\nI went out today to look for salvage. Didn't go far. The jungle doesn't want you to go far.\n\nI found something disturbing near the fire plants — a boot. Standard-issue fleet. Small. Maren's size. But Maren is on the ship.\n\nIt wasn't Maren's boot. There was a fourth crew member on our manifest that I never met before launch. A specialist. Classified assignment. They were in the aft section during the crash.\n\nI never thought to check.\n\nI'm going back to search the aft section properly.\n  — Lena",

        // Journal 07 — Navigator's Personal Log, Final Entry (Ruins Courtyard)
        "KOFI'S LOG — FINAL\n\nI found the source of the signal.\n\nIt's an altar. Deep in the ruins. Something is on it — a small crystalline chip, clearly not alien in origin. Fleet-issued navigation hardware. Someone brought it here. Someone placed it deliberately.\n\nI don't understand. I don't understand any of it.\n\nThe ruins are not empty. There are creatures here — larger than the ones in the jungle. They don't scatter. They watch and they wait.\n\nI'm going to take the chip. It's what Lena needs to fix the engine.\n\nIf it's a trap, I'm walking into it.\nIf it's a gift, I don't know what we owe for it.\n\nTell my mother I saw something extraordinary.\n\n  — Kofi Asante, Navigator, ISS Meridian\n  — Signing off",

        // Journal 08 — Captain's Log, Final Entry (Inner Sanctum, near altar)
        "CAPTAIN'S LOG — FINAL ENTRY\n\nI found Kofi.\n\nHe didn't make it. The creatures — the large ones that guard this place — he must have triggered them when he took the chip. I found him near the altar. He had put the chip back. I think he did it on purpose. I think he was trying to undo something.\n\nThe ruins are older than I have words for. The carvings on these walls — I've been staring at them for an hour. I'm not a linguist. But you don't need to be. The images tell the story well enough.\n\nSomething came to this planet before us. Something was here, and it left something behind, and the creatures have been guarding it ever since. The signal wasn't a call for help.\n\nIt was a warning. Keep away.\n\nWe didn't listen.\n\nCommander Hara — if you're reading this, it means you woke up. It means there's still time.\n\nTake the chip. Don't linger. Don't look at the carvings too long.\n\nFly straight and don't look back.\n\nI'm sorry I won't be there to see it.\n  — Captain Yusra Okafor, ISS Meridian",

        // Journal 09 — Dr. Solís Audio Log Transcript (Engine Room Terminal, plays on chip insertion)
        "[AUDIO LOG — AUTOMATIC TRANSMISSION — DAY 11]\n[TRIGGERED ON CHIP INSERTION]\n\nRei.\n\nI recorded this and set it to play if anyone ever put that chip in the terminal. I hoped it would be you.\n\nI figured out what the signal was. The ruins, the chip, the creatures — it's a test. It's been a test for every species that followed the signal here. Something built this planet as a filter. I know how that sounds.\n\nThe ones who take the chip and run — they go home. That's the answer. That's what you're supposed to do. Take it and go.\n\nThe Captain stayed. Kofi stayed. They were good people who asked the wrong questions.\n\nI stayed too long trying to understand it.\n\nI don't think I'm going to make it to the ship.\n\nBut the blast door is unlocked now, Rei. The engine is live.\n\nYou always said you could fly anything.\n\nProve it.\n\n[END OF TRANSMISSION]\n[BLAST DOOR: UNLOCKED]"
    };
    public void SceneLoader(string sceneName)
    {
        SceneManager.LoadScene(sceneName); 
    }
    public void Quit()
    {
        // If running in the Unity Editor, stop the Play Mode
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        // If running as a standalone built game, close the application
        Application.Quit();
        #endif
    }
    public void DisplayJournalText(int journalNumber)
    {
        journalPanel.enabled = !journalPanel.activeSelf;
        if (journalPanel.activeSelf)
        {
            journalText.text = journalContents[journalNumber-1];
        }
    }
}

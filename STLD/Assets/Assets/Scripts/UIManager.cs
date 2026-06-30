using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] TMP_Text journalText;
    [SerializeField] GameObject journalPanel;
    [SerializeField] private GameObject MenuPanel;
    [SerializeField] private GameObject DiePanel;
    [SerializeField] private TMP_Text HintText;
    [SerializeField] private Image frontHealthBar;
    [SerializeField] private Image backHealthBar;
    [SerializeField] private Image scoreBar;


    [Header("Variables")] 
    private float lerpTimer = 0;
    [SerializeField] private float chipSpeed = 1f;


    // I used claude for all this I'm not writing out 9 different journals
    List<string> journalContents = new List<string>
    {
        // Journal 01 — Captain's Log, Entry 1 (Bridge)
        "CAPTAIN'S LOG, DAY 1. We came down hard over Verath Prime. Port engine's gone, hull's cracked but holding. Lena's arm is fractured, Maren's got a head wound, Kofi's unaccounted for — last seen heading aft. Rei's still out cold. I'm keeping spirits up. That's my job. We will fix this ship and we will go home.\n  — Capt. Y. Okafor",

        // Journal 02 — Engineer's Log, Entry 1 (Engine Room)
        "ENGINEERING LOG, DAY 2. Writing left-handed, arm's no good. Port engine's slag — fuel line fire before suppression kicked in. Starboard's intact but the nav chip ejected on impact, landed somewhere east past the jungle. Blast door's on lockdown — won't release till the chip's slotted and the engine reads active. Find it, Rei.\n  — Chief Vogt",

        // Journal 03 — Medic's Log, Entry 1 (Med Bay)
        "MEDICAL LOG, DAY 3. Kofi never came back. Found his helmet by the secondary airlock — it's been open since the crash. He walked out into that jungle alone, didn't tell anyone. Found a log of his stuffed behind a panel. Haven't told the Captain what's in it. The creatures outside were active yesterday. Today, dead silent.\n  — Dr. M. Solís",

        // Journal 04 — Navigator's Personal Log (Jungle Clearing)
        "I know how this looks, like I panicked. I didn't. Found something in the sensor logs before we crashed — a signal in a frequency we don't use. Old. Deliberate. It's pulling me east, toward ruins on the ridge, getting stronger the closer I get. The jungle watches you. The creatures haven't attacked — yet. Don't follow the signal.\n  — Kofi",

        // Journal 05 — Captain's Log, Entry 2 (Jungle, near ruins archway)
        "CAPTAIN'S LOG, DAY 6. Came after Kofi against Lena's advice. Found his camp cold, rations empty, still heading east. I can see the ruins now — ancient, massive, nothing in our database. Creatures circled my camp last night, scattered at a warning shot, came back smarter. Lena — the chip's in those ruins. Trust Rei to fly you home.\n  — Capt. Okafor",

        // Journal 06 — Engineer's Log, Entry 2 (Jungle, fire plant zone)
        "ENGINEERING LOG, DAY 8. Captain's been gone four days. Maren and I are alone on the ship, creatures testing the airlock seal nightly. Went out for salvage today, didn't get far — found a boot near the fire plants. Wrong size for Maren. There was a fourth crew member on our manifest I never met before launch. Classified assignment. I never checked the aft section properly. Going back now.\n  — Lena",

        // Journal 07 — Navigator's Personal Log, Final Entry (Ruins Courtyard)
        "Found the source of the signal. An altar, deep in the ruins, with the nav chip resting on it — placed there deliberately, by someone, for some reason I don't understand. The creatures here are bigger than the jungle ones. They don't scatter. They watch and wait. Taking the chip — Lena needs it. If it's a trap, I'm walking in anyway. Tell my mother I saw something extraordinary.\n  — Kofi Asante, Navigator, ISS Meridian",

        // Journal 08 — Captain's Log, Final Entry (Inner Sanctum, near altar)
        "Found Kofi. He didn't make it — the guardian creatures got him near the altar. He'd put the chip back first. I think he meant to. The wall carvings here say it plain enough without translation: something came before us, left something behind, and the signal was never a call for help. It was a warning. We didn't listen. Rei — if you're reading this, take the chip and don't look back. Fly straight home.\n  — Captain Yusra Okafor, ISS Meridian",

        // Journal 09 — Dr. Solís, Final Entry (Med Bay, written near the end)
        "MEDICAL LOG, DAY 11. I'm the only one left on the ship now. I rigged the engine terminal to log this the moment someone slots that chip back in — figured it'd be you, Rei. The ones who go home are the ones who turn back early. The Captain and Kofi stayed and asked too many questions. I should've made them leave. Door should be open once you place it. Go. Don't stop for me.\n  — Dr. Maren Solís"
    };
    public void SceneLoader(string sceneName)
    {
        SceneManager.LoadScene(sceneName); 
    }
    
    public void Quit()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

    public void DisplayJournalText(int journalNumber)
    {
        journalPanel.SetActive(!journalPanel.activeSelf);
        if (journalPanel.activeSelf)
        {
            journalText.text = journalContents[journalNumber-1];
        }
    }

    public void Pause()
    {
        //sets the menu
        MenuPanel.SetActive(!MenuPanel.activeSelf);
        Cursor.visible = MenuPanel.activeSelf;
        Cursor.lockState = MenuPanel.activeSelf ? CursorLockMode.None : CursorLockMode.Locked;
    }

    public void UpdateScore(float score, float maxScore)
    {
        //Changes the score and the xp bar so that the player can say their xp bar is low
        scoreBar.fillAmount = Mathf.Clamp(score / maxScore, 0, 1);
    }

    public void Restart()
    {
        SceneManager.LoadScene("MainLevel");
    }

    public void ShowDiePanel()
    {
        //Lets the player die
        DiePanel.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ShowHintingText(string hintString)
    {
        //Show hints to the player
        HintText.text = hintString;
    }

    public void UpdateHealthUI(float health, float maxHealth)
    {
        //Change the health bar when taking damage
        float fillB = backHealthBar.fillAmount;
        float hFraction = health / maxHealth;
        if(fillB > hFraction)
        {
            frontHealthBar.fillAmount = hFraction;
            backHealthBar.color = Color.red;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            backHealthBar.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplete);
        }
    }

}

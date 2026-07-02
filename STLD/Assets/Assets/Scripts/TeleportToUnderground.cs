using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportToUnderground : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] string sceneName = "Underground";

    private playerCollider playerScript;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerScript = player.GetComponent<playerCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject == player)
        {
            StaticScript.playerScore = playerScript.score;
            StaticScript.journalsCollected = playerScript.journalsCollected;
            SceneManager.LoadScene(sceneName);

        }
    }
}

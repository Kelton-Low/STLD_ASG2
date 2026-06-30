using UnityEngine;

public class Hazard : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int damage;
    [SerializeField] private float howLongBetweenDamage = 0.1f;

    [Header("References")]
    [SerializeField] private GameObject player;

    /// <summary>Tracks time elapsed since last damage tick to enforce damage cooldown.</summary>
    private float timeBetweenDamage = 0;

    /// <summary>Reference to the player's collider script to apply damage to health.</summary>
    private playerCollider playerScript;

    void Start()
    {
        playerScript = player.GetComponent<playerCollider>();
    }


    //Damages player upon first contact with collider
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject == player)
        {
            timeBetweenDamage = 0;
            playerScript.DamagePlayer(damage);
        }
    }

    //Damages player when staying inside the hazard
    void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject == player)
        {
            timeBetweenDamage += Time.deltaTime;
            if (timeBetweenDamage >= howLongBetweenDamage)
            {
                timeBetweenDamage = 0;
                playerScript.DamagePlayer(damage);
            }
        }
    }

    
}

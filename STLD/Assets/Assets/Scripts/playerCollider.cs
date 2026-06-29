using System;
using System.Collections;
using UnityEngine;

public class playerCollider : MonoBehaviour
{
    /// <summary>
    /// Handles player interactions including collecting items, opening doors, and shooting crystals.
    /// </summary>

    [Header("Masks")]
    [SerializeField] private LayerMask CollectibleMask;
    [SerializeField] private LayerMask DoorMask;
    [SerializeField] private LayerMask CrystalMask;

    [Header("Interaction")]
    [SerializeField] private GameObject Camera;
    [SerializeField] private float MaxDistanceInteraction;


    [Header("UI")]
    [SerializeField] private UIManager myUIManager;
    [SerializeField] private int maxScore;

    [Header("Audio")]
    [SerializeField] private AudioSource collectSound;
    [SerializeField] private AudioSource doorSound;

    [Header("Stats")]
    public int playerHealth;
    private float maxHealth;
    private int score = 0;




    void Start()
    {
        myUIManager.UpdateScore(score, maxScore);
        maxHealth = playerHealth;

    }

    // Update is called once per frame
    void Update()
    {
        myUIManager.UpdateHealthUI(playerHealth, maxHealth);
    }

    void OnInteract()
    {
        print("interacting");
        //Check for collectible
        if (Physics.Raycast(Camera.transform.position, Camera.transform.forward, out RaycastHit hit, MaxDistanceInteraction, CollectibleMask))
        {
            CoinValue coinValue = hit.collider.GetComponent<CoinValue>();

            Destroy(hit.collider.gameObject);
            collectSound.Play();

            score += coinValue.Value;
            print("Score: "+ score);
        }
        //Check for door
        if (Physics.Raycast(Camera.transform.position, Camera.transform.forward, out RaycastHit hitDoor, 10, DoorMask))
        {
            print("Found Door");
            
        }
    }
    void OnShoot()
    {
        print("shooting");
    }
    void OnMenu()
    {
        myUIManager.Pause();
    }

    void OnDrawGizmos()
    {
        //Used to see my raycast range
        Gizmos.color = new Color(1, 0, 0, 0.1f);

        Gizmos.DrawSphere(Camera.gameObject.transform.position, MaxDistanceInteraction);
    }

    public void DamagePlayer(int damageAmount)
    {
        playerHealth -= damageAmount;
        if (playerHealth <= 0)
        {
            myUIManager.ShowDiePanel();
        }
    }
}

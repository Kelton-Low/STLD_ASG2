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
    [SerializeField] private LayerMask playerMask;

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
    [SerializeField] int gunDamage = 1;

    private LineRenderer lineRenderer;
    [SerializeField] private GameObject eyeLine;



    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
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
            Collectible collectible = hit.collider.GetComponent<Collectible>();
            
            Destroy(hit.collider.gameObject);
            if (collectible.isJournal)
            {
                myUIManager.DisplayJournalText(collectible.journalValue);
            }
            else
            {
                collectSound.Play();
            }
            

            
            score += collectible.value;
            print("Score: "+ score);
        }
        
    }
    void OnShoot()
    {
        myUIManager.DisplayJournalText(1);
        if (Physics.Raycast(Camera.transform.position, Camera.transform.forward, out RaycastHit hit, 100f, ~playerMask))
        {
            DrawLine(eyeLine.transform.position, hit.point);
            if (hit.collider != null && hit.collider.gameObject.CompareTag("enemy"))
            {
                SharkEnemy sharkScript = hit.collider.gameObject.GetComponent<SharkEnemy>();
                sharkScript.DamageShark(gunDamage);
            }
        }
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
    void DrawLine(Vector3 start, Vector3 end)
    {

        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);

        StopCoroutine("HideLine");
        StartCoroutine("HideLine");

    }
    IEnumerator HideLine()
    {
        yield return new WaitForSeconds(0.1f);
        lineRenderer.enabled = false;
    }
}

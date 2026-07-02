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
    [SerializeField] private LayerMask ignoreRaycastMask;
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private LayerMask doorMask;

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
    public int score = 0;
    [SerializeField] int gunDamage = 1;

    private LineRenderer lineRenderer;
    [SerializeField] private GameObject eyeLine;
    private MeshRenderer lastOutlineRenderer;
    public int journalsCollected = 0;



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
        myUIManager.UpdateScore(score, maxScore);
        EnableOutline();
    }

    void OnInteract()
    {
        myUIManager.CloseJournal();
        print("interacting");
        //Check for collectible
        if (Physics.Raycast(Camera.transform.position, Camera.transform.forward, out RaycastHit hit, MaxDistanceInteraction, CollectibleMask))
        {
            Collectible collectible = hit.collider.GetComponent<Collectible>();
            
            Destroy(hit.collider.gameObject);
            if (collectible.isJournal)
            {
                myUIManager.DisplayJournalText(collectible.journalValue);
                journalsCollected += 1;
            }
            else
            {
                score += collectible.value;
                collectSound.Play();
            }
            

            
            score += collectible.value;
            print("Score: "+ score);
        }
        
    }
    void OnShoot()
    {
        if (Physics.Raycast(Camera.transform.position, Camera.transform.forward, out RaycastHit hit, 100f, ~(playerMask | ignoreRaycastMask)))
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
        myUIManager.CloseJournal();
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
    
    void EnableOutline()
    {
        if (Physics.Raycast(Camera.transform.position, Camera.transform.forward, out RaycastHit hit, MaxDistanceInteraction, CollectibleMask))
        {
            MeshRenderer[] renderers = hit.collider.gameObject.GetComponentsInChildren<MeshRenderer>();
            MeshRenderer outlineRenderer = renderers[1];
            print(hit.collider.gameObject.name);
            if (lastOutlineRenderer != null && lastOutlineRenderer != outlineRenderer)
            {
                lastOutlineRenderer.enabled = false;
            }

            outlineRenderer.enabled = true;
            lastOutlineRenderer = outlineRenderer;
        }
        else
        {
            // Not hovering over anything, disable the last one
            if (lastOutlineRenderer != null)
            {
                lastOutlineRenderer.enabled = false;
                lastOutlineRenderer = null;
            }
        }
    }

    bool CheckToOpenDoor()
    {
        return Physics.Raycast(Camera.transform.position, Camera.transform.forward, MaxDistanceInteraction, doorMask)
            && score >= maxScore 
            && journalsCollected >= 9;
    }
}

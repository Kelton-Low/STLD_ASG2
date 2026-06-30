using System;
using Unity.VisualScripting;
using UnityEngine;


public class SharkEnemy : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject oil;
    [SerializeField] int sharkHealth;
    [SerializeField] int playerDamage;
    private playerCollider playerScript;
    [SerializeField] float timeBetweenDamage;
    float damageTimer;
    [SerializeField] float chaseSpeed = 1;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerScript = player.GetComponent<playerCollider>();
    }
    

    // Update is called once per frame
    void Update()
    {
        FlyToPlayer();
    }
    void OnTriggerEnter()
    {
        damageTimer = 0;
        playerScript.DamagePlayer(playerDamage);
    }
    void OnTriggerStay()
    {
        print("staying");
        if(damageTimer < timeBetweenDamage)
        {
            damageTimer += Time.deltaTime;
        }
        else
        {
            playerScript.DamagePlayer(playerDamage);
            damageTimer = 0;
            print("Taking damage from shark");
        }
    }


    void FlyToPlayer()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        Vector3 newPosition = transform.position + direction * chaseSpeed * Time.deltaTime;
        transform.LookAt(new Vector3(
            player.transform.position.x,
            transform.position.y,
            player.transform.position.z
        ));

        transform.Rotate(90f, 0f, 0f);
        transform.position = newPosition;
    }
    void SharkDie()
    {
        Instantiate(oil, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    public void DamageShark(int damage)
    {
        sharkHealth -= damage;
        if (sharkHealth <= 0)
        {
            SharkDie();
        }
    }

}

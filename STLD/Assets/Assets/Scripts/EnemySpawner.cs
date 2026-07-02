using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject enemy;
    [SerializeField] int spawnCount = 5;
    [SerializeField] private float minScale = 1f;
    [SerializeField] private float maxScale = 5f;
    private Collider cubeCollider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cubeCollider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject == player)
        {
            SpawnRandom();
        }
    }
    

    void SpawnRandom()
    {
        Bounds bounds = cubeCollider.bounds;
        for (int i = 0; i < spawnCount; i++)
        {
            Vector3 randomPosition = new Vector3(
                Random.Range(bounds.min.x, bounds.max.x),
                Random.Range(bounds.min.y, bounds.max.y),
                Random.Range(bounds.min.z, bounds.max.z)
            );

            GameObject spawnedObject = Instantiate(enemy, randomPosition, Quaternion.identity);

            float randomScale = Random.Range(minScale, maxScale);
            spawnedObject.transform.localScale = new Vector3(randomScale, randomScale, randomScale);
            spawnedObject.SetActive(true);
        }
    }
}

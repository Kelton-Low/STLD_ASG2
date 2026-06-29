using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] CharacterController player;

    [SerializeField] float moveSpeed;

    [SerializeField] float obstacleCheckDist;

    CharacterController self;

    /// <summary>
    /// Player's position
    /// </summary>
    Vector3 playerPos;

    /// <summary>
    /// Current position
    /// </summary>
    Vector3 currentPos;

    /// <summary>
    /// Direction needed to move in
    /// </summary>
    Vector3 moveDir;

    /// <summary>
    /// Direction needed to move in if theres a wall
    /// </summary>
    Vector3 avoidDir;
    
    /// <summary>
    /// Stores raycast hit data
    /// </summary>
    RaycastHit obstacleHit;

    /// <summary>
    /// Whether there is an obstacle in front
    /// </summary>
    bool obstacle;

    /// <summary>
    /// Whether the navigation is still ongoing
    /// </summary>
    bool isNavigating;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        self = GetComponent<CharacterController>();
    }

    void Update()
    {
        FindMoveDir();
        RotateToMoveDir();
        Move();
    }

    /// <summary>
    /// Find the direction needed to move in
    /// </summary>
    void FindMoveDir()
    {
        // Set the player's position
        playerPos = new Vector3(player.transform.position.x, 0 , player.transform.position.z);

        // Set the current position
        currentPos = new Vector3(self.transform.position.x, 0 , self.transform.position.z);

        moveDir = (playerPos - currentPos).normalized;
    }
    
    // Rotate to the direction needed to move
    void RotateToMoveDir()
    {
        obstacle = Physics.Raycast(self.transform.position, moveDir, out obstacleHit, obstacleCheckDist);

        if (obstacle)
        {
            NavigateObstacle();
        }
        else
        {
            isNavigating = false;

            transform.rotation = Quaternion.LookRotation(moveDir);
        }
    }

    /// <summary>
    /// Move in the move direction
    /// </summary>
    void Move()
    {
        self.Move(self.transform.forward * moveSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Navigate past an obstacle
    /// </summary>
    void NavigateObstacle()
    {   
        if (isNavigating)
        {
            return;
        }

        Vector3 wallRight = Vector3.Cross(Vector3.up, obstacleHit.normal).normalized;
        Vector3 wallLeft = -wallRight;

        float right = Vector3.Dot(wallRight, moveDir);
        float left = Vector3.Dot(wallLeft, moveDir);

        if (right >= left)
        {
            transform.rotation = Quaternion.LookRotation(wallRight);

            isNavigating = true;
        }
        else
        {
            transform.rotation = Quaternion.LookRotation(wallLeft);

            isNavigating = true;
        }
    }
}

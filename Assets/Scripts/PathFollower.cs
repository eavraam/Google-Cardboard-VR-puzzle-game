using UnityEngine;
using PathCreation;

// Moves along a path at constant speed.
// Depending on the end of path instruction, will either loop, reverse, or stop at the end of the path.
public class PathFollower : MonoBehaviour
{
    public PathCreator firstPathCreator;
    public PathCreator secondPathCreator;
    private PathCreator pathCreator;
    public EndOfPathInstruction endOfPathInstruction;
    public float speed = 5;
    float distanceTravelled;

    public bool firstObstacleResolved = false;
    public bool secondObstacleResolved = false;
    public bool thirdObstacleResolved = false;
    public bool fourthObstacleResolved = false;
    
    float firstObstacleDistance = 5.6f;
    float secondObstacleDistance = 93.0f;
    float thirdObstacleDistance = 63.6f;
    float fourthObstacleDistance = 145.0f;
    float gameOverScreenDistance = 150.0f;

    public GameOverScreen gameOverScreen;

    public delegate void ChangeEvent();

    void Start() {
        if (firstPathCreator != null)
        {
            // Subscribed to the pathUpdated event so that we're notified if the path changes during the game
            pathCreator = firstPathCreator;
            pathCreator.pathUpdated += OnPathChanged;
            SecondObstacleController.platformUpEvent += OnPlatformUp;
        }
    }

    void Update()
    {
        //Debug.Log(distanceTravelled);

        if (pathCreator != null)
        {
            if ((distanceTravelled < firstObstacleDistance) || //Before the first obstacle
            ((distanceTravelled < secondObstacleDistance) && firstObstacleResolved && !secondObstacleResolved) || //Before the second obstacle
            ((distanceTravelled < thirdObstacleDistance) && secondObstacleResolved && !thirdObstacleResolved) || //Before the third obstacle
            ((distanceTravelled < fourthObstacleDistance) && thirdObstacleResolved && !fourthObstacleResolved) || //Before the fourth obstacle
            fourthObstacleResolved) //After the fourth obstacle
            {
                distanceTravelled += speed * Time.deltaTime;
                transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction) + new Vector3(0.0f, 0.5f, 0.0f);
                transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
            }
        }

        if (distanceTravelled > gameOverScreenDistance && firstObstacleResolved && secondObstacleResolved && thirdObstacleResolved && fourthObstacleResolved)
        {
            gameOverScreen.gameObject.SetActive(true);
        }

    }

    //When the platform comes up this event will set as true the second obstacle variable, so the ball will continue it's path
    void OnPlatformUp()
    {
        secondObstacleResolved = true;
        pathCreator = secondPathCreator;
        distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
    }

    // If the path changes during the game, update the distance travelled so that the follower's position on the new path
    // is as close as possible to its position on the old path
    void OnPathChanged() {
        distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
    }
}
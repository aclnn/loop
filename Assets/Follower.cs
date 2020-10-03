using UnityEngine;
using PathCreation;
using UnityEngine.Events;

public class Follower : MonoBehaviour
{
    public PathCreator pathCreator;
    public float speed = 5;
    private float distanceTravelled;
    private float currentLapDistance;
    [SerializeField] private Vector3 transformOffset;

    public UnityEvent lapDone = new UnityEvent();

    private bool gameStarted = false;
    private bool canMove = true;

    public bool CanMove
    {
        get => canMove;
        set => canMove = value;
    }

    public bool GameStarted
    {
        get => gameStarted;
        set => gameStarted = value;
    }

    public void AddSpeed(float add)
    {
        speed += add;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            distanceTravelled += speed * Time.deltaTime;
            transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled) + transformOffset;
            transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled);
        }
        //J'ai suivi le tuto pour faire ça
        
        if (gameStarted)
        {
            currentLapDistance += speed * Time.deltaTime;
        }
        
        if (currentLapDistance > pathCreator.path.length)
        {
            Debug.Log("LAP DONE");
            currentLapDistance = 0;
            lapDone.Invoke();
        }
    }
}

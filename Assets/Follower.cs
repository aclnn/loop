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
    
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //J'ai suivi le tuto pour faire ça
        distanceTravelled += speed * Time.deltaTime;
        currentLapDistance += speed * Time.deltaTime;
        transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled) + transformOffset;
        transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled);

        Debug.Log($"currentLapDistance = {currentLapDistance} | pathLength = {pathCreator.path.length}");
        
        if (currentLapDistance > pathCreator.path.length)
        {
            currentLapDistance = 0;
            lapDone.Invoke();
        }
    }
}

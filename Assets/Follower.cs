using UnityEngine;
using PathCreation;

public class Follower : MonoBehaviour
{
    public PathCreator pathCreator;
    public float speed = 5;
    private float distanceTravelled;
    [SerializeField] private Vector3 transformOffset;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //J'ai suivi le tuto pour faire ça
        distanceTravelled += speed * Time.deltaTime;
        transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled) + transformOffset;
        transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled);

    }
}

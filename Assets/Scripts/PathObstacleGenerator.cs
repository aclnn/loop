using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using PathCreation;
using UnityEngine;

[RequireComponent(typeof(PathCreator))]
public class PathObstacleGenerator : MonoBehaviour
{
    [SerializeField] private GameObject obstaclePoint;
    public PathCreator pathCreator;
    //Si c'est trop bas le drawgizmos va vouloir draw trop de sphere et ça crash/répond dans longtemps (genre 0.0003)
    [SerializeField] private float obstacleTileLength = 1f;
    [SerializeField] private Vector3 obstacleOffset;

    [SerializeField] private int startingObstacles = 100;

    [SerializeField] private Follower follower;
    
    private List<GameObject> obstaclePoints = new List<GameObject>();
    private List<GameObject> availableObstaclePoints = new List<GameObject>();

    public bool half = true;

    public bool Half
    {
        get => half;
        set => half = value;
    }

    //Dans l'inspector -> les 3 petits points ça ajoute un bouton pour lancer la fonction
    [ContextMenu("Start")]
    void Start()
    {
        pathCreator = GetComponent<PathCreator>();
    }

    public void InitializeObstacle()
    {
        InitObstaclePos();

        availableObstaclePoints = obstaclePoints;


        for (int i = 0; i < startingObstacles*2; i++)
        {
            int random = Random.Range(0, availableObstaclePoints.Count - 1);
            
            int posRandom = Random.Range(1, 6);

            availableObstaclePoints[random].GetComponent<ObstacleBehaviour>().SpawnObstacle(posRandom);
            
            RefreshAvailableObstaclePoints();
        }
    }
    
    public void DestroyAllObstacles()
    {
        foreach (var point in GameObject.FindGameObjectsWithTag("ObstacleTag"))
        {
            DestroyImmediate(point);
        }
        obstaclePoints.Clear();
        availableObstaclePoints.Clear();
        half = true;
    }
    
    private void RefreshAvailableObstaclePoints()
    {
        for (int i = 0; i < availableObstaclePoints.Count; i++)
        {
            if (availableObstaclePoints[i].transform.childCount >= 4)
            {
                availableObstaclePoints.Remove(availableObstaclePoints[i]);
            }
        }
    }

    public void CallSpawnRandomObstacles(int quantity)
    {
        StartCoroutine(SpawnRandomObstacles(quantity));
    }
    
    private IEnumerator SpawnRandomObstacles(int quantity)
    {
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < quantity; i++)
        {
            int random;
            if (half)
            {
                random = Random.Range(0, availableObstaclePoints.Count/2 - 1);
            }
            else
            {
                random = Random.Range((availableObstaclePoints.Count/2), availableObstaclePoints.Count);
            }
            
            int posRandom = Random.Range(1, 6);

            availableObstaclePoints[random].GetComponent<ObstacleBehaviour>().SpawnObstacle(posRandom);
            
            RefreshAvailableObstaclePoints();
        }
        
        Debug.Break();
        half = !half;
    }

    private void InitObstaclePos()
    {
        obstaclePoints.Clear();
        
        for (float i = 0; i < pathCreator.path.length - 1; i += obstacleTileLength)
        {
            var instantiatedObj = Instantiate(obstaclePoint);
            instantiatedObj.transform.position = pathCreator.path.GetPointAtDistance(i + follower.DistanceTravelled) + obstacleOffset;
            instantiatedObj.transform.localRotation = pathCreator.path.GetRotationAtDistance(i + follower.DistanceTravelled);
            obstaclePoints.Add(instantiatedObj);
        }
    }
    
    [ContextMenu("SpawnObstacles")]
    private void SpawnObstacles()
    {
        foreach (var point in obstaclePoints)
        {
            point.GetComponent<ObstacleBehaviour>().SpawnObstacle(ObstacleEnum.FAR_LEFT);
            point.GetComponent<ObstacleBehaviour>().SpawnObstacle(ObstacleEnum.LEFT);
            point.GetComponent<ObstacleBehaviour>().SpawnObstacle(ObstacleEnum.MIDDLE);
            point.GetComponent<ObstacleBehaviour>().SpawnObstacle(ObstacleEnum.RIGHT);
            point.GetComponent<ObstacleBehaviour>().SpawnObstacle(ObstacleEnum.FAR_RIGHT);
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        
        for (int i = 0; i < obstaclePoints.Count; i++)
        {
            Gizmos.DrawWireSphere(obstaclePoints[i].transform.position, 1f);
        }
    }
#endif
    
}

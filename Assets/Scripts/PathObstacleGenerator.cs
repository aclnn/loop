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
    
    private List<GameObject> obstaclePoints = new List<GameObject>();
    private List<GameObject> availableObstaclePoints = new List<GameObject>();
    
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
        
        SpawnRandomObstacles(startingObstacles);
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
    
    public void SpawnRandomObstacles(int quantity)
    {
        for (int i = 0; i < quantity; i++)
        {
            int random = Random.Range(0, availableObstaclePoints.Count);
            int posRandom = Random.Range(1, 6);

            availableObstaclePoints[random].GetComponent<ObstacleBehaviour>().SpawnObstacle(posRandom);

            RefreshAvailableObstaclePoints();
        }
    }

    private void InitObstaclePos()
    {
        obstaclePoints.Clear();
        
        for (float i = 0; i < pathCreator.path.length; i += obstacleTileLength)
        {
            //pathCreator.path.GetPointAtTime(i) récupère les points du chemin en fonction du temps, de 0 à 1
            var instantiatedObj = Instantiate(obstaclePoint);
            instantiatedObj.transform.position = pathCreator.path.GetPointAtDistance(i) + obstacleOffset;
            instantiatedObj.transform.localRotation = new Quaternion(
                pathCreator.path.GetRotationAtDistance(i).x, 
                pathCreator.path.GetRotationAtDistance(i).y,
                pathCreator.path.GetRotationAtDistance(i).z,
                pathCreator.path.GetRotationAtDistance(i).w);
            obstaclePoints.Add(instantiatedObj);
        }
    }
    
    [ContextMenu("SpawnObstacles")]
    private void SpawnObstacles()
    {
        foreach (var point in obstaclePoints)
        {
            point.GetComponent<ObstacleBehaviour>().SpawnObstacle(ObstacleEnum.LEFT);
            point.GetComponent<ObstacleBehaviour>().SpawnObstacle(ObstacleEnum.MIDDLE);
            point.GetComponent<ObstacleBehaviour>().SpawnObstacle(ObstacleEnum.RIGHT);
        }
    }

    [ContextMenu("DestroyObstacles")]
    private void DestroyObstacles()
    {
        foreach (var point in obstaclePoints)
        {
            point.GetComponent<ObstacleBehaviour>().DestroyObstacles();
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

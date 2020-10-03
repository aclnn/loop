using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class ObstacleBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject obstaclePrefab;
    [SerializeField] private float obsOffset = 3.8f;
    private GameObject tempoObstacle;

    private GameObject leftObstacle;
    private GameObject middleObstacle;
    private GameObject rightObstacle;
    
    private ObstacleEnum obstacleEnum;
    
    public void SpawnObstacle(ObstacleEnum pos)
    {
        if (pos == ObstacleEnum.LEFT && leftObstacle == null)
        {
            leftObstacle = Instantiate(obstaclePrefab, transform);
            leftObstacle.transform.Rotate(0, 90, 0);
        } 
        else if (pos == ObstacleEnum.MIDDLE && middleObstacle == null)
        {
            middleObstacle = Instantiate(obstaclePrefab, transform);
            middleObstacle.transform.localPosition += new Vector3(-obsOffset, 0, 0);
            middleObstacle.transform.Rotate(0, 90, 0);
        }
        else if (pos == ObstacleEnum.RIGHT && rightObstacle == null)
        {
            rightObstacle = Instantiate(obstaclePrefab, transform);
            rightObstacle.transform.localPosition += new Vector3(obsOffset, 0, 0);
            rightObstacle.transform.Rotate(0, 90, 0);
        }
    }

    public void SpawnObstacle(int pos)
    {
        if (pos == 1)
        {
            SpawnObstacle(ObstacleEnum.LEFT);
        }
        else if (pos == 2)
        {
            SpawnObstacle(ObstacleEnum.MIDDLE);
        }
        else if (pos == 3)
        {
            SpawnObstacle(ObstacleEnum.RIGHT);
        }
    }

    public void DestroyObstacles()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            DestroyImmediate(transform.GetChild(i));
        }
    }
}

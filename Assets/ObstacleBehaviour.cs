using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class ObstacleBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject obstaclePrefab;
    [SerializeField] private float obsOffset = 3.8f;

    private GameObject tempoObstacle;

    private GameObject farLeftObstacle;
    private GameObject leftObstacle;
    private GameObject middleObstacle;
    private GameObject rightObstacle;
    private GameObject farRightObstacle;
    
    private ObstacleEnum obstacleEnum;
    
    public void SpawnObstacle(ObstacleEnum pos)
    {
        if (pos == ObstacleEnum.FAR_LEFT && farLeftObstacle == null)
        {
            farLeftObstacle = Instantiate(obstaclePrefab, transform);
            farLeftObstacle.transform.localPosition += new Vector3(-(obsOffset*2), 0, 0);
            farLeftObstacle.transform.Rotate(0, 90, 0);
        } 
        else if (pos == ObstacleEnum.LEFT && leftObstacle == null)
        {
            leftObstacle = Instantiate(obstaclePrefab, transform);
            leftObstacle.transform.localPosition += new Vector3(-obsOffset, 0, 0);
            leftObstacle.transform.Rotate(0, 90, 0);
        }
        else if (pos == ObstacleEnum.MIDDLE && middleObstacle == null)
        {
            middleObstacle = Instantiate(obstaclePrefab, transform);
            middleObstacle.transform.Rotate(0, 90, 0);
        }
        else if (pos == ObstacleEnum.RIGHT && rightObstacle == null)
        {
            rightObstacle = Instantiate(obstaclePrefab, transform);
            rightObstacle.transform.localPosition += new Vector3(obsOffset, 0, 0);
            rightObstacle.transform.Rotate(0, 90, 0);
        }
        else if (pos == ObstacleEnum.FAR_RIGHT && farRightObstacle == null)
        {
            farRightObstacle = Instantiate(obstaclePrefab, transform);
            farRightObstacle.transform.localPosition += new Vector3(obsOffset*2, 0, 0);
            farRightObstacle.transform.Rotate(0, 90, 0);
        }
    }

    public void SpawnObstacle(int pos)
    {
        if (pos == 1)
        {
            SpawnObstacle(ObstacleEnum.FAR_LEFT);
        }
        else if (pos == 2)
        {
            SpawnObstacle(ObstacleEnum.LEFT);
        }
        else if (pos == 3)
        {
            SpawnObstacle(ObstacleEnum.MIDDLE);
        }
        else if (pos == 4)
        {
            SpawnObstacle(ObstacleEnum.RIGHT);
        }
        else if (pos == 5)
        {
            SpawnObstacle(ObstacleEnum.FAR_RIGHT);
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

using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class ObstacleBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject obstaclePrefab;
    [SerializeField] private float obsOffset = 3.8f;
    private GameObject tempoObstacle;
    private ObstacleEnum obstacleEnum;
    
    public void SpawnObstacle(ObstacleEnum pos)
    {
        if (pos == ObstacleEnum.LEFT)
        {
            tempoObstacle = Instantiate(obstaclePrefab, transform);
            tempoObstacle.transform.Rotate(0, 90, 0);
        } 
        else if (pos == ObstacleEnum.MIDDLE)
        {
            tempoObstacle = Instantiate(obstaclePrefab, transform);
            tempoObstacle.transform.localPosition += new Vector3(-obsOffset, 0, 0);
            tempoObstacle.transform.Rotate(0, 90, 0);
        }
        else
        {
            tempoObstacle = Instantiate(obstaclePrefab, transform);
            tempoObstacle.transform.localPosition += new Vector3(obsOffset, 0, 0);
            tempoObstacle.transform.Rotate(0, 90, 0);
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

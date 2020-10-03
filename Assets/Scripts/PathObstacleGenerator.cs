using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using PathCreation;
using UnityEngine;

[RequireComponent(typeof(PathCreator))]
public class PathObstacleGenerator : MonoBehaviour
{
    public PathCreator pathCreator;
    //Si c'est trop bas le drawgizmos va vouloir draw trop de sphere et ça crash/répond dans longtemps (genre 0.0003)
    [SerializeField] [Range(0.001f, 0.1f)] private float obstacleTileLength = 0.02f;

    private List<Vector3> obstaclePoints = new List<Vector3>();
    
    //Dans l'inspector -> les 3 petits points ça ajoute un bouton pour lancer la fonction
    [ContextMenu("Start")]
    void Start()
    {
        pathCreator = GetComponent<PathCreator>();
        
        obstaclePoints.Clear();
        
        for (float i = 0; i < 1; i += obstacleTileLength)
        {
            //pathCreator.path.GetPointAtTime(i) récupère les points du chemin en fonction du temps, de 0 à 1
            obstaclePoints.Add(pathCreator.path.GetPointAtTime(i));
        }
    }
    void Update()
    {
        pathCreator.path.GetPointAtTime(obstacleTileLength);
    }

    private void SpawnObstacles()
    {
        
    }
    
    
    #if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        
        obstaclePoints.Clear();

        for (float i = 0; i < 1; i += obstacleTileLength)
        {
            obstaclePoints.Add(pathCreator.path.GetPointAtTime(i));
        }
        
        Gizmos.color = Color.red;
        foreach (Vector3 point in obstaclePoints)
        {
            Gizmos.DrawWireSphere(point, 1f);
        }
    }
    #endif
}

using System;
using System.Collections;
using System.Collections.Generic;
using PathCreation.Examples;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float sensivity = 0.2f;
    [SerializeField] private float clampOffset = 1f;
    
    public RoadMeshCreator roadMeshCreator;
    public bool movementEnabled = false;
    
    public bool MovementEnabled
    {
        get => movementEnabled;
        set => movementEnabled = value;
    }


    // Update is called once per frame
    void Update()
    {
        if (movementEnabled)
        {
            Vector3 position = transform.localPosition + new Vector3(Input.GetAxis("Mouse X") * sensivity, 0, 0);
            
            position.x = Mathf.Clamp(position.x, -roadMeshCreator.roadWidth + clampOffset, roadMeshCreator.roadWidth - clampOffset);
            transform.localPosition = position;
        }
    }
}

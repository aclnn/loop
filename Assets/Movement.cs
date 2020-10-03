using System;
using System.Collections;
using System.Collections.Generic;
using PathCreation.Examples;
using UnityEngine;
using UnityEngine.Assertions.Comparers;

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

            TiltShip();
        }
    }

    void TiltShip()
    {
        float shipXAngle = transform.localEulerAngles.x;
        shipXAngle = shipXAngle > 180 ? shipXAngle - 360 : shipXAngle;
        
        float tiltAngle = Mathf.Clamp(Input.GetAxis("Mouse X") * 2, -1F, 1F) * -30;
        float smoothTiltAngle = Mathf.Lerp(shipXAngle, tiltAngle, .1F);
        
        transform.localEulerAngles = new Vector3(smoothTiltAngle, -90, 0);
    }
}

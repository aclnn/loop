using System.Collections;
using System.Collections.Generic;
using PathCreation.Examples;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float sensivity = 0.2f;
    [SerializeField] private float maxOffset = 1f;
    public RoadMeshCreator roadMeshCreator;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
            Vector3 position = transform.localPosition + new Vector3(Input.GetAxis("Mouse X") * sensivity, 0, 0);
            
            position.x = Mathf.Clamp(position.x, -roadMeshCreator.roadWidth, roadMeshCreator.roadWidth);

            transform.localPosition = position;
    }
}

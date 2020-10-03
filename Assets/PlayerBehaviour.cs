using System;
using System.Collections;
using System.Collections.Generic;
using PathCreation;
using UnityEngine;
using UnityEngine.Events;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private LayerMask obstacleLayer;
    [SerializeField] private GameObject explosion;
    public UnityEvent collide;
    private Collider collider;

    private void Start()
    {
        collider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("COLLIDE");

        if (obstacleLayer == (obstacleLayer | (1 << other.gameObject.layer)))
        {
            collide.Invoke();
        }
    }

    public void ExplodeShip()
    {
        for (int i = 0; i < 2; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        Instantiate(explosion, transform.position, transform.rotation, transform.parent);
        collider.enabled = false;
    }

    public void RepairShip()
    {
        for (int i = 0; i < 2; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
        collider.enabled = true;
    }
}

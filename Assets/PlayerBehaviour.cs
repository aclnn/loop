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
    [SerializeField] private float sideTriggerSpeedIncrement = 0.1f;
    public UnityEvent collide;
    private Collider hitCollider;

    private void Start()
    {
        hitCollider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (obstacleLayer == (obstacleLayer | (1 << other.gameObject.layer)))
        {
            collide.Invoke();
        }
    }

    public void SideTrigger()
    {
        transform.parent.GetComponent<Follower>().AddSpeed(sideTriggerSpeedIncrement);
    }

    public void ExplodeShip()
    {
        for (int i = 0; i < 2; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        Instantiate(explosion, transform.position, transform.rotation, transform.parent);
        hitCollider.enabled = false;
    }

    public void RepairShip()
    {
        for (int i = 0; i < 2; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
        hitCollider.enabled = true;
    }
}

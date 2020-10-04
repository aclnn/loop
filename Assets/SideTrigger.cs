using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideTrigger : MonoBehaviour
{
    [SerializeField] private DrillBehaviour drillBehaviour;
    [SerializeField] private GameObject sideTriggerParticles;
    
    private void OnTriggerExit(Collider other)
    {
        drillBehaviour.IncreaseDrillCharge(1);
        transform.parent.GetComponent<PlayerBehaviour>().SideTrigger();
        
        SpawnSideTriggerParticles(other.transform.position);
    }

    private void SpawnSideTriggerParticles(Vector3 otherPosition)
    {
        Vector3 particlesPosition = new Vector3(
            (transform.position.x + otherPosition.x) / 2F,
            (transform.position.y + otherPosition.y) / 2F,
            (transform.position.z + otherPosition.z) / 2F
        );
        Instantiate(sideTriggerParticles, particlesPosition, Quaternion.identity, transform.parent);
    }
}

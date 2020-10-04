using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideTrigger : MonoBehaviour
{
    [SerializeField] private DrillBehaviour drillBehaviour;
    
    private void OnTriggerExit(Collider other)
    {
        drillBehaviour.IncreaseDrillCharge(1);
        transform.parent.GetComponent<PlayerBehaviour>().SideTrigger();
    }
}

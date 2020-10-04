using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideTrigger : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        transform.parent.GetComponent<PlayerBehaviour>().SideTrigger();
    }
}

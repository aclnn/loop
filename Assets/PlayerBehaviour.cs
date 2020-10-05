using System;
using System.Collections;
using System.Collections.Generic;
using PathCreation;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private LayerMask obstacleLayer;
    [SerializeField] private GameObject explosion;
    [SerializeField] private float sideTriggerSpeedIncrement = 0.1f;
    [SerializeField] private int baseHealth;
    [SerializeField] private GameObject restartGameObject;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private CursorScript cursorScript;
    private int health;
    public UnityEvent collide;
    private Collider hitCollider;
    private Follower followerComponent;
    private Movement movementComponent;

    private void Start()
    {
        hitCollider = GetComponent<Collider>();
        followerComponent = GetComponentInParent<Follower>();
        movementComponent = GetComponent<Movement>();

        health = baseHealth;
        healthText.text = "life: " + health + "/" + baseHealth;
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

    public void BrickCollide()
    {
        health--;
        healthText.text = "life: " + health + "/" + baseHealth;
        
        if (health <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(Immunity(2F));
        }
    }

    public void Die()
    {
        followerComponent.CanMove = false;
        movementComponent.MovementEnabled = false;
        restartGameObject.SetActive(true);
        
        for (int i = 0; i < 2; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        Instantiate(explosion, transform.position, transform.rotation, transform.parent);
        hitCollider.enabled = false;
        cursorScript.EnableCursor();
    }

    public IEnumerator Immunity(float duration)
    {
        // Disable colliders
        GetComponent<BoxCollider>().enabled = false;
        BoxCollider[] sideTriggerColliders = GameObject.FindWithTag("SideTrigger").GetComponents<BoxCollider>();
        foreach (BoxCollider sideTriggerCollider in sideTriggerColliders)
        {
            sideTriggerCollider.enabled = false;
        }
        
        // Blink
        MeshRenderer playerModel = GameObject.FindWithTag("PlayerModel").GetComponent<MeshRenderer>();
        for (int i = 0; i < duration * 4; i++)
        {
            playerModel.enabled = !playerModel.enabled;
            yield return new WaitForSeconds(.2F);
        }
        playerModel.enabled = true;
        
        // Enable colliders
        GetComponent<BoxCollider>().enabled = true;
        foreach (BoxCollider sideTriggerCollider in sideTriggerColliders)
        {
            sideTriggerCollider.enabled = true;
        }
    }

    public void RepairShip()
    {
        for (int i = 0; i < 2; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
        hitCollider.enabled = true;
        health = baseHealth;
    }
}

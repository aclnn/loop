using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DrillBehaviour : MonoBehaviour
{
    public int drillCharge = 0;
    [SerializeField] private int drillChargeMax = 8;
    private Collider collider;

    [Header("Camera boost settings")]
    [SerializeField] [Range(0, 1)] private float cameraFovTime = 0.3f;
    [SerializeField] private float cameraFovIncrement = 60;
    private float cameraStartFov;
    
    [Header("References")] 
    public Follower follower;
    [SerializeField] private Camera camera;
    [SerializeField] private TextMeshProUGUI drillText;
    [SerializeField] private Collider bodyCollider;
    [SerializeField] private LayerMask obstacleLayer;
    void Start()
    {
        collider = GetComponent<Collider>();
        collider.enabled = false;
        drillText.text = "drill: " + drillCharge + "/" + drillChargeMax;

    }

    public void SetDrillCharge(int value)
    {
        drillCharge = value;
        IncreaseDrillCharge(0);
    }
    
    public void IncreaseDrillCharge(int charge)
    {
        if (drillCharge < drillChargeMax)
        {
            drillCharge += charge;
            if (drillCharge == drillChargeMax)
            {
                drillText.color = Color.red;
            }
            else
            {
                drillText.color = Color.white;
            }
            drillText.text = "drill: " + drillCharge + "/" + drillChargeMax;

        }
    }

    public IEnumerator ActivateDrill(float duration)
    {
        if (drillCharge >= drillChargeMax)
        {
            StartCoroutine(ChangeCameraFov(camera.fieldOfView + cameraFovIncrement));
            drillCharge = 0;
            IncreaseDrillCharge(0); //Mettre à jour le texte
            Debug.Log("DRILL ON");
            collider.enabled = true;
            bodyCollider.enabled = false;
            yield return new WaitForSeconds(duration);
            StartCoroutine(ChangeCameraFov(cameraStartFov));
            collider.enabled = false;
            bodyCollider.enabled = true;
            Debug.Log("DRILL OFF");
        }
    }

    private IEnumerator ChangeCameraFov(float goalfov)
    {
        if (goalfov > camera.fieldOfView)
        {
            while (camera.fieldOfView < goalfov)
            {
                camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, goalfov, cameraFovTime);
                yield return new WaitForEndOfFrame();
            }
        }
        else
        {
            while (camera.fieldOfView > goalfov)
            {
                camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, goalfov, cameraFovTime);
                yield return new WaitForEndOfFrame();
            }
        }

        camera.fieldOfView = goalfov;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (obstacleLayer == (obstacleLayer | (1 << other.gameObject.layer)))
        {
            Debug.Log("OBSTACLE COLLDIE");
            Destroy(other.gameObject);
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}

using System;
using System.Collections;
using UnityEngine;
using PathCreation;
using TMPro;
using UnityEngine.Events;

public class Follower : MonoBehaviour
{
    public PathCreator pathCreator;
    public float speed = 5;
    private float distanceTravelled;
    private float currentLapDistance;
    [SerializeField] private Vector3 transformOffset;

    [SerializeField] private Camera camera;
    [SerializeField] [Range(0, 1f)] private float fovStepTime = 0.3f;
    [SerializeField] [Range(0, 1f)] private float speedStepTime = 0.3f;
    [SerializeField] [Range(0, 1f)] private float displaySpeedStepTime = 0.3f;
    [SerializeField] [Range(0, 1f)] private float fovDecreaseTime = 1f;

    [SerializeField] private TextMeshProUGUI speedText;
    
    public UnityEvent lapDone = new UnityEvent();

    private bool startCountingLap = false;
    private bool canMove = true;
    private float smoothspeed;
    private float cameraStartingFov;
    private float targetfov;
    private float targetSpeed;
    private float startingSpeed;
    private float displaySpeed;

    public bool CanMove
    {
        get => canMove;
        set => canMove = value;
    }

    public bool StartCountingLap
    {
        get => startCountingLap;
        set => startCountingLap = value;
    }

    public void AddSpeed(float add)
    {
        StartCoroutine(AddSpeedRoutine(add));
    }
    
    private IEnumerator AddSpeedRoutine(float add)
    {
        StartCoroutine(AddFov());
        targetSpeed += add;
        while (speed < targetSpeed)
        {
            speed = Mathf.Lerp(speed, targetSpeed, speedStepTime);
            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator AddFov()
    {
        targetfov += 1;
        while (camera.fieldOfView < targetfov)
        {
            camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, targetfov, fovStepTime);
            yield return new WaitForEndOfFrame();
        }
    }

    public void ResetLap()
    {
        StopAllCoroutines();
        currentLapDistance = 0;
        speed = startingSpeed;
        targetSpeed = startingSpeed;
        displaySpeed = startingSpeed;
        targetfov = cameraStartingFov;
        StartCoroutine(ReduceFOV());
    }

    private IEnumerator ReduceFOV()
    {
        while (camera.fieldOfView > cameraStartingFov)
        {
            camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, cameraStartingFov, fovDecreaseTime);
            yield return new WaitForEndOfFrame();
        }

        camera.fieldOfView = cameraStartingFov;
    }
    
    void Start()
    {
        cameraStartingFov = camera.fieldOfView;
        targetfov = cameraStartingFov;
        startingSpeed = speed;
        targetSpeed = speed;
        displaySpeed = speed;

    }
    
    // Update is called once per frame
    void Update()
    {
        displaySpeed = Mathf.Lerp(displaySpeed, targetSpeed, displaySpeedStepTime);
        speedText.text = String.Format("Speed: {0:F3}", Math.Round(displaySpeed, 3));
        
        if (canMove)
        {
            distanceTravelled += speed * Time.deltaTime;
            transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled) + transformOffset;
            transform.rotation = new Quaternion(
                pathCreator.path.GetRotationAtDistance(distanceTravelled).x,
                pathCreator.path.GetRotationAtDistance(distanceTravelled).y,
                pathCreator.path.GetRotationAtDistance(distanceTravelled).z,
                pathCreator.path.GetRotationAtDistance(distanceTravelled).w
                );
        }
        //J'ai suivi le tuto pour faire ça
        
        if (startCountingLap)
        {
            currentLapDistance += speed * Time.deltaTime;
        }
        
        if (currentLapDistance > pathCreator.path.length)
        {
            Debug.Log("LAP DONE");
            currentLapDistance = 0;
            lapDone.Invoke();
        }
    }
}

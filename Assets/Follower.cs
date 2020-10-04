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
    [SerializeField] private float fovStepTime = 0.3f;
    [SerializeField] private float fovDecreaseTime = 1f;

    [SerializeField] private TextMeshProUGUI speedText;
    
    public UnityEvent lapDone = new UnityEvent();

    private bool startCountingLap = false;
    private bool canMove = true;
    private float smoothspeed;
    private float cameraStartingFov;
    private float targetfov;
    private float targetSpeed;
    private float startingSpeed;

    private float smoothRef;

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
            speed = Mathf.SmoothDamp(speed, targetSpeed, ref smoothRef, fovStepTime);
            yield return null;
        }
    }

    private IEnumerator AddFov()
    {
        targetfov += 1;
        while (camera.fieldOfView < targetfov)
        {
            camera.fieldOfView = Mathf.SmoothDamp(camera.fieldOfView, targetfov, ref smoothRef, fovStepTime);
            yield return null;
        }
    }

    public void ResetLap()
    {
        StopAllCoroutines();
        currentLapDistance = 0;
        speed = startingSpeed;
        targetSpeed = startingSpeed;
        StartCoroutine(ReduceFOV());
    }

    private IEnumerator ReduceFOV()
    {
        while (camera.fieldOfView > cameraStartingFov)
        {
            camera.fieldOfView = Mathf.SmoothDamp(camera.fieldOfView, cameraStartingFov,ref smoothRef, fovDecreaseTime);
            yield return null;
        }

        camera.fieldOfView = cameraStartingFov;
    }
    
    void Start()
    {
        cameraStartingFov = camera.fieldOfView;
        targetfov = camera.fieldOfView;
        startingSpeed = speed;
        targetSpeed = speed;
        
    }
    
    // Update is called once per frame
    void Update()
    {
        speedText.text = "Speed: " + System.Math.Round(speed, 3);
        
        if (canMove)
        {
            distanceTravelled += speed * Time.deltaTime;
            transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled) + transformOffset;
            transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled);
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

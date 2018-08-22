using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    public float maxLaunchSpeed;

    public PhysicsEngine ballToLaunch;
    public GameObject launchedBalls;

    public AudioClip windUpFX;
    public AudioClip launchFX;

    private float launchSpeed;
    private float extraSpeedPerFrame;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();

        extraSpeedPerFrame = (maxLaunchSpeed * Time.fixedDeltaTime) / windUpFX.length;
    }

    private void IncreaseLaunchSpeed()
    {
        if (launchSpeed < maxLaunchSpeed)
        {
            launchSpeed += extraSpeedPerFrame;
        }
    }

    private void OnMouseDown()
    {
        launchSpeed = 0f;

        InvokeRepeating("IncreaseLaunchSpeed", 0.5f, Time.fixedDeltaTime);

        audioSource.clip = windUpFX;
        audioSource.Play();
    }

    private void OnMouseUp()
    {
        Launch();
    }

    private void Launch()
    {
        CancelInvoke("IncreaseLaunchSpeed");

        audioSource.Stop();
        audioSource.clip = launchFX;
        audioSource.Play();

        Vector3 launchVelocity = new Vector3(1f, 1f, 0f).normalized * launchSpeed;

        PhysicsEngine newBall = Instantiate(ballToLaunch) as PhysicsEngine;
        newBall.transform.parent = launchedBalls.transform;

        newBall.velocityVector = launchVelocity;
    }
}
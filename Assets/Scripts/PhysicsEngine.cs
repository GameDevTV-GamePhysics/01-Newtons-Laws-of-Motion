using System.Collections.Generic;
using UnityEngine;

public class PhysicsEngine : MonoBehaviour
{
    public float mass;                  // [kg]
    public Vector3 velocityVector;      // [m s^-1]
    public Vector3 netForceVector;      // N [kg m s^-2]

    public bool showTrails = true;

    private List<Vector3> forceVectorList = new List<Vector3>();
    private LineRenderer lineRenderer;
    private int numberOfForces;

    public void AddForce(Vector3 forceVector)
    {
        forceVectorList.Add(forceVector);

        Debug.Log("Adding force " + forceVector + " to " + gameObject.name);
    }

    private void Start()
    {
        SetupThrustTrails();
    }

    private void FixedUpdate()
    {
        RenderTrails();
        UpdatePosition();
    }

    private void SetupThrustTrails()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.yellow;
        lineRenderer.endColor = Color.yellow;
        lineRenderer.startWidth = 0.2f;
        lineRenderer.endWidth = 0.2f;
        lineRenderer.useWorldSpace = false;
    }

    private void RenderTrails()
    {
        if (showTrails)
        {
            lineRenderer.enabled = true;
            numberOfForces = forceVectorList.Count;
            lineRenderer.positionCount = numberOfForces * 2;
            int i = 0;
            foreach (Vector3 forceVector in forceVectorList)
            {
                lineRenderer.SetPosition(i, Vector3.zero);
                lineRenderer.SetPosition(i + 1, -forceVector);
                i = i + 2;
            }
        }
        else
        {
            lineRenderer.enabled = false;
        }
    }

    private void UpdatePosition()
    {
        // sum forces and clear list
        netForceVector = Vector3.zero;

        foreach (Vector3 vectorForce in forceVectorList)
        {
            netForceVector += vectorForce;
        }

        forceVectorList.Clear();
        forceVectorList.TrimExcess();

        // calculate position and change due to net force
        Vector3 accelerationVector;

        accelerationVector = netForceVector / mass;

        velocityVector += accelerationVector * Time.deltaTime;

        transform.position += velocityVector * Time.deltaTime;
    }
}

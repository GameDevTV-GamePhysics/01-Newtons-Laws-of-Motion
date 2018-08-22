using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniversalGravitation : MonoBehaviour
{
    private PhysicsEngine[] physicsEngineArray;
    private const float bigG = 6.67e-11f;  // [ m^3 s^-2 kg^-1 ]

    private void Start()
    {
        physicsEngineArray = GameObject.FindObjectsOfType<PhysicsEngine>();
    }

    private void FixedUpdate()
    {
        CalculateGravity();
    }

    private void CalculateGravity()
    {
        foreach (PhysicsEngine physicsEngineA in physicsEngineArray)
        {
            foreach (PhysicsEngine physicsEngineB in physicsEngineArray)
            {
                if (physicsEngineA != physicsEngineB && physicsEngineA != this)
                {
                    Debug.Log("Calculating gravitational force exerted on " + physicsEngineA.name + " due to the gravity of " + physicsEngineB.name);

                    Vector3 offset = physicsEngineA.transform.position - physicsEngineB.transform.position;
                    float rSquared = Mathf.Pow(offset.magnitude, 2f);
                    float gravityMagnitude = bigG * physicsEngineA.mass * physicsEngineB.mass / rSquared;
                    Vector3 gravityFeltVector = gravityMagnitude * offset.normalized;

                    physicsEngineA.AddForce(-gravityFeltVector);
                }
            }
        }
    }
}

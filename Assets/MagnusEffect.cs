using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnusEffect : MonoBehaviour
{
    public float magnusConstant = 1f;

    private Rigidbody rigidBody;

    // Use this for initialization
    private void Start()
    {
        rigidBody = gameObject.GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rigidBody.AddForce(magnusConstant * Vector3.Cross(rigidBody.angularVelocity, rigidBody.velocity) * Time.fixedDeltaTime);
    }

}

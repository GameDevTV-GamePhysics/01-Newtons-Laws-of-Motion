using UnityEngine;

[RequireComponent(typeof(PhysicsEngine))]
public class FluidDrag : MonoBehaviour
{
    [Range(1f, 2f)]
    public float velocityExponent;      // [none]

    public float dragConstant;          // ??

    private PhysicsEngine physicsEngine;

    private void Start()
    {
        physicsEngine = GetComponent<PhysicsEngine>();
    }

    private float CalculateDrag(float speed)
    {
        return dragConstant * Mathf.Pow(speed, velocityExponent);
    }

    private void FixedUpdate()
    {
        Vector3 velocityVector = physicsEngine.velocityVector;

        float speed = velocityVector.magnitude;
        float dragSize = CalculateDrag(speed);
        Vector3 dragVector = dragSize * -velocityVector.normalized;

        physicsEngine.AddForce(dragVector);
    }
}
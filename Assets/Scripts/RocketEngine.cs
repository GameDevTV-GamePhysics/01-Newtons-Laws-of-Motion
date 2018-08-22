using UnityEngine;

[RequireComponent(typeof(PhysicsEngine))]
public class RocketEngine : MonoBehaviour
{
    public float fuelMass;              // kg
    public float maxThrust;             // kN [kg m s^-2]

    [Range(0f, 1f)]
    public float thrustPercent;         // [none]

    public Vector3 thrustUnitVector;    // [none]

    private PhysicsEngine physicsEngine;
    private float currentThrust;        // N

    void Start()
    {
        physicsEngine = gameObject.GetComponent<PhysicsEngine>();
        physicsEngine.mass += fuelMass;
    }

    private void FixedUpdate()
    {
        float spentFuelMass = FuelThisUpdate();

        if (fuelMass > spentFuelMass)
        {
            fuelMass -= spentFuelMass;
            physicsEngine.mass -= spentFuelMass;

            ExertForce();
        }
        else
        {
            Debug.LogWarning("Out of rocket fuel");
        }
    }

    private float FuelThisUpdate()
    {
        float exhaustMassFlow;                                          // [kg s^-1]
        float effectiveExhaustVelocity;                                 // [m s^-1]

        effectiveExhaustVelocity = 4476f;                               // [m s^-1] Liquid H O 
        exhaustMassFlow = currentThrust / effectiveExhaustVelocity;

        return exhaustMassFlow * Time.deltaTime;                        // [kg]
    }

    private void ExertForce()
    {
        currentThrust = thrustPercent * maxThrust * 1000f;
        Vector3 thrustVector = thrustUnitVector.normalized * currentThrust;    // N

        physicsEngine.AddForce(thrustVector);
    }
}

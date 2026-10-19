using UnityEngine;

public class Boat : MonoBehaviour
{
    // these could be parametrized if need be
    const float DRAG = 0.7f;
    const float MAX_ACCELERATION = 5;
    const float SAIL_ANGLE_DISCREPANCY_FORCE_LOSS = 1;
    [SerializeField] WaterGenerator _waterGenerator;

    private Vector2 _velocity;
    private float _hullDirection;
    private float _sailDirection;
    private float _sailFullness = 0.4f;
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void FixedUpdate()
    {
        _velocity *= 1 - (DRAG * Time.fixedDeltaTime);
        float sailForce = 1 - Mathf.Abs(
            Vector2.Dot(GameController.WindDirection, Quaternion.AngleAxis(_sailDirection, Vector3.forward) * Vector2.right) -
            Vector2.Dot(Quaternion.AngleAxis(_hullDirection, Vector3.forward) * Vector2.right, Quaternion.AngleAxis(_sailDirection, Vector3.forward) * Vector2.right)
        ) * SAIL_ANGLE_DISCREPANCY_FORCE_LOSS;
        Debug.Log(sailForce);
        _velocity += (Vector2)(Quaternion.AngleAxis(_hullDirection, Vector3.forward) * Vector2.right) * sailForce  * MAX_ACCELERATION * _sailFullness * Time.fixedDeltaTime;

        transform.position += new Vector3(_velocity.x,0,_velocity.y) * Time.fixedDeltaTime;
    }

    public void SetWaterGenerator(WaterGenerator wg)
    {
        _waterGenerator = wg;
    }
}

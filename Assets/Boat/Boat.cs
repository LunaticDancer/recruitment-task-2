using UnityEngine;

public class Boat : MonoBehaviour
{
    // these could be parametrized if need be
    const float DRAG = 0.2f;
    const float MAX_ACCELERATION = 5;
    const float SAIL_ANGLE_DISCREPANCY_FORCE_LOSS = 1;
    const float SAIL_DIRECTION_LIMIT = 90;

    [SerializeField] WaterGenerator _waterGenerator;
    [SerializeField] Transform _sail;
    [SerializeField] MeshRenderer _sailMaterial;

    private Vector2 _velocity;
    private float _hullDirection;
    private float _sailDirection;
    private float _sailFullness = 0.4f;
    private InputSystem_Actions _input;
    
    void Awake()
    {
        _input = new InputSystem_Actions();
        _input.Player.Enable();
    }

    void Start()
    {
        
    }

    void Update()
    {
        ReadInput();
        UpdateVisuals();
    }

    void FixedUpdate()
    {
        _velocity *= 1 - (DRAG * Time.fixedDeltaTime);
        float sailForce = Mathf.Clamp01(1 - Mathf.Abs(
            Vector2.Dot(GameController.WindDirection, Quaternion.AngleAxis(_sailDirection + _hullDirection, Vector3.forward) * Vector2.right) -
            Vector2.Dot(Quaternion.AngleAxis(_hullDirection, Vector3.forward) * Vector2.right, Quaternion.AngleAxis(_sailDirection + _hullDirection, Vector3.forward) * Vector2.right)
        )) * SAIL_ANGLE_DISCREPANCY_FORCE_LOSS;
        Debug.Log(sailForce);
        _velocity += (Vector2)(Quaternion.AngleAxis(_hullDirection, Vector3.forward) * Vector2.right) * sailForce  * MAX_ACCELERATION * _sailFullness * Time.fixedDeltaTime;

        transform.position += new Vector3(_velocity.x,0,-_velocity.y) * Time.fixedDeltaTime;
    }

    void ReadInput()
    {
        _sailFullness = Mathf.Clamp01(_sailFullness + _input.Player.Sail.ReadValue<Vector2>().y * 0.5f * Time.deltaTime);
        _sailDirection = Mathf.Clamp(_sailDirection + _input.Player.Sail.ReadValue<Vector2>().x * 60 * Time.deltaTime, -SAIL_DIRECTION_LIMIT, SAIL_DIRECTION_LIMIT);
        _hullDirection += _input.Player.Rudder.ReadValue<float>() * 10 * Time.deltaTime * _velocity.magnitude;
    }

    void UpdateVisuals()
    {
        transform.rotation = Quaternion.Euler(0, _hullDirection, 0);
        _sail.localEulerAngles = new Vector3(0, _sailDirection, 0);
        _sailMaterial.materials[0].SetFloat("_Percent", _sailFullness);
    }

    public void SetWaterGenerator(WaterGenerator wg)
    {
        _waterGenerator = wg;
    }
}

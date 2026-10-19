using UnityEngine;

public class GameController : MonoBehaviour
{
    const float WIND_VARIANCE = 4;
    public static GameController Instance;
    public static Vector2 WindDirection;
    [SerializeField] WaterGenerator _waterGenerator;
    // I'd usually spawn the boat after Scene init, but the task explicitly requires it being present in the scene
    [SerializeField] Boat _boat;

    void Awake()
    {
        if(Instance != null) Destroy(gameObject);
        Instance = this;

        WindDirection = Random.onUnitCircle;
    }

    void Start()
    {
        _waterGenerator.GeneratePlane();
        _boat.SetWaterGenerator(_waterGenerator);
        CameraController.Instance.SetTarget(_boat.transform);
    }

    void FixedUpdate()
    {
        WindDirection = (WindDirection + Random.onUnitCircle * WIND_VARIANCE * Time.fixedDeltaTime).normalized;
    }
}

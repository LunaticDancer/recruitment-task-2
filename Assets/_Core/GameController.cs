using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    [SerializeField] WaterGenerator _waterGenerator;
    void Awake()
    {
        if(Instance != null) Destroy(gameObject);
        Instance = this;
    }

    void Start()
    {
        _waterGenerator.GeneratePlane();
    }
}

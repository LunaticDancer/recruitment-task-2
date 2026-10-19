using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance;

    [SerializeField] Transform _target;
    [SerializeField] float _followIntensity = 1;

    Vector3 _dampVelocity = Vector3.zero;

    void Awake()
    {
        if(Instance != null) Destroy(gameObject);
        Instance = this;
    }

    void FixedUpdate()
    {
        if(_target == null) return;
        transform.position = Vector3.SmoothDamp(transform.position, _target.position, ref _dampVelocity, 0.3f);
    }

    public void SetTarget(Transform t)
    {
        _target = t;
    }
}

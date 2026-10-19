using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance;

    [SerializeField] Transform _target;
    [SerializeField] float _followIntensity = 1;

    void Awake()
    {
        if(Instance != null) Destroy(gameObject);
        Instance = this;
    }

    void Update()
    {
        if(_target == null) return;
        transform.position = Vector3.Lerp(transform.position, _target.position, Mathf.Min(1, Time.deltaTime * _followIntensity));
    }

    public void SetTarget(Transform t)
    {
        _target = t;
    }
}

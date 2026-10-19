using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class UserInterface : MonoBehaviour
{
    public static UserInterface Instance;
    [SerializeField] TextMeshProUGUI _angle;
    [SerializeField] TextMeshProUGUI _sail;
    void Awake()
    {
        if(Instance != null) Destroy(gameObject);
        Instance = this;
    }

    public void SetAngle(float v)
    {
        _angle.text = Mathf.RoundToInt(v).ToString() + " Deg";
    }

    public void SetSail(float v)
    {
        _sail.text = Mathf.RoundToInt(v*100).ToString() + "%";
    }
}

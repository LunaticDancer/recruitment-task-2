using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

public class UserInterface : MonoBehaviour
{
    public static UserInterface Instance;
    [SerializeField] PanelRenderer _renderer;

    Label _angleDisplay;
    Label _sailDisplay;
    string _angle;
    string _sail;
    void Awake()
    {
        if(Instance != null) Destroy(gameObject);
        Instance = this;
    }

    void OnEnable()
    {
        _renderer.RegisterUIReloadCallback(OnUiReload);
    }

    void OnDisnable()
    {
        _renderer.UnregisterUIReloadCallback(OnUiReload);
    }

    public void SetAngle(float v)
    {
        _angle = Mathf.RoundToInt(v).ToString() + " Deg";
        if(_angleDisplay == null) return;
        _angleDisplay.text = _angle;
    }

    public void SetSail(float v)
    {
        _sail = Mathf.RoundToInt(v*100).ToString() + "%";
        if(_sailDisplay == null) return;
        _sailDisplay.text = _sail;
    }

    void OnUiReload(PanelRenderer pr, VisualElement root)
    {
        _angleDisplay = root.Q<Label>("Angle");
        _sailDisplay = root.Q<Label>("Sail");
    }
}

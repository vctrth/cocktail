using UnityEngine;

public class glass_submit : MonoBehaviour
{
    private glass_fill fill;

    void Start()
    {
        fill = GetComponent<glass_fill>();
    }

    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.RTouch)) // A-knop
        {
            GameDirector.Instance.CheckGlassContents();
        }
    }
}
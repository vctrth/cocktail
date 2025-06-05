using TMPro;
using UnityEngine;

public class debugging : MonoBehaviour
{
    public Transform target;
    public TextMeshPro textMesh;

    void Update()
    {
        float angle = target.localEulerAngles.x;
        if (angle > 180) angle -= 360;

        textMesh.text = "Hoek: " + Mathf.RoundToInt(angle) + "°";
    }
}

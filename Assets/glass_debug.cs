using TMPro;
using UnityEngine;
using System.Collections.Generic;

public class glass_debug : MonoBehaviour
{
    public TextMeshPro textMesh;
    public GameObject glass;

    void Update()
    {
        var glassFill = glass.GetComponent<glass_fill>();
        Dictionary<string, float> volumes = glassFill.ingredientVolumes;

        float total = 0f;
        string debugText = "";

        foreach (KeyValuePair<string, float> entry in volumes)
        {
            float rounded = Mathf.RoundToInt(entry.Value);
            debugText += $"{entry.Key}: {rounded}L\n";
            total += entry.Value;
        }

        debugText += $"Totaal: {Mathf.RoundToInt(total)}L";

        textMesh.text = debugText;
    }
}

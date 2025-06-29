using TMPro;
using UnityEngine;

public class glass_debug : MonoBehaviour
{
    public TextMeshProUGUI textMesh;

    void Update()
    {
        if (GameDirector.Instance == null) return;

        string debugText = "Inhoud glas:\n\n";

        // Toon vloeibare ingrediŽnten
        debugText += "Vloeistoffen:\n";
        float liquidTotal = 0f;
        foreach (var entry in GameDirector.Instance.GetLiquidVolumes())
        {
            debugText += $"- {entry.Key}: {entry.Value:F2}L\n";
            liquidTotal += entry.Value;
        }
        debugText += $"Totaal vloeistof: {liquidTotal:F2}L\n\n";

        // Toon vaste ingrediŽnten
        debugText += "Vaste ingrediŽnten:\n";
        foreach (var entry in GameDirector.Instance.GetSolidCounts())
        {
            debugText += $"- {entry.Key}: {entry.Value}x\n";
        }

        textMesh.text = debugText;
    }
}
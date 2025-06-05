using TMPro;
using UnityEngine;

public class glass_debug : MonoBehaviour
{
    public TextMeshPro textMesh;
    public GameObject glass;
    private float currentSprite = 0.0f;
    private float currentBruis = 0.0f;

    void Update()
    {
        currentSprite = glass.GetComponent<glass_fill>().spriteVolume;
        currentBruis = glass.GetComponent<glass_fill>().bruisVolume;
        textMesh.text = "Sprite: " + Mathf.RoundToInt(currentSprite) + "L\n" +
                      "Bruis: " + Mathf.RoundToInt(currentBruis) + "L\n" +
                      "Totaal: " + Mathf.RoundToInt(currentSprite + currentBruis) + "L";
    }
}

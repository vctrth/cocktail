using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class glass_fill : MonoBehaviour
{
    public Transform liquidMesh;           // Het meshje dat stijgt
    public float fillSpeed = 0.1f;         // Hoe snel het vult
    public float maxFill = 5.0f;           // Maximum hoeveelheid vloeistof

    private bool hasSubmitted = false;     // Wordt true na het controleren
    private float currentFill = 0f;        // Huidige inhoud

    public Dictionary<string, float> ingredientVolumes = new Dictionary<string, float>();

    void Update()
    {
        // Reset met B-knop op rechter controller
        if (OVRInput.GetDown(OVRInput.Button.Two, OVRInput.Controller.RTouch))
        {
            ResetGlass();
        }
    }

    void OnParticleCollision(GameObject other)
    {
        if (!other.CompareTag("Liquid") || currentFill >= maxFill || hasSubmitted)
            return;

        string tag = other.name.ToLower();
        float added = fillSpeed * Time.deltaTime;

        if (!ingredientVolumes.ContainsKey(tag))
            ingredientVolumes[tag] = 0f;

        ingredientVolumes[tag] += added;
        currentFill += added;

        UpdateLiquidLevel();

        if (currentFill >= maxFill)
        {
            hasSubmitted = true;
            GameDirector.Instance.CheckGlassContents(ingredientVolumes);
        }
    }

    void UpdateLiquidLevel()
    {
        Vector3 scale = liquidMesh.localScale;
        scale.y = Mathf.Clamp(currentFill, 0f, maxFill); // vloeistof groeit van 0 tot 5
        liquidMesh.localScale = scale;
    }

    public void ResetGlass()
    {
        ingredientVolumes.Clear();
        currentFill = 0f;
        hasSubmitted = false;
        UpdateLiquidLevel();
        Debug.Log("🧼 Glas gereset via B-knop");
    }
}

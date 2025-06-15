using UnityEngine;
using System.Collections.Generic;

public class glass_fill : MonoBehaviour
{
    public Transform liquidMesh; // Het meshje dat stijgt
    public float fillSpeed = 0.1f;
    public float maxFill = 5.0f;
    private bool hasSubmitted = false;


    public Dictionary<string, float> ingredientVolumes = new Dictionary<string, float>();
    private float currentFill = 0f;

    void OnParticleCollision(GameObject other)
    {
        if (!other.CompareTag("Liquid") || currentFill >= maxFill || hasSubmitted) return;

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
        //scale.y = Mathf.Clamp(currentFill / maxFill, 0f, 1f); // verhouding 0–1
        scale.y = Mathf.Clamp(currentFill, 0f, maxFill);
        liquidMesh.localScale = scale;
    }

    public void ResetGlass()
    {
        ingredientVolumes.Clear();
        currentFill = 0f;
        hasSubmitted = false;
        UpdateLiquidLevel();
    }
}

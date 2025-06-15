using UnityEngine;
using System.Collections.Generic;

public class glass_fill : MonoBehaviour
{
    public Transform liquidMesh;           // Het meshje dat stijgt
    public float fillSpeed = 0.1f;         // Hoe snel het vult
    public float maxFill = 5.0f;           // Maximum hoeveelheid vloeistof
    private float currentFill = 0f;        // Huidige inhoud

    public bool hasSubmitted = false;     // Wordt true na het controleren

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

        string ingredientName = other.name.ToLower();
        float addedVolume = fillSpeed * Time.deltaTime;

        // Registreer vloeistof bij GameDirector
        GameDirector.Instance.AddIngredient(ingredientName, addedVolume, isSolid: false);

        currentFill += addedVolume;
        UpdateLiquidLevel();

        if (currentFill >= maxFill)
        {
            hasSubmitted = true;
            GameDirector.Instance.CheckGlassContents();
        }
    }

    public void AddSolidIngredient(string ingredientName)
    {
        // Registreer vast ingrediënt bij GameDirector (telt als 1 eenheid)
        GameDirector.Instance.AddIngredient(ingredientName, 1f, isSolid: true);
    }

    void UpdateLiquidLevel()
    {
        Vector3 scale = liquidMesh.localScale;
        scale.y = Mathf.Clamp(currentFill, 0f, maxFill);
        liquidMesh.localScale = scale;
    }

    public void ResetGlass()
    {
        currentFill = 0f;
        hasSubmitted = false;
        UpdateLiquidLevel();
        GameDirector.Instance.GoToNextRecipe(); // Dit reset ook de ingrediënten
        Debug.Log("🧼 Glas gereset via B-knop");
    }
}
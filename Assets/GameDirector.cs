using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class IngredientRequirement
{
    public string ingredientName;
    public float amountRequired;
}

[System.Serializable]
public class DrinkRecipe
{
    public string name;
    public List<IngredientRequirement> ingredients;
}

public class GameDirector : MonoBehaviour
{
    public static GameDirector Instance { get; private set; }

    public TextMeshProUGUI feedbackText;

    public List<DrinkRecipe> recipes;
    public int currentRecipeIndex = 0;

    private float roundedVolumes = 0.0f; // For rounding the actual volume to 1 decimal place

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public DrinkRecipe CurrentRecipe => recipes[currentRecipeIndex];

    public void CheckGlassContents(Dictionary<string, float> actualVolumes)
    {
        DrinkRecipe recipe = CurrentRecipe;
        bool allCorrect = true;

        string feedback = $"🔍 Recept: {recipe.name}\n";

        foreach (var ingredient in recipe.ingredients)
        {
            actualVolumes.TryGetValue(ingredient.ingredientName.ToLower(), out float actualVolume);
            roundedVolumes = Mathf.Round(actualVolume * 10.0f) *0.1f; // Round to 1 decimal 
            float difference = Mathf.Abs(roundedVolumes - ingredient.amountRequired);

            if (difference > 0.3f)
            {
                feedback += $"❌ {ingredient.ingredientName}: verwacht {ingredient.amountRequired}, gekregen {actualVolume:F2}\n";
                allCorrect = false;
            }
            else
            {
                feedback += $"✅ {ingredient.ingredientName}: correct ({actualVolume:F2})\n";
            }
        }

        if (allCorrect)
        {
            feedback += "\n🎉 Juiste drank gemaakt!";
            GoToNextRecipe();
        }
        else
        {
            feedback += "\n⚠️ Fout recept! Probeer opnieuw.";
        }

        feedbackText.text = feedback;
    }

    public void GoToNextRecipe()
    {
        currentRecipeIndex++;
        if (currentRecipeIndex >= recipes.Count)
            currentRecipeIndex = 0;
    }
}

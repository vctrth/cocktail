using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class IngredientRequirement
{
    public string ingredientName;
    public float amountRequired;
    public bool isSolid; // Toevoegen om aan te geven of het een vast ingrediënt is
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
    public TextMeshProUGUI checkText;

    public List<DrinkRecipe> recipes;
    public int currentRecipeIndex = 0;

    private Dictionary<string, float> liquidVolumes = new Dictionary<string, float>();
    private Dictionary<string, int> solidCounts = new Dictionary<string, int>();

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

    public void AddIngredient(string ingredientName, float amount, bool isSolid = false)
    {
        if (isSolid)
        {
            if (!solidCounts.ContainsKey(ingredientName))
                solidCounts[ingredientName] = 0;
            solidCounts[ingredientName] += (int)amount; // Voor vaste ingrediënten is amount het aantal
        }
        else
        {
            if (!liquidVolumes.ContainsKey(ingredientName))
                liquidVolumes[ingredientName] = 0f;
            liquidVolumes[ingredientName] += amount;
        }
    }

    public void CheckGlassContents()
    {
        DrinkRecipe recipe = CurrentRecipe;
        bool allCorrect = true;

        string feedback = $"🔍 Recept: {recipe.name}\n";

        foreach (var ingredient in recipe.ingredients)
        {
            if (ingredient.isSolid)
            {
                // Controleer vaste ingrediënten
                solidCounts.TryGetValue(ingredient.ingredientName.ToLower(), out int actualCount);
                int requiredCount = (int)ingredient.amountRequired; // Voor vaste ingrediënten is amountRequired het benodigde aantal

                if (actualCount != requiredCount)
                {
                    feedback += $"❌ {ingredient.ingredientName}: verwacht {requiredCount}x, gekregen {actualCount}x\n";
                    allCorrect = false;
                }
                else
                {
                    feedback += $"✅ {ingredient.ingredientName}: correct ({actualCount}x)\n";
                }
            }
            else
            {
                // Controleer vloeibare ingrediënten
                liquidVolumes.TryGetValue(ingredient.ingredientName.ToLower(), out float actualVolume);
                float roundedVolume = Mathf.Round(actualVolume * 10.0f) * 0.1f; // Round to 1 decimal 
                float difference = Mathf.Abs(roundedVolume - ingredient.amountRequired);

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

        checkText.text = feedback;
    }

    public Dictionary<string, float> GetLiquidVolumes()
    {
        return new Dictionary<string, float>(liquidVolumes);
    }

    public Dictionary<string, int> GetSolidCounts()
    {
        return new Dictionary<string, int>(solidCounts);
    }

    public void GoToNextRecipe()
    {
        currentRecipeIndex++;
        if (currentRecipeIndex >= recipes.Count)
            currentRecipeIndex = 0;

        // Reset de ingredienten voor het volgende recept
        liquidVolumes.Clear();
        solidCounts.Clear();
    }

    public void SelectRecipe(int recipeIndex)
    {
        if (recipeIndex >= 0 && recipeIndex < recipes.Count)
        {
            currentRecipeIndex = recipeIndex;
            string ingredientList = $"📋 Geselecteerd recept: {CurrentRecipe.name}\n\n";
            foreach (var ingredient in CurrentRecipe.ingredients)
            {
                if (ingredient.isSolid)
                    ingredientList += $"🔹 {ingredient.ingredientName}: {ingredient.amountRequired}x (vast)\n";
                else
                    ingredientList += $"🔹 {ingredient.ingredientName}: {ingredient.amountRequired}L (vloeibaar)\n";
            }

            feedbackText.text = ingredientList;

            // Reset de inhoud van het glas
            liquidVolumes.Clear();
            solidCounts.Clear();
        }
        else
        {
            Debug.LogWarning($"⚠️ Ongeldige recipe index: {recipeIndex}");
        }
    }

    public void ResetGlass()
    {
        liquidVolumes.Clear();
        solidCounts.Clear();
    }
}
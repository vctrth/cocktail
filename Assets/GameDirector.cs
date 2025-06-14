using System.Collections.Generic;
using UnityEngine;

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

    public List<DrinkRecipe> recipes;
    public int currentRecipeIndex = 0;

    public Dictionary<string, float> ingredientVolumes = new Dictionary<string, float>();

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

    public void AddIngredient(string tag, float amount)
    {
        tag = tag.ToLower();

        if (!ingredientVolumes.ContainsKey(tag))
            ingredientVolumes[tag] = 0f;

        ingredientVolumes[tag] += amount;
    }

    public void CheckGlassContents(Dictionary<string, float> actualVolumes)
    {
        DrinkRecipe recipe = CurrentRecipe;
        bool allCorrect = true;

        Debug.Log($"🔍 Recept: {recipe.name}");

        foreach (var ingredient in recipe.ingredients)
        {
            actualVolumes.TryGetValue(ingredient.ingredientName.ToLower(), out float actualVolume);
            float difference = Mathf.Abs(actualVolume - ingredient.amountRequired);

            if (difference > 0.1f)
            {
                Debug.Log($"❌ {ingredient.ingredientName}: verwacht {ingredient.amountRequired}, gekregen {actualVolume}");
                allCorrect = false;
            }
            else
            {
                Debug.Log($"✅ {ingredient.ingredientName}: correct ({actualVolume})");
            }
        }

        if (allCorrect)
        {
            Debug.Log("🎉 Juiste drank gemaakt!");
            GoToNextRecipe();
        }
        else
        {
            Debug.Log("⚠️ Fout recept! Probeer opnieuw.");
        }

        ResetIngredients(); // klaar voor volgende poging
    }

    public void GoToNextRecipe()
    {
        currentRecipeIndex++;
        if (currentRecipeIndex >= recipes.Count)
            currentRecipeIndex = 0;

        Debug.Log("➡️ Volgend recept: " + CurrentRecipe.name);
    }

    public void ResetIngredients()
    {
        ingredientVolumes.Clear();
    }
}

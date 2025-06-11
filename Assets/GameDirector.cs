using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RecipeIngredient
{
    public string ingredientName;
    public float amountRequired;
}

[System.Serializable]
public class DrinkRecipe
{
    public string name;
    public List<RecipeIngredient> ingredients;
}

public class GameDirector : MonoBehaviour
{
    public static GameDirector Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public List<DrinkRecipe> recipes;
    public int currentRecipeIndex = 0;

    public DrinkRecipe CurrentRecipe => recipes[currentRecipeIndex];

    public void CheckGlassContents(float spriteVolume, float bruisVolume)
    {
        DrinkRecipe recipe = CurrentRecipe;

        bool allCorrect = true;

        foreach (var ingredient in recipe.ingredients)
        {
            float actualVolume = 0f;

            switch (ingredient.ingredientName.ToLower())
            {
                case "sprite":
                    actualVolume = spriteVolume;
                    break;
                case "bruis":
                    actualVolume = bruisVolume;
                    break;
                default:
                    Debug.LogWarning("Onbekend ingrediënt: " + ingredient.ingredientName);
                    break;
            }

            if (Mathf.Abs(actualVolume - ingredient.amountRequired) > 0.1f)
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
            Debug.Log("🎉 Juiste drank gemaakt: " + recipe.name);
            GoToNextRecipe();
        }
    }

    public void GoToNextRecipe()
    {
        currentRecipeIndex++;

        if (currentRecipeIndex >= recipes.Count)
        {
            Debug.Log("✅ Alle recepten afgewerkt!");
            currentRecipeIndex = 0; // Of stop hier als gewenst
        }
        else
        {
            Debug.Log("Volgend recept: " + CurrentRecipe.name);
        }
    }
}

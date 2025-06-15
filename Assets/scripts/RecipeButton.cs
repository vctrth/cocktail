using UnityEngine;
using UnityEngine.UI;

public class RecipeButton : MonoBehaviour
{
    public int recipeIndex; // Stel dit in via de Inspector

    public void OnClickSelectRecipe()
    {
        if (GameDirector.Instance != null)
        {
            GameDirector.Instance.SelectRecipe(recipeIndex);
        }
        else
        {
            Debug.LogError("❌ GameDirector.Instance is null!");
        }
    }
}
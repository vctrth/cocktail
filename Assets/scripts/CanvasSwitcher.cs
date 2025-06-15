using UnityEngine;

public class CanvasSwitcher : MonoBehaviour
{
    public GameObject introCanvas;
    public GameObject mainMenuCanvas;
    public GameObject cocktailsCanvas;
    public GameObject recipeCanvas;
    public GameObject doneCanvas;

    public void ShowMainMenu()
    {
        introCanvas.SetActive(false);
        mainMenuCanvas.SetActive(true);
        cocktailsCanvas.SetActive(false);
        recipeCanvas.SetActive(false);
        doneCanvas.SetActive(false);
    }

    public void ShowCocktailsMenu()
    {
        mainMenuCanvas.SetActive(false);
        cocktailsCanvas.SetActive(true);
        recipeCanvas.SetActive(false);
        doneCanvas.SetActive(false);

    }

    public void ShowRecipeMenu()
    {
        mainMenuCanvas.SetActive(false);
        cocktailsCanvas.SetActive(false);
        recipeCanvas.SetActive(true);
        doneCanvas.SetActive(false);
    }

    public void ShowDoneMenu()
    {
        mainMenuCanvas.SetActive(false);
        cocktailsCanvas.SetActive(false);
        recipeCanvas.SetActive(false);
        doneCanvas.SetActive(true);
    }

}

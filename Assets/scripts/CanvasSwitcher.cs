using UnityEngine;

public class CanvasSwitcher : MonoBehaviour
{
    public GameObject introCanvas;
    public GameObject mainMenuCanvas;
    public GameObject cocktailsCanvas;

    public void ShowMainMenu()
    {
        introCanvas.SetActive(false);
        mainMenuCanvas.SetActive(true);
        cocktailsCanvas.SetActive(false);
    }

    public void ShowCocktailsMenu()
    {
        mainMenuCanvas.SetActive(false);
        cocktailsCanvas.SetActive(true);
    }
}

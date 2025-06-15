using UnityEngine;

public class CanvasSwitcher : MonoBehaviour
{
    public GameObject introCanvas;
    public GameObject mainMenuCanvas;

    public void ShowMainMenu()
    {
        introCanvas.SetActive(false);
        mainMenuCanvas.SetActive(true);
    }
}

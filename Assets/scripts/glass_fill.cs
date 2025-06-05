using UnityEngine;

public class glass_fill : MonoBehaviour
{
    public Transform liquidMesh; // het meshje dat stijgt
    public float fillSpeed = 0.1f;
    private float currentFill = 0f;
    public float maxFill = 5.0f;
    public float spriteVolume = 0.0f;
    public float bruisVolume = 0.0f;


    void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Liquid") && currentFill < maxFill)
        {
            if(other.name == "sprite")
            {
                spriteVolume += fillSpeed * Time.deltaTime; // Aantal liters dat de sprite toevoegt
                currentFill += fillSpeed * Time.deltaTime;
                UpdateLiquidLevel();
            }
            else if(other.name == "bruis")
            {
                bruisVolume += fillSpeed * Time.deltaTime; // Aantal liters dat de bruis toevoegt
                currentFill += fillSpeed * Time.deltaTime;
                UpdateLiquidLevel();

            }
        }
    }

    void UpdateLiquidLevel()
    {
        Vector3 scale = liquidMesh.localScale;
        scale.y = Mathf.Clamp(currentFill, 0f, maxFill);
        liquidMesh.localScale = scale;
    }
}

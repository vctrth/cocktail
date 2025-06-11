using UnityEngine;

public class glass_fill : MonoBehaviour
{
    public Transform liquidMesh;
    public float fillSpeed = 0.1f;
    private float currentFill = 0f;
    public float maxFill = 5.0f;

    public float spriteVolume = 0.0f;
    public float bruisVolume = 0.0f;

    private bool alreadyChecked = false;

    void OnParticleCollision(GameObject other)
    {
        if (currentFill >= maxFill || alreadyChecked)
            return;

        if (other.CompareTag("Liquid"))
        {
            float added = fillSpeed * Time.deltaTime;

            if (other.name.ToLower().Contains("sprite"))
            {
                spriteVolume += added;
            }
            else if (other.name.ToLower().Contains("bruis"))
            {
                bruisVolume += added;
            }

            currentFill += added;
            UpdateLiquidLevel();

            // Check recept als glas vol zit
            if (currentFill >= maxFill)
            {
                alreadyChecked = true;

                // Stuur volumes naar GameDirector
                GameDirector.Instance.CheckGlassContents(spriteVolume, bruisVolume);
            }
        }
    }

    void UpdateLiquidLevel()
    {
        Vector3 scale = liquidMesh.localScale;
        scale.y = Mathf.Clamp(currentFill / maxFill, 0f, 1f); // verhouding 0–1
        liquidMesh.localScale = scale;
    }
}

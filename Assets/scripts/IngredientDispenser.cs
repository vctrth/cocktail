using UnityEngine;

public class IngredientDispenser : MonoBehaviour
{
    public GameObject ingredientPrefab; // Sleep prefab hier naartoe
    public Transform spawnPoint; // Optioneel: waar precies spawnen
    public float cooldown = 0.5f; // Snelheidsbeperking
    private float nextSpawnTime = 0f;

    void Update()
    {
        if ((OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger) || OVRInput.GetDown(OVRInput.Button.SecondaryHandTrigger)) && Time.time >= nextSpawnTime)
        {
            SpawnIngredient();
            nextSpawnTime = Time.time + cooldown;
        }
    }

    void SpawnIngredient()
    {
        Vector3 position = spawnPoint != null ? spawnPoint.position : transform.position;
        Quaternion rotation = spawnPoint != null ? spawnPoint.rotation : transform.rotation;

        Instantiate(ingredientPrefab, position, rotation);
    }
}

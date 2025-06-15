using UnityEngine;

public class GlassIngredientTrigger : MonoBehaviour
{
    private glass_fill glassFill;

    void Start()
    {
        glassFill = GetComponentInParent<glass_fill>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (glassFill == null || glassFill.hasSubmitted) return;

        string tag = other.tag.ToLower();
        if (tag == "ice" || tag == "mint" || tag == "lime") // voeg meer toe indien nodig
        {
            glassFill.AddSolidIngredient(tag);
            Destroy(other.gameObject); // verwijder het object nadat het toegevoegd is
        }
    }
}
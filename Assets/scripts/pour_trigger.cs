using UnityEngine;

public class PourTrigger : MonoBehaviour
{
    public ParticleSystem pourEffect;

    void Start()
    {
        // Pak de shape module van het particle system
    }

    void Update()
    {
        // Gebruik de lokale rotatie van de fles
        float angle = transform.localEulerAngles.x;
        if (angle > 180) angle -= 360;

        // Begin schenken als de fles genoeg gekanteld is
        if (angle > 0)
        {
            if (!pourEffect.isPlaying)
            {
                pourEffect.Play();

            }
        }
        else
        {
            if (pourEffect.isPlaying)
            {
                pourEffect.Stop();
            }
        }
    }
}

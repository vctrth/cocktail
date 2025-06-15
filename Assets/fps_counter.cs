using UnityEngine;
using System.Collections.Generic;

public class fps_counter : MonoBehaviour
{
    [Header("Settings")]
    public float logInterval = 5f;
    public int sampleSize = 60; // Aantal frames om te middelen

    private List<float> fpsSamples = new List<float>();
    private float timer;

    void Update()
    {
        // Bereken huidige FPS
        float currentFPS = 1f / Time.unscaledDeltaTime;
        fpsSamples.Add(currentFPS);

        // Beperk sample size
        if (fpsSamples.Count > sampleSize)
            fpsSamples.RemoveAt(0);

        // Log op interval
        timer += Time.unscaledDeltaTime;
        if (timer >= logInterval)
        {
            LogFPSData();
            timer = 0f;
        }
    }

    void LogFPSData()
    {
        if (fpsSamples.Count == 0) return;

        float sum = 0f;
        float min = float.MaxValue;
        float max = float.MinValue;

        foreach (float fps in fpsSamples)
        {
            sum += fps;
            if (fps < min) min = fps;
            if (fps > max) max = fps;
        }

        float avg = sum / fpsSamples.Count;

        Debug.Log($"FPS: " +
                 $"Current: {1f / Time.unscaledDeltaTime:F1} | " +
                 $"Avg: {avg:F1} | " +
                 $"Min: {min:F1} | " +
                 $"Max: {max:F1} | " +
                 $"Samples: {fpsSamples.Count}");
    }
}
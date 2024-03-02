using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utility : MonoBehaviour
{
    public static Palette InstantiatePalette(GameObject palettePrefab, Transform spawnPosition, Transform parent)
    {
        if (palettePrefab != null)
        {
            GameObject paletteObj = Instantiate(palettePrefab, spawnPosition.position, Quaternion.identity, parent);
            Palette palette = paletteObj.GetComponent<Palette>();
            return palette;
        }
        else
        {
            Debug.LogError("Palette prefab is not assigned.");
            return null;
        }
    }

    public static Ball InstantiateBall(GameObject ballPrefab, Transform spawnPosition, Transform parent)
    {
        if (ballPrefab != null)
        {
            GameObject ballObj = Instantiate(ballPrefab, spawnPosition.position, Quaternion.identity, parent);
            Ball ball = ballObj.GetComponent<Ball>();
            return ball;
        }
        else
        {
            Debug.LogError("Ball prefab is not assigned in the GameModeController.");
            return null;
        }
    }
}

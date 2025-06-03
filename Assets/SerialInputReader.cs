using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerialInputReader : MonoBehaviour
{
    public static SerialInputReader Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    // This function will be updated once Arduino is available
    public float GetTiltValue()
    {
        // Simulate no input for now
        return 0f;
    }
}


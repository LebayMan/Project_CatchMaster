using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatcherInput : MonoBehaviour
{
    public enum InputMode { Arduino, DebugKeyboard }
    public InputMode inputMode = InputMode.DebugKeyboard;

    public float moveSpeed = 5f;
    private float inputX;

    void Update()
    {
        if (inputMode == InputMode.DebugKeyboard)
        {
            inputX = Input.GetAxis("Horizontal"); // A/D or Left/Right
        }
        else if (inputMode == InputMode.Arduino)
        {
            // Placeholder for serial input:
            inputX = SerialInputReader.Instance.GetTiltValue(); 
        }

        Vector3 move = new Vector3(inputX * moveSpeed * Time.deltaTime, 0, 0);
        transform.Translate(move);
    }
}


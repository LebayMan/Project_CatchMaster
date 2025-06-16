using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatcherInput : MonoBehaviour
{
    public enum InputMode { Arduino, DebugKeyboard }
    public InputMode inputMode = InputMode.DebugKeyboard;

    public float moveSpeed = 5f;
    public float gyroSensitivity = 1.0f; // Since value already 0-1, no need to reduce
    public float deadZone = 0.05f;

    private float inputX;
    private SerialController serialController;
    public float minX = -5f;  // Maximum left position
public float maxX = 5f;   // Maximum right position


    void Start()
    {
        if (inputMode == InputMode.Arduino)
        {
            serialController =  GetComponent<SerialController>();
            if (serialController == null)
            {
                Debug.LogError("SerialController not found!");
            }
        }
    }

    void Update()
{
    if (inputMode == InputMode.DebugKeyboard)
    {
        inputX = Input.GetAxis("Horizontal");
    }
    else if (inputMode == InputMode.Arduino)
    {
        string message = serialController.ReadSerialMessage();

        if (!string.IsNullOrEmpty(message) && message.StartsWith("GYRO:"))
        {
            if (float.TryParse(message.Substring(5), out float gyroValue))
            {
                gyroValue = Mathf.Clamp01(gyroValue); // Ensure value is between 0–1
                float centeredInput = (gyroValue - 0.5f) * 2f; // Map 0–1 to -1 to +1
                inputX = Mathf.Abs(centeredInput) > deadZone ? centeredInput * gyroSensitivity : 0f;
            }
        }
    }

    Vector3 move = new Vector3(inputX * moveSpeed * Time.deltaTime, 0, 0);
    transform.Translate(move);

    // Clamp catcher position between minX and maxX
    Vector3 clampedPos = transform.position;
    clampedPos.x = Mathf.Clamp(clampedPos.x, minX, maxX);
    transform.position = clampedPos;
}

}

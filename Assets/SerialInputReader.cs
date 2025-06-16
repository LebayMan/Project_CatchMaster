using System;
using System.IO.Ports;
using System.Threading;
using UnityEngine;

public class SerialController : MonoBehaviour
{
    [Header("Serial Port Settings")]
    public string portName = "COM3";     // Update this to match your Arduino COM port
    public int baudRate = 115200;

    private SerialPort serialPort;
    private Thread readThread;
    private string latestMessage = null;
    private bool isRunning = false;

    void Start()
    {
        try
        {
            serialPort = new SerialPort(portName, baudRate);
            serialPort.ReadTimeout = 100;
            serialPort.Open();

            isRunning = true;
            readThread = new Thread(ReadSerial);
            readThread.Start();

            Debug.Log("Serial port opened successfully on " + portName);
        }
        catch (Exception e)
        {
            Debug.LogError("Failed to open serial port: " + e.Message);
        }
    }

    private void ReadSerial()
    {
        while (isRunning)
        {
            try
            {
                string message = serialPort.ReadLine();
                lock (this)
                {
                    latestMessage = message;
                }
            }
            catch (TimeoutException)
            {
                // Ignore timeout exceptions
            }
            catch (Exception e)
            {
                Debug.LogError("Serial read error: " + e.Message);
            }
        }
    }
    public void CallScore()
    {
        serialPort.WriteLine("score");
    }
        public void LoseScore()
    {
        serialPort.WriteLine("Lose");
    }
    public string ReadSerialMessage()
    {
        lock (this)
        {
            string message = latestMessage;
            latestMessage = null;
            return message;
        }
    }

    public void SendSerialMessage(string message)
    {
        if (serialPort != null && serialPort.IsOpen)
        {
            try
            {
                serialPort.WriteLine(message);
            }
            catch (Exception e)
            {
                Debug.LogError("Failed to send serial message: " + e.Message);
            }
        }
    }

    void OnApplicationQuit()
    {
        isRunning = false;
        if (readThread != null && readThread.IsAlive)
        {
            readThread.Join();
        }

        if (serialPort != null && serialPort.IsOpen)
        {
            serialPort.Close();
        }
    }
}

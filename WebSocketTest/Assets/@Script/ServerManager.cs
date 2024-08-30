using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WebSocketSharp;

public class ServerManager : MonoBehaviour
{
    public Button _connectButton;
    public Button _sendButton;
    public Button _closeButton;
    public TMP_InputField _inputField;
    public TMP_Text _logText;

    WebSocket ws;

    private void Start()
    {
        _logText.text = string.Empty;
        _sendButton.onClick.AddListener(() => Send());
        _connectButton.onClick.AddListener(() => Connect());
        _closeButton.onClick.AddListener(() => Close());
    }

    private void Connect()
    {
        UnityMainThreadDispatcher.Instance();
        ws = new WebSocket("ws://localhost:7777");

        ws.Connect();
        ws.OnMessage += Call;
        
    }

    private void Close()
    {
        ws.OnMessage -= Call;
        ws.Close();
        ws = null;
    }

    private void Call(object sender, MessageEventArgs e)
    {
        try
        {
            var msg = e.Data + "\n";
            Debug.Log(msg);
            UnityMainThreadDispatcher.Instance().Enqueue(() => _logText.text += msg);
        }
        catch(Exception ex)
        {
            Debug.LogError(ex);
        }

        
    }

    private void Send()
    {
        ws.Send(_inputField.text);
    }
}

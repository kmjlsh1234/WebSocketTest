using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WebSocketSharp;

public class ServerManager : MonoBehaviour
{
    public Button _sendButton;
    public TMP_Text _logText;

    WebSocket ws;

    private void Start()
    {
        _logText.text = string.Empty;
        _sendButton.onClick.AddListener(() => Send());
        ws = new WebSocket("ws://localhost:7777");

        ws.Connect();
        ws.OnMessage += Call;
    }

    private void Call(object sender, MessageEventArgs e)
    {
        var msg = ((WebSocket)sender).Url + "\n" + e.Data;
        _logText.text += msg;
    }

    private void Send()
    {
        ws.Send("shinano");
    }
}

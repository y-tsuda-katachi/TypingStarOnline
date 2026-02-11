using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using NativeWebSocket;
using UnityEngine;

/// <summary>
/// サーバーとWebSocketでメッセージの送受信を行う静的クラス
/// </summary>
public class WebSocketController : MonoBehaviour
{
    private WebSocket webSocket;
    public Action<MatchMessage> OnProgressMessage { get; set; }
    public Action<MatchMessage> OnGameResultMessage { get; set; }

    private async void Awake()
    {
        var path = Path.Combine(Application.streamingAssetsPath, Constants.StreamingAssets.SERVER_SETTINGS);
        var serverSettings = await ServerSettingsLoader.GetServerSettings(path, false);
        var webSocketHandler = new WebSocketHandler(serverSettings);
        webSocket = new WebSocket(
            webSocketHandler.GetWsUri(
                Constants.API.WebSocket.BASE_KEY,
                Constants.API.WebSocket.MATCH_MESSAGE)
            .AbsoluteUri,
            new Dictionary<string, string>()
            {
                {
                    Constants.API.WebSocket.HeaderName.MATCH_ID,
                    Prefs.LastMatch.matchId
                }
            });
        webSocket.OnMessage += OnMessage;
        await webSocket.Connect();
    }

    private void Update()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        webSocket?.DispatchMessageQueue();
#endif
    }

    public async void SendMatchMessage(MatchMessage msg)
    {
        if (webSocket.State != WebSocketState.Open)
            return;
        var json = JsonUtility.ToJson(msg);
        await webSocket.Send(Encoding.UTF8.GetBytes(json));
    }

    private void OnMessage(byte[] msg)
    {
        var matchMessage = JsonUtility.FromJson<MatchMessage>(Encoding.UTF8.GetString(msg));
        switch (matchMessage.type)
        {
            case MatchMessageType.ProgressMessage:
                OnProgressMessage?.Invoke(matchMessage);
                break;
            case MatchMessageType.GameResultMessage:
                OnGameResultMessage?.Invoke(matchMessage);
                break;
        }
    }

    private void OnDestroy()
    {
        webSocket?.Close();
    }
}
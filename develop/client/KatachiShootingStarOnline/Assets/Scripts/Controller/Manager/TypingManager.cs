using System;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// タイピング入力管理クラス
/// </summary>
public class TypingManager : MonoBehaviour
{
    private TypingController typingInputController;
    private WebSocketController webSocketController;
    private TypingWord[] typingWords;
    private GeneralInput generalInput;
    private int currentIndex = 0;
    private int maxIndex = 0;

    public Action OnComplete { get; set; }
    public Action OnFailureInput { get; set; }
    public Action<string, float> OnSendProgress { get; set; }

    private async void Awake()
    {
        typingInputController = FindAnyObjectByType<TypingController>();
        webSocketController = FindAnyObjectByType<WebSocketController>();

        typingWords = await TypingWordGenerator.GenerateFromCSV(Prefs.LastMatch.matchId);
        generalInput = new GeneralInput();
        maxIndex = typingWords.Length - 1;

        typingInputController.InputAction = generalInput.UI.Input;
        typingInputController.OnFailureInput += OnFailureInput;
    }

    private void Update()
    {
        var controllerState = typingInputController.CurrentState;
        switch (controllerState)
        {
            case TypingController.State.Waiting:
                var isCompleted = TryMoveToNext();
                if (isCompleted) // 全部のワードが終了した
                    OnComplete();
                else // ワードが残っている
                    StartListening();
                break;
            case TypingController.State.Listening:
                var isFinished = typingInputController.TryListen();
                if (isFinished)
                    StartWaiting();
                break;
        }
    }

    /// <summary>
    /// 次のワードに進む
    /// </summary>
    /// <returns>全部のワードが終了したかどうか</returns>
    private bool TryMoveToNext()
    {
        if (currentIndex > maxIndex)
            // 全ワード終了したらゲーム終了
            return true;

        SendProgress();

        var typingWord = typingWords[currentIndex++]; // 現在のタイピングワードを取得して次へ
        typingInputController.SetTypingWord(typingWord);

        return false;
    }

    private void SendProgress()
    {
        var playerId = Prefs.Player.playerId;
        var progress = (float)Math.Round((float)currentIndex / maxIndex, 3);

        webSocketController.SendMatchMessage(new MatchMessage
        {
            type = MatchMessageType.ProgressMessage,
            playerId = playerId,
            message = JsonUtility.ToJson(new ProgressMessage
            {
                progress = progress
            }),
        });

        OnSendProgress(playerId, progress);
    }

    public void StartWaiting()
    {
        generalInput.Disable();
        typingInputController.GetWaiting();
    }

    public void StartListening()
    {
        generalInput.Enable();
        typingInputController.GetListening();
    }

    public void StartFinishing()
    {
        generalInput.Dispose();
        typingInputController.GetFinishing();
    }
}
using System.IO;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ゲーム全体の管理クラス
/// </summary>
public class GameManager : MonoBehaviour
{
    private TypingManager typingManager;
    private GameController gameController;
    private WebSocketController webSocketController;
    public GameInspector gameInspector;

    private void Awake()
    {
        typingManager = FindAnyObjectByType<TypingManager>();
        gameController = FindAnyObjectByType<GameController>();
        webSocketController = FindAnyObjectByType<WebSocketController>();

        gameInspector = new GameInspector();

        typingManager.OnComplete += GameClear;
        typingManager.OnFailureInput += gameInspector.IncrementFailureCount;
        typingManager.OnSendProgress += gameController.UpdatePlayerIcon;
        webSocketController.OnProgressMessage += OnProgressMessage;
    }

    private void Start() => StartGame();
    private void LateUpdate()
    {
        var gameState = gameController.CurrentState;
        if (gameState == GameState.Playing)
        {
            gameController.UpdateTimeLabel(
                gameInspector.GetElapssedMilliSeconds());
        }
    }

    /// <summary>
    /// ゲーム開始の流れ
    /// </summary>
    public async void StartGame()
    {
        gameController.GetInitializing();
        // 開始演出
        await gameController.InitializeGame(Prefs.LastMatch);
        // ゲーム開始
        gameController.GetPlaying();
        // タイピング開始
        typingManager.StartWaiting();
        // タイム計測開始
        gameInspector.StartWatching();
    }

    /// <summary>
    /// ゲームクリア時の処理
    /// </summary>
    public async void GameClear()
    {
        gameController.GetGameClear();
        await EndGame();
    }

    private void SaveResult(GameResult gameResult)
    {
        var player = Prefs.Player;
        player.gameResult = gameResult;
        Prefs.Player = player;
    }

    /// <summary>
    /// ゲーム終了の流れ
    /// </summary>
    public async Task EndGame()
    {
        gameController.GetFinalizing();
        // タイピング終了
        typingManager.StartFinishing();
        // タイム計測終了
        gameInspector.StopWatching();
        // 結果まとめ
        SaveResult(gameInspector.GetGameResult());
        // 終了演出
        await gameController.FinalizeGame();
        await SceneManager.LoadSceneAsync(Constants.Scene.RESULT);
    }

    private void OnProgressMessage(MatchMessage msg)
    {
        var progressMsg = JsonUtility.FromJson<ProgressMessage>(msg.message);
        gameController.UpdatePlayerIcon(msg.playerId, progressMsg.progress);
    }
}
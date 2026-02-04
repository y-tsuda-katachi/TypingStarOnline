using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// ゲーム全体の制御クラス
/// </summary>
public class GameController : MonoBehaviour
{
    private GamePresenter gamePresenter;
    public GameState CurrentState { get; private set; } = GameState.Initializing;

    private void Awake()
    {
        gamePresenter = FindAnyObjectByType<GamePresenter>();
    }

    /// <summary>
    /// ゲームの開始時処理の流れ
    /// </summary>
    public async Task InitializeGame(Match match)
    {
        gamePresenter.SetPlayerIcons(match.players);
        await gamePresenter.Annouce(Constants.Label.GAME_START);
    }

    /// <summary>
    /// ゲームの終了時処理の流れ
    /// </summary>
    public async Task FinalizeGame()
    {
        await gamePresenter.Annouce(Constants.Label.GAME_FINISHED);
    }

    public void UpdateTimeLabel(float elapsedMilliSeconds)
    {
        gamePresenter.SetTimeLabel(elapsedMilliSeconds);
    }

    public void UpdatePlayerIcon(string playerId, float progress)
    {
        gamePresenter.UpdatePlayerIcon(playerId, progress);
    }

    public void GetInitializing()
    {
        gamePresenter.InitUI();
        gamePresenter.ShowGameLabel();
        gamePresenter.ShowPlayerIconPanel();
        CurrentState = GameState.Initializing;
    }

    public void GetPlaying()
    {
        gamePresenter.HideGameLabel();
        gamePresenter.ShowTimeLabel();
        CurrentState = GameState.Playing;
    }

    public void GetGameClear()
    {
        gamePresenter.InitUI();
        gamePresenter.HideTimeLabel();
        gamePresenter.ShowGameLabel();
        CurrentState = GameState.GameClear;
    }

    public void GetFinalizing()
    {
        gamePresenter.InitUI();
        gamePresenter.HidePlayerIconPanel();
        gamePresenter.ShowGameLabel();
        CurrentState = GameState.Finalizing;
    }
}
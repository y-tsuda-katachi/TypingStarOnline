using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// マッチ画面全体の管理クラス
/// </summary>
public class MatchManager : MonoBehaviour
{
    private MatchController matchController;

    private void Awake()
    {
        matchController = FindAnyObjectByType<MatchController>();
    }

    private void Start() => LoadMatches();
    public async void LoadMatches()
    {
        matchController.GetLoading();
        var matches = await matchController.GetMatches(Prefs.Player);
        // TODO: テキトーに待たせる
        await UniTask.WaitForSeconds(3);
        matchController.ShowMatches(matches, OnMatchButtonClicked);
        matchController.GetLoaded();
    }

    private async Task UpdateLastMatch()
    {
        Prefs.LastMatch = await matchController.GetMatch(Prefs.LastMatch);
    }

    private async Task GoTyping()
    {
        await SceneManager.LoadSceneAsync(Constants.Scene.TYPING);
    }

    public async void StartMatch()
    {
        await matchController.StartMatch(Prefs.Player, Prefs.LastMatch);
        await UpdateLastMatch();
        await GoTyping();
    }

    public async void ExitMatch()
    {
        await matchController.Exit(Prefs.Player, Prefs.LastMatch);
        matchController.GetInitialized();
        LoadMatches();
    }

    public async void GoBackTitle()
    {
        await SceneManager.LoadSceneAsync(Constants.Scene.TITLE);
    }

    /// <summary>
    /// マッチボタンが押下されたときの処理
    /// </summary>
    private async void OnMatchButtonClicked(MatchButton matchButton)
    {
        matchController.GetWaiting();
        Prefs.LastMatch = matchButton.Match;
        await matchController.Enter(Prefs.Player, Prefs.LastMatch);
        await UpdateLastMatch();
        await GoTyping();
    }
}

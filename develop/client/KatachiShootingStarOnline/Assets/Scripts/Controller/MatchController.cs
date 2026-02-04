using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
public class MatchController : MonoBehaviour
{
    private MatchPresenter matchPresenter;
    public MatchSceneState CurrentState { get; private set; } = MatchSceneState.Initialized;

    private void Awake()
    {
        matchPresenter = FindAnyObjectByType<MatchPresenter>();
    }

    public void ShowMatches(List<Match> matches, Action<MatchButton> action)
    {
        matchPresenter.ClearAllMatchButton();
        matchPresenter.CreateMatchButtons(matches, action);
    }
    public async Task<List<Match>> GetMatches(Player player)
    {
        return await REST.GetAllMatch(player);
    }

    public async Task<Match> GetMatch(Match match)
    {
        return await REST.GetMatch(Prefs.Player, Prefs.LastMatch);
    }

    public async Task<bool> Enter(Player player, Match match)
    {
        return await REST.EnterMatch(player, match);
    }

    public async Task<bool> Exit(Player player, Match match)
    {
        return await REST.ExitMatch(player, match);
    }

    public async Task<bool> StartMatch(Player player, Match match)
    {
        return await REST.StartMatch(player, match);
    }

    public void GetInitialized()
    {
        matchPresenter.HideWaitingPanel();
        CurrentState = MatchSceneState.Initialized;
    }
    public void GetLoading()
    {
        matchPresenter.HideMatchesPanel();
        matchPresenter.ShowLoadingPanel();
        CurrentState = MatchSceneState.Loading;
    }

    public void GetLoaded()
    {
        matchPresenter.HideLoadingPanel();
        matchPresenter.ShowMatchesPanel();
        CurrentState = MatchSceneState.Loaded;
    }

    public void GetWaiting()
    {
        matchPresenter.HideMatchesPanel();
        matchPresenter.ShowWaitingPanel();
        CurrentState = MatchSceneState.Waiting;
    }
    public void GetStarted()
    {
        matchPresenter.HideWaitingPanel();
        CurrentState = MatchSceneState.Started;
    }
}
using System;
using System.Collections.Generic;
using UnityEngine;

public class MatchPresenter : MonoBehaviour
{
    [SerializeField] private GameObject _loadingPanel;
    [SerializeField] private GameObject _matchesPanel;
    [SerializeField] private GameObject _waitingPanel;
    [SerializeField] private GameObject _contentRoot;
    [SerializeField] private GameObject _matchButtonPrefab;
    private List<MatchButton> matchButtons;

    private void Awake()
    {
        if (_loadingPanel) _loadingPanel.SetActive(false);
        if (_matchesPanel) _matchesPanel.SetActive(false);
        if (_waitingPanel) _waitingPanel.SetActive(false);
    }

    public void CreateMatchButtons(List<Match> matches, Action<MatchButton> action)
    {
        var tempList = new List<MatchButton>();
        matches.ForEach(match =>
        {
            var matchButton =
                Instantiate(
                    _matchButtonPrefab,
                    _contentRoot.transform,
                    false)
                .GetComponent<MatchButton>()
                .Init(match)
                .SetOnClickCB(action);

            tempList.Add(matchButton);
        });
        matchButtons = tempList;
    }

    public void ClearAllMatchButton()
    {
        matchButtons?.ForEach(button => Destroy(button.gameObject));
    }

    public void ShowLoadingPanel() { if (_loadingPanel) _loadingPanel.SetActive(true); }
    public void HideLoadingPanel() { if (_loadingPanel) _loadingPanel.SetActive(false); }
    public void ShowMatchesPanel() { if (_matchesPanel) _matchesPanel.SetActive(true); }
    public void HideMatchesPanel() { if (_matchesPanel) _matchesPanel.SetActive(false); }
    public void ShowWaitingPanel() { if (_waitingPanel) _waitingPanel.SetActive(true); }
    public void HideWaitingPanel() { if (_waitingPanel) _waitingPanel.SetActive(false); }
}
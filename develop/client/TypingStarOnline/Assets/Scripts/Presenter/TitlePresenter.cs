using System;
using TMPro;
using UnityEngine;

public class TitlePresenter : MonoBehaviour
{
    [SerializeField] private TMP_InputField _playerNameInput;
    [SerializeField] private TextMeshProUGUI _connectionLabel;

    private void Awake()
    {
        if (_connectionLabel) _connectionLabel.text = string.Empty;
    }

    public string GetInputPlayerName()
    {
        return _playerNameInput?.text;
    }

    public void SetInputPlayerName(string playerName)
    {
        _playerNameInput.text = playerName;
    }

    public void SetConnectionLabel(string connectionText)
    {
        if (_connectionLabel == false) return;
        switch (connectionText)
        {
            case "UP":
                _connectionLabel.text = Constants.Label.SERVER_UP;
                break;
            case "DOWN":
            default:
                _connectionLabel.text = Constants.Label.SERVER_DOWN;
                break;
        }
    }

    public void ShowConnectionLabel() { if (_connectionLabel) _connectionLabel.enabled = true; }
    public void HideConnectionLabel() { if (_connectionLabel) _connectionLabel.enabled = false; }
}
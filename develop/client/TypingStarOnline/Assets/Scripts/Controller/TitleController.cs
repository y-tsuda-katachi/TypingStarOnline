using System.IO;
using System.Threading.Tasks;
using UnityEngine;

public class TitleController : MonoBehaviour
{
    private TitlePresenter titlePresenter;

    private void Awake() =>
        titlePresenter = FindAnyObjectByType<TitlePresenter>();

    public async Task InitREST() => 
        await REST.Initialize();

    public void SetLastPlayer(Player player) =>
        titlePresenter.SetInputPlayerName(player.playerName);

    public async void UpdateConnection()
    {
        var health = await REST.CheckHealth();
        titlePresenter.SetConnectionLabel(health?.status);
    }

    public async Task<Player> Connect()
    {
        var player = new Player
        {
            playerName = titlePresenter.GetInputPlayerName()
        };

        return await REST.ConnectPlayer(player);
    }

    public async Task<bool> Disconnect(Player player) => 
        await REST.RemovePlayer(player);
}
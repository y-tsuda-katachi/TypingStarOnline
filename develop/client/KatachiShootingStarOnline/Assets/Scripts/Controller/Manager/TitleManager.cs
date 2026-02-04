using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// タイトルシーン管理クラス
/// </summary>
public class TitleManager : MonoBehaviour
{
    private TitleController titleController;

    private void Awake()
    {
        titleController = FindAnyObjectByType<TitleController>();
    }

    private async void Start()
    {
        if (Prefs.Player != null)
            titleController.SetLastPlayer(Prefs.Player);
        await titleController.InitREST();
        titleController.UpdateConnection();
    }

    public async void EnterGame()
    {
        var player = await titleController.Connect();
        Prefs.Player = player;
        await SceneManager.LoadSceneAsync(Constants.Scene.MATCH);
    }

    public async void ExitGame()
    {
        if (Prefs.Player != null)
            await titleController.Disconnect(Prefs.Player);
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
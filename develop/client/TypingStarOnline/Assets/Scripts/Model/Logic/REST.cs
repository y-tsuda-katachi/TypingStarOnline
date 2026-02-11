using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// サーバーからRESTでデータの受け渡しを行う静的クラス
/// </summary>
public class REST
{
    private static RestHandler restHandler;

    public static async Task Initialize()
    {
        var path = Path.Combine(Application.streamingAssetsPath, Constants.StreamingAssets.SERVER_SETTINGS);
        var serverSettings = await ServerSettingsLoader.GetServerSettings(path, true);
        restHandler = new RestHandler(serverSettings);
    }

    #region アクチュエータ機能

    /// <summary>
    /// サーバーのヘルスチェック
    /// </summary>
    /// <returns>ヘルス情報</returns>
    public static async Task<Health> CheckHealth()
    {
        var uri = restHandler.GetHttpUri(
            Constants.API.Actuator.BASE_KEY,
            Constants.API.Actuator.HEALTH_KEY
        );
        return await restHandler.GetJson<Health>(uri);
    }

    #endregion

    #region プレイヤー機能

    /// <summary>
    /// プレイヤーを登録する
    /// </summary>
    /// <returns>登録したプレイヤー情報 or null</returns>
    public static async Task<Player> ConnectPlayer(Player player)
    {
        var uri = restHandler.GetHttpUri(
            Constants.API.Player.BASE_KEY,
            Constants.API.Player.CONNECT_KEY,
            player);
#if UNITY_EDITOR
        Debug.Log($"Sending : {uri}");
#endif
        return await restHandler.GetJson<Player>(uri);
    }

    /// <summary>
    /// プレイヤーを削除する
    /// </summary>
    /// <returns>削除したプレイヤー情報 or null</returns>
    public static async Task<bool> RemovePlayer(Player player)
    {
        var uri = restHandler.GetHttpUri(
            Constants.API.Player.BASE_KEY,
            Constants.API.Player.DISCONNECT_KEY,
            player);
#if UNITY_EDITOR
        Debug.Log($"Sending : {uri}");
#endif
        return await restHandler.Get(uri);
    }

    #endregion

    #region マッチ機能

    /// <summary>
    /// すべてのマッチを取得する
    /// </summary>
    /// <returns>マッチ一覧</returns>
    public static async Task<List<Match>> GetAllMatch(Player player)
    {
        var uri = restHandler.GetHttpUri(
            Constants.API.Match.BASE_KEY,
            Constants.API.Match.GET_ALL_KEY,
            player);
        return (await restHandler.GetArrayJson<JsonHelper<Match>>(uri)).root;
    }

    /// <summary>
    /// マッチを取得する
    /// </summary>
    /// <returns>マッチ</returns>
    public static async Task<Match> GetMatch(Player player, Match match)
    {
        var uri = restHandler.GetHttpUri(
            Constants.API.Match.BASE_KEY,
            Constants.API.Match.GET_KEY,
            player,
            match);
        return await restHandler.GetJson<Match>(uri);
    }

    /// <summary>
    /// マッチへ参加する
    /// </summary>
    /// <returns>参加できたかどうか</returns>
    public static async Task<bool> EnterMatch(Player player, Match match)
    {
        var uri = restHandler.GetHttpUri(
            Constants.API.Match.BASE_KEY,
            Constants.API.Match.ENTER_KEY,
            player,
            match);
        return await restHandler.Get(uri);
    }

    /// <summary>
    /// マッチから退出する
    /// </summary>
    /// <returns>退出できたかどうか</returns>
    public static async Task<bool> ExitMatch(Player player, Match match)
    {
        var uri = restHandler.GetHttpUri(
            Constants.API.Match.BASE_KEY,
            Constants.API.Match.EXIT_KEY,
            player,
            match);
        return await restHandler.Get(uri);
    }

    /// <summary>
    /// マッチを開始する
    /// </summary>
    /// <returns>開始できたかどうか</returns>
    public static async Task<bool> StartMatch(Player player, Match match)
    {
        var uri = restHandler.GetHttpUri(
            Constants.API.Match.BASE_KEY,
            Constants.API.Match.START_KEY,
            player,
            match);
        return await restHandler.Get(uri);
    }

    #endregion

    #region リザルト機能

    /// <summary>
    /// ゲーム結果をサーバーにポストする
    /// </summary>
    /// <returns>ポストが成功したかどうか</returns>
    public static async Task<bool> PostResult(Player player, Match match)
    {
        var uri = restHandler.GetHttpUri(
            Constants.API.Result.BASE_KEY,
            Constants.API.Result.POST_KEY,
            player,
            match);
        return await restHandler.Post(uri, player.gameResult);
    }

    #endregion
}
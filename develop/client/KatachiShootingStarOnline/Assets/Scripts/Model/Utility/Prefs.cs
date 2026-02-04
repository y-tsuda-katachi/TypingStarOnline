using UnityEngine;

public class Prefs
{
    /// <summary>
    /// プレイヤーデータ
    /// </summary>
    public static Player Player
    {
        get => GetDeserialized<Player>(Constants.PlayerPrefsKey.PLAYER);
        set => SetSerialized(Constants.PlayerPrefsKey.PLAYER, value);
    }

    /// <summary>
    /// 最後に参加したマッチ
    /// </summary>
    public static Match LastMatch
    {
        get => GetDeserialized<Match>(Constants.PlayerPrefsKey.LAST_MATCH);
        set => SetSerialized(Constants.PlayerPrefsKey.LAST_MATCH, value);
    }

    /// <summary>
    /// JSON形式からオブジェクトを取得
    /// </summary>
    /// <typeparam name="T">オブジェクト型</typeparam>
    /// <param name="key">PlayerPrefsに設定しているキー</param>
    private static T GetDeserialized<T>(string key)
    {
        var json = PlayerPrefs.GetString(key, null);
        return json != null ? JsonUtility.FromJson<T>(json) : default(T);
    }

    /// <summary>
    /// オブジェクトをJSON形式で保存
    /// </summary>
    /// <typeparam name="T">オブジェクト型</typeparam>
    /// <param name="key">PlayerPrefsに設定するキー</param>
    /// <param name="value">オブジェクト</param>
    private static void SetSerialized<T>(string key, T value)
    {
        var json = JsonUtility.ToJson(value);
        PlayerPrefs.SetString(key, json);
    }
}
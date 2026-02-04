using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class RestHandler
{
    /// <summary>
    /// サーバー設定
    /// </summary>
    protected ServerSettings serverSettings;
    /// <summary>
    /// HTTPリクエスト先となる基底のURI
    /// </summary>
    private Uri httpBaseUri;
    public RestHandler(ServerSettings serverSettings)
    {
        this.serverSettings = serverSettings;
        Init();
    }

    /// <summary>
    /// サーバー設定などの初期化
    /// </summary>
    protected virtual void Init()
    {
        httpBaseUri = new UriBuilder(
            serverSettings.HttpProtocol,
            serverSettings.ServerLocation,
            serverSettings.Port)
            .Uri;
    }

    /// <summary>
    /// URIからJSON形式でデータを受け取る
    /// </summary>
    public async Task<T> GetJson<T>(Uri uri)
    {
        var www = UnityWebRequest.Get(uri);
        await www.SendWebRequest();
        var json = www.downloadHandler.text;
#if UNITY_EDITOR
        Debug.Log($"Send to {uri}");
        Debug.Log($"Gets {json}");
#endif
        return JsonUtility.FromJson<T>(json); ;
    }

    /// <summary>
    /// URIからJSON形式で配列データを受け取る
    /// </summary>
    public async Task<T> GetArrayJson<T>(Uri uri)
    {
        var www = UnityWebRequest.Get(uri);
        await www.SendWebRequest();
        var json = $"{{\"root\":{www.downloadHandler.text}}}";
#if UNITY_EDITOR
        Debug.Log($"Send to {uri}");
        Debug.Log($"Gets {json}");
#endif
        return JsonUtility.FromJson<T>(json);
    }

    /// <summary>
    /// URIからstring形式でデータを受け取る
    /// </summary>
    public async Task<bool> Get(Uri uri)
    {
        var www = UnityWebRequest.Get(uri);
        await www.SendWebRequest();
        var isDone = www.isDone;
#if UNITY_EDITOR
        Debug.Log($"Sent to {uri}");
        Debug.Log($"Gets {isDone}");
#endif
        return isDone;
    }

    /// <summary>
    /// URIにJSON形式でデータを送信して結果を受け取る
    /// </summary>
    /// <typeparam name="T">変換する型</typeparam>
    /// <param name="uri">送信先</param>
    /// <param name="data">送信するデータ</param>
    /// <returns>stringポストが成功したかどうか</returns>
    public async Task<bool> Post<T>(Uri uri, T data)
    {
        var json = JsonUtility.ToJson(data);
        var bytes = Encoding.UTF8.GetBytes(json);
        var www = new UnityWebRequest(uri, WebRequestMethods.Http.Post)
        {
            uploadHandler = new UploadHandlerRaw(bytes),
            downloadHandler = new DownloadHandlerBuffer(),
        };
        www.SetRequestHeader("Content-Type", "application/json");

        await www.SendWebRequest();
        var text = www.downloadHandler.text;
#if UNITY_EDITOR
        Debug.Log($"Sent to {uri}");
        Debug.Log($"Gets {text}");
#endif
        return Convert.ToBoolean(text);
    }

    /// <summary>
    /// 基底キーと関係キーからHTTP用URIを作る
    /// </summary>
    /// <param name="baseKey"></param>
    /// <param name="relativeKey"></param>
    /// <param name="replaceables"></param>
    /// <returns></returns>
    public Uri GetHttpUri(string baseKey,
        string relativeKey,
        params IReplaceable[] replaceables)
    {
        var request = GetRequest(
            baseKey,
            relativeKey);

        var relativeUri = GetReplacedEndpoint(
            request.Endpoint,
            request.Parameters,
            replaceables);

        return new Uri(httpBaseUri, relativeUri);
    }

    /// <summary>
    /// サーバー設定からリクエスト情報を取得する
    /// </summary>
    /// <param name="baseKey">ベースとなるAPIキー</param>
    /// <param name="relativeKey">詳細なAPIキー</param>
    /// <returns>見つかったリクエスト情報</returns>
    protected Request GetRequest(string baseKey, string relativeKey)
    {
        return serverSettings.ApiList
            .First(api => api.Name.Equals(baseKey))
            .AvailableRequests
            .First(req => req.Name.Equals(relativeKey));
    }

    /// <summary>
    /// エンドポイントに使われるパラメータを実際の値に置き換える
    /// </summary>
    /// <param name="rawEndpoint">元のエンドポイント</param>
    /// <param name="parameters">パラメータ</param>
    /// <param name="obj">置き換えに使われるオブジェクト</param>
    /// <returns>置き換えた後のエンドポイント</returns>
    protected string GetReplacedEndpoint(string rawEndpoint,
        List<Parameter> parameters,
        params IReplaceable[] replaceables)
    {
        var replacedEndpoint = rawEndpoint;
        parameters?.ForEach(parameter =>
        {
            replacedEndpoint = replacedEndpoint.Replace(
                parameter.Placeholder,
                replaceables.First(obj => obj
                .HasStrValue(parameter.Placeholder))
                .GetStrValue(parameter.Placeholder));
        });
        return replacedEndpoint;
    }
}
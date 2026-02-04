using System;

public class WebSocketHandler : RestHandler
{
    /// <summary>
    /// WSリクエスト先となる基底のURI
    /// </summary>
    private Uri wsBaseUri;

    public WebSocketHandler(ServerSettings serverSettings) : base(serverSettings)
    {
        wsBaseUri = new UriBuilder(
            serverSettings.WsProtocol,
            serverSettings.ServerLocation,
            serverSettings.Port)
            .Uri;
    }

    /// <summary>
    /// 基底キーと関係キーからWS用URIを作る
    /// </summary>
    /// <param name="baseKey"></param>
    /// <param name="relativeKey"></param>
    /// <param name="replaceables"></param>
    /// <returns></returns>
    public Uri GetWsUri(string baseKey, string relativeKey)
    {
        var request = GetRequest(
            baseKey,
            relativeKey);

        var relativeUri = GetReplacedEndpoint(
            request.Endpoint,
            parameters: default);

        return new Uri(wsBaseUri, relativeUri);
    }
}
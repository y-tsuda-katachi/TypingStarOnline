using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ServerSettings
{
    [SerializeField] private string httpProtocol;
    [SerializeField] private string wsProtocol;
    [SerializeField] private string serverLocation;
    [SerializeField] private int port;
    [SerializeField] private List<Api> apiList;

    public string HttpProtocol { get => httpProtocol; }
    public string WsProtocol { get => wsProtocol; }
    public string ServerLocation { get => serverLocation; }
    public int Port { get => port; }
    public List<Api> ApiList { get => apiList; }
}
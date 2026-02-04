using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Request
{
    [SerializeField] private string name;
    [SerializeField] private string method;
    [SerializeField] private string endpoint;
    [SerializeField] private List<Parameter> parameters;

    public string Name { get => name; }
    public string Method { get => method; }
    public string Endpoint { get => endpoint; }
    public List<Parameter> Parameters { get => parameters; }
}
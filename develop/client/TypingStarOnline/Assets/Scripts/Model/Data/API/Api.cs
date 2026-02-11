using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Api
{
    [SerializeField] private string name;
    [SerializeField] private List<Request> availableRequests;

    public string Name { get => name; }
    public List<Request> AvailableRequests { get => availableRequests; }
}
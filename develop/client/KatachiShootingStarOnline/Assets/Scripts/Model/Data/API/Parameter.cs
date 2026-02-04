using System;
using UnityEngine;

[Serializable]
public class Parameter
{
    [SerializeField] private string placeholder;

    public string Placeholder { get => placeholder; }
}
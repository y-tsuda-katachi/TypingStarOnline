using UnityEngine;

[CreateAssetMenu(fileName = "TextColor", menuName = "Scriptable Objects/TextColor")]
public class TextColor : ScriptableObject
{
    [SerializeField] private Color32 color;
    
    public Color32 Color { get => color; }
}

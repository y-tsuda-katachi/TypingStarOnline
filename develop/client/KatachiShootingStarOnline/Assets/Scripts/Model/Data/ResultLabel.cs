using TMPro;
using UnityEngine;

public class ResultLabel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _prefixLabel;
    [SerializeField] private TextMeshProUGUI _valueLabel;
    [SerializeField] private TextMeshProUGUI _suffixLabel;

    public string Value { get => _valueLabel?.text; }
    
    public void InitValueLabel(string valueText)
    {
        if (_valueLabel) _valueLabel.text = valueText;
    }
}
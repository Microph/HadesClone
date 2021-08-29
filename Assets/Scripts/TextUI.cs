using UnityEngine;
using TMPro;

public class TextUI : MonoBehaviour
{    
    private TextMeshProUGUI textComponent;

    private void Awake()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateText(string v)
    {
        textComponent.text = v;
    }
}

using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;

[RequireComponent(typeof(TMP_Text))]
public class CopyTextByButton : MonoBehaviour
{
    private TMP_Text textBox;

    private void Awake()
    {
        textBox = GetComponent<TMP_Text>();
    }

    public void CopyToClipboard()
    { 
        string textToCopy = textBox.text;
        string copiedTextWithoutTags = Regex.Replace(textToCopy, "<.*?>", string.Empty);
        GUIUtility.systemCopyBuffer = copiedTextWithoutTags;
        Debug.Log("Copied to Clipboard" + textBox.text);
    }
}

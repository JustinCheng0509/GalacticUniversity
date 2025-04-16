using UnityEngine;
using TMPro;

public class UILeaderboardEntryController : MonoBehaviour
{
    [SerializeField] private TMP_Text _positionText;
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _scoreText;

    public void SetEntry(int position, string name, int score)
    {
        _positionText.text = position.ToString();
        _nameText.text = name;
        _scoreText.text = score.ToString();
    }

    public void SetHighlightColor(Color color)
    {
        _positionText.color = color;
        _nameText.color = color;
        _scoreText.color = color;
    }

    public void SetHighlightColor(string hexColor)
    {
        if (ColorUtility.TryParseHtmlString(hexColor, out Color color))
        {
            SetHighlightColor(color);
        }
        else
        {
            Debug.LogError($"Invalid color string: {hexColor}");
        }
    }
}

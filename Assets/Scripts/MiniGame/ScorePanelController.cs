using TMPro;
using UnityEngine;

public class ScorePanelController : MonoBehaviour
{
    
    [SerializeField] private GameObject gameEndPanel;

    [SerializeField] private TMP_Text baseScoreText;
    [SerializeField] private TMP_Text damageDealtScoreText;
    [SerializeField] private TMP_Text dangersDestroyedScoreText;
    [SerializeField] private TMP_Text damageTakenScoreText;
    [SerializeField] private TMP_Text timesDeadScoreText;
    [SerializeField] private TMP_Text todayScoreText;
    [SerializeField] private TMP_Text previousTotalScoreText;
    [SerializeField] private TMP_Text newTotalScoreText;

    public void UpdateScorePanel(int baseScore, int damageDealtScore, int dangersDestroyedScore, int damageTakenScore, int timesDeadScore)
    {
        baseScoreText.text = baseScore.ToString();
        damageDealtScoreText.text = damageDealtScore.ToString();
        dangersDestroyedScoreText.text = dangersDestroyedScore.ToString();
        damageTakenScoreText.text = damageTakenScore.ToString();
        timesDeadScoreText.text = timesDeadScore.ToString();
        int todayScore = baseScore + damageDealtScore + dangersDestroyedScore + damageTakenScore + timesDeadScore;
        todayScoreText.text = todayScore.ToString();
        // Check PlayerPrefs for saved data
        int previousTotalScore = 0;
        if (PlayerPrefs.HasKey("totalScore"))
        {
            previousTotalScore = PlayerPrefs.GetInt("totalScore");
        }
        previousTotalScoreText.text = previousTotalScore.ToString();
        int newTotalScore = previousTotalScore + todayScore;
        newTotalScoreText.text = newTotalScore.ToString();
    }
}

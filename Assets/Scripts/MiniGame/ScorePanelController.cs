using TMPro;
using UnityEngine;

public class ScorePanelController : MonoBehaviour
{
    [SerializeField] private TMP_Text dayCompleteText;
    [SerializeField] private TMP_Text baseScoreText;
    [SerializeField] private TMP_Text damageDealtScoreText;
    [SerializeField] private TMP_Text dangersDestroyedScoreText;
    [SerializeField] private TMP_Text damageTakenScoreText;
    [SerializeField] private TMP_Text timesDeadScoreText;
    [SerializeField] private TMP_Text todayScoreText;
    [SerializeField] private TMP_Text previousTotalScoreText;
    [SerializeField] private TMP_Text newTotalScoreText;
    [SerializeField] private PlayerShipInfo playerShipInfo;

    public void UpdateScorePanel(int baseScore, int damageDealtScore, int dangersDestroyedScore, int damageTakenScore, int timesDeadScore)
    {
        dayCompleteText.text = "Day " + playerShipInfo.gameData.currentDay + " Complete!";
        baseScoreText.text = baseScore.ToString();

        damageDealtScoreText.text = damageDealtScore.ToString();
        dangersDestroyedScoreText.text = dangersDestroyedScore.ToString();  
        playerShipInfo.gameData.totalDestructionScore = playerShipInfo.gameData.totalDestructionScore + damageDealtScore + dangersDestroyedScore;

        damageTakenScoreText.text = "- " + damageTakenScore.ToString();
        timesDeadScoreText.text = "- " + timesDeadScore.ToString();
        playerShipInfo.gameData.totalSafetyScore = playerShipInfo.gameData.totalSafetyScore + damageTakenScore + timesDeadScore;

        int todayScore = damageDealtScore + dangersDestroyedScore - damageTakenScore - timesDeadScore;
        todayScoreText.text = todayScore.ToString();
        
        int previousTotalScore = playerShipInfo.gameData.totalScore;
        previousTotalScoreText.text = previousTotalScore.ToString();

        int newTotalScore = baseScore + previousTotalScore + todayScore;
        playerShipInfo.gameData.totalScore = newTotalScore;
        newTotalScoreText.text = newTotalScore.ToString();
    }
}

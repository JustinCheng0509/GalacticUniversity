using UnityEngine;

public class TutorialController : MonoBehaviour
{
    public Tutorial startTutorial;

    public Tutorial classTutorial;

    public Tutorial classTimeTutorial;

    public Tutorial needTutorial;

    public Tutorial skillTutorial;

    public Tutorial homeworkTutorial;

    public Tutorial workTutorial;

    public Tutorial leaderboardTutorial;

    public Tutorial shopTutorial;

    public Tutorial shipControlTutorial;

    private Tutorial currentTutorial;

    [SerializeField] private GameObject tutorialPanel;

    public void ShowTutorial(Tutorial tutorial)
    {
        currentTutorial = tutorial;
        Time.timeScale = 0;
        // Find the Title child of the tutorialPanel
        tutorialPanel.transform.Find("Title").GetComponent<TMPro.TextMeshProUGUI>().text = tutorial.title;
        // Find the Description child of the tutorialPanel
        tutorialPanel.transform.Find("Description").GetComponent<TMPro.TextMeshProUGUI>().text = tutorial.description;
        tutorialPanel.SetActive(true);
    }
    
    public void HideTutorial()
    {
        Time.timeScale = 1;
        tutorialPanel.SetActive(false);
        if ((currentTutorial == shopTutorial || currentTutorial == skillTutorial) && PlayerPrefs.GetInt("NeedTutorial", 0) == 0)
        {
            PlayerPrefs.SetInt("NeedTutorial", 1);
            ShowTutorial(needTutorial);
        } else if ((currentTutorial == homeworkTutorial || currentTutorial == workTutorial || currentTutorial == leaderboardTutorial) && PlayerPrefs.GetInt("SkillTutorial", 0) == 0 && StaticValues.USE_SKILL_SYSTEM)
        {
            PlayerPrefs.SetInt("SkillTutorial", 1);
            ShowTutorial(skillTutorial);        
        } else if (currentTutorial == shipControlTutorial && PlayerPrefs.GetInt("LeaderboardTutorial", 0) == 0)
        {
            PlayerPrefs.SetInt("LeaderboardTutorial", 1);
            ShowTutorial(leaderboardTutorial);

        } else {
            currentTutorial = null;
        }
    }
}

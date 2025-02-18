using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OverworldUIController : MonoBehaviour
{
    [SerializeField] private OverworldTimeController overworldTimeController;

    [SerializeField] private PlayerInfo playerInfo;

    [SerializeField] private TMP_Text attendanceText;

    [SerializeField] private GameObject classStartWarningPanel;

    [SerializeField] private TMP_Text homeworkText;

    [SerializeField] private TMP_Text moneyText;

    [SerializeField] private TMP_Text dayText;

    [SerializeField] private GameObject bottomRightPanel;

    [SerializeField] private TMP_Text maneuverabilityText;

    [SerializeField] private TMP_Text destructionText;

    [SerializeField] private TMP_Text mechanicsText;


    public void UpdateHomeworkProgress(float homeworkProgress)
    {
        homeworkText.text = $"Homework Progress: {homeworkProgress}%";
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // If not using skill system, remove this
        if (!GameConstants.USE_SKILL_SYSTEM)
        {
            bottomRightPanel.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameConstants.USE_SKILL_SYSTEM) {
            maneuverabilityText.text = playerInfo.gameData.maneuverability.ToString();
            destructionText.text = playerInfo.gameData.destruction.ToString();
            mechanicsText.text = playerInfo.gameData.mechanics.ToString();
        }

        if (overworldTimeController.canAttendClass && playerInfo.GetAttendanceStatus() != AttendanceStatus.ATTENDED)
        {
            classStartWarningPanel.SetActive(true);
        }
        else
        {
            classStartWarningPanel.SetActive(false);
        }

        dayText.text = "Day " + playerInfo.gameData.currentDay;
        string attendanceStatus = "Not Started";
        if (playerInfo.GetAttendanceStatus() == AttendanceStatus.ATTENDED)
        {
            attendanceStatus = "Attended";
        } else if (playerInfo.GetAttendanceStatus() == AttendanceStatus.ABSENT)
        {
            attendanceStatus = "Absent";
        }
        homeworkText.text = "Homework Progress: " + playerInfo.GetHomeworkProgress() + "%";
        attendanceText.text = "Attendance: " + attendanceStatus;
        moneyText.text = playerInfo.gameData.money.ToString();
    }
}

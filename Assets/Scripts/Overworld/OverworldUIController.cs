using TMPro;
using UnityEngine;

public class OverworldUIController : MonoBehaviour
{
    [SerializeField]
    private OverworldTimeController overworldTimeController;

    [SerializeField]
    private PlayerInfo playerInfo;

    [SerializeField]
    private GameObject classNotificationPanel;

    [SerializeField]
    private GameObject classPanel;

    [SerializeField]
    private TMP_Text attendanceText;

    [SerializeField]
    private TMP_Text homeworkText;

    public void UpdateHomeworkProgress(float homeworkProgress)
    {
        homeworkText.text = $"Homework Progress: {homeworkProgress}%";
    }

    public void ToggleClassPanel()
    {
        classPanel.SetActive(!classPanel.activeSelf);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (overworldTimeController.canAttendClass && !playerInfo.dailyGrade.attendance)
        {
            classNotificationPanel.SetActive(true);
        }
        else
        {
            classNotificationPanel.SetActive(false);
        }
        attendanceText.text = "Today's attendance: " + (playerInfo.dailyGrade.attendance ? "Attended" : "Absent/Not Started");
    }
}

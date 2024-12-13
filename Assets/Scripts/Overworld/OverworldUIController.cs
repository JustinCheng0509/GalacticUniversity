using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    [SerializeField]
    private GameObject taskPanel;

    public Toggle checkoutClassRoomToggle;
    public Toggle talkToNPCToggle;
    public Toggle checkoutShopToggle;
    public Toggle checkoutDormToggle;
    public Toggle attendClassToggle;
    public Toggle doHomeworkToggle;


    public void UpdateHomeworkProgress(float homeworkProgress)
    {
        homeworkText.text = $"Homework Progress: {homeworkProgress}%";
    }

    public void ToggleClassPanel()
    {
        classPanel.SetActive(!classPanel.activeSelf);
    }

    public void ToggleTaskPanel()
    {
        taskPanel.SetActive(!taskPanel.activeSelf);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        // Check the toggle
        if (PlayerPrefs.HasKey("introClassPlayed")) {
            checkoutClassRoomToggle.isOn = true;
        }
        if (PlayerPrefs.HasKey("introShopPlayed")) {
            checkoutShopToggle.isOn = true;
        }
        if (PlayerPrefs.HasKey("introDormPlayed")) {
            checkoutDormToggle.isOn = true;
        }
        if (PlayerPrefs.HasKey("introNPCTalked")) {
            talkToNPCToggle.isOn = true;
        }
        if (PlayerPrefs.HasKey("homeworkProgress") && PlayerPrefs.GetFloat("homeworkProgress") >= 100) {
            doHomeworkToggle.isOn = true;
        }
        if (PlayerPrefs.HasKey("attendance") && PlayerPrefs.GetInt("attendance") == 1) {
            attendClassToggle.isOn = true;
        }
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
        attendClassToggle.isOn = playerInfo.dailyGrade.attendance;
        doHomeworkToggle.isOn = playerInfo.dailyGrade.homeworkProgress >= 100;
    }
}

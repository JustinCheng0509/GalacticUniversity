using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OverworldUIController : MonoBehaviour
{
    [SerializeField] private OverworldTimeController overworldTimeController;

    [SerializeField] private PlayerInfo playerInfo;

    [SerializeField] private TMP_Text attendanceText;

    [SerializeField] private TMP_Text homeworkText;

    [SerializeField] private TMP_Text moneyText;

    // public Toggle checkoutClassRoomToggle;
    // public Toggle talkToNPCToggle;
    // public Toggle checkoutShopToggle;
    // public Toggle checkoutDormToggle;
    // public Toggle attendClassToggle;
    // public Toggle doHomeworkToggle;


    public void UpdateHomeworkProgress(float homeworkProgress)
    {
        homeworkText.text = $"Homework Progress: {homeworkProgress}%";
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        // Check the toggle
        // if (PlayerPrefs.HasKey("introClassPlayed")) {
        //     checkoutClassRoomToggle.isOn = true;
        // }
        // if (PlayerPrefs.HasKey("introShopPlayed")) {
        //     checkoutShopToggle.isOn = true;
        // }
        // if (PlayerPrefs.HasKey("introDormPlayed")) {
        //     checkoutDormToggle.isOn = true;
        // }
        // if (PlayerPrefs.HasKey("introNPCTalked")) {
        //     talkToNPCToggle.isOn = true;
        // }
        // if (playerInfo.GetHomeworkProgress() >= 100) {
        //     doHomeworkToggle.isOn = true;
        // }
        // if (playerInfo.GetAttendanceStatus() == AttendanceStatus.ATTENDED) {
        //     attendClassToggle.isOn = true;
        // }
    }

    // Update is called once per frame
    void Update()
    {
        string attendanceStatus = "Not Started";
        if (playerInfo.GetAttendanceStatus() == AttendanceStatus.ATTENDED)
        {
            attendanceStatus = "Attended";
        } else if (playerInfo.GetAttendanceStatus() == AttendanceStatus.ABSENT)
        {
            attendanceStatus = "Absent";
        }
        attendanceText.text = "Attendance: " + attendanceStatus;
        moneyText.text = playerInfo.gameData.money.ToString();
    }
}

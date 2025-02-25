using TMPro;
using UnityEngine;

public class OverworldUIAttendanceController : MonoBehaviour
{
    [SerializeField] private TMP_Text _attendanceText;

    private GameDataManager _gameDataManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _gameDataManager = FindAnyObjectByType<GameDataManager>();
        _gameDataManager.OnAttendanceUpdated += HandleAttendanceUpdate;

        HandleAttendanceUpdate(_gameDataManager.Attendance);
    }

    // Update is called once per frame
    void HandleAttendanceUpdate(AttendanceStatus status)
    {
        string statusString;
        switch (status)
        {
            case AttendanceStatus.ATTENDED:
                statusString = "Attended";
                break;
            case AttendanceStatus.ABSENT:
                statusString = "Absent";
                break;
            default:
                statusString = "Not Started";
                break;
        }
        _attendanceText.text = "Attendance: " + statusString;
    }
}

using TMPro;
using UnityEngine;

public class OverworldUIAttendanceController : MonoBehaviour
{
    [SerializeField] private TMP_Text _attendanceText;

    private OverworldPlayerStatusController _overworldPlayerStatusController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _overworldPlayerStatusController = FindFirstObjectByType<OverworldPlayerStatusController>();
        _overworldPlayerStatusController.OnAttendanceUpdated += HandleAttendanceUpdate;
    }

    // Update is called once per frame
    void HandleAttendanceUpdate(AttendanceStatus status)
    {
        _attendanceText.text = status.ToString();
    }
}

using UnityEngine;

public class OverworldUIController : MonoBehaviour
{
    [SerializeField]
    private OverworldTimeController overworldTimeController;

    [SerializeField]
    private PlayerInfo playerInfo;

    [SerializeField]
    private GameObject classNotificationPanel;
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
    }
}

using UnityEngine;

public class OverworldTriggerZoneController : MonoBehaviour
{
    private QuestController _questController;

    private void Start()
    {
        _questController = FindAnyObjectByType<QuestController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) {
            if (gameObject.tag == GameConstants.TRIGGER_TAG_DORM) {
                _questController.TryReturnQuest(QuestIDs.QUEST_DORM_INTRO);
            }
        }
    }
}

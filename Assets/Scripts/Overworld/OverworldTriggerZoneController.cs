using UnityEngine;

public class OverworldTriggerZoneController : MonoBehaviour
{
    private QuestController _questController;

    private void Start()
    {
        _questController = FindAnyObjectByType<QuestController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    
        // Debug.Log("Triggered");
        // Debug.Log(collision.tag);
        // Debug.Log(gameObject.tag);

        //plays amb when enter room trigger zone 

       { if (collision.CompareTag("Player"))
        {
            AudioSource audioSource = GetComponent<AudioSource>();
            if (audioSource != null && !audioSource.isPlaying)  // Check if audio isn't already playing
            {
                audioSource.Play();
            }
        
        if (collision.CompareTag("Player")) {
            if (gameObject.tag == GameConstants.TRIGGER_TAG_DORM) {
                _questController.TryReturnQuest(QuestIDs.QUEST_INTRO_DORM);
            } else if (gameObject.tag == GameConstants.TRIGGER_TAG_CLASS) {
                _questController.TryReturnQuest(QuestIDs.QUEST_INTRO_CLASSROOM);
            } else if (gameObject.tag == GameConstants.TRIGGER_TAG_PLAYROOM) {
                _questController.TryReturnQuest(QuestIDs.QUEST_INTRO_PLAY_ROOM);
            } else if (gameObject.tag == GameConstants.TRIGGER_TAG_WORK) {
                _questController.TryReturnQuest(QuestIDs.QUEST_INTRO_WORK);
            } else if (gameObject.tag == GameConstants.TRIGGER_TAG_SHOP) {
                _questController.TryReturnQuest(QuestIDs.QUEST_INTRO_SHOP);
            }
        
        }
    }
       }
    
    //stops amb once leave trigger zone

      private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AudioSource audioSource = GetComponent<AudioSource>();
            if (audioSource != null && audioSource.isPlaying)  // checks if audio is playing
            {
                audioSource.Stop();
            }
}

    
}
       }
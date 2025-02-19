// using TMPro;
// using UnityEngine;
// using UnityEngine.InputSystem;

// public class OverworldInteract : MonoBehaviour
// {
//     [SerializeField] private GameObject interactPromptPanel;

//     [SerializeField] private TMP_Text interactPromptText;

//     [SerializeField] private GameObject interactStatusPanel;

//     [SerializeField] private TMP_Text interactStatusText;
    
//     public InputActionReference interact;

//     private string interactableTag = "";

//     [SerializeField] private PlayerInfo playerInfo;

//     [SerializeField] private AudioSource schoolBell;

//     [SerializeField] private OverworldSwitchScene overworldSwitchScene;

//     [SerializeField] private DialogController dialogController;

//     [SerializeField] private OverworldTimeController overworldTimeController;

//     [SerializeField] private OverworldUIController overworldUIController;

//     [SerializeField] private GameDataManager gameDataManager;

//     void Update() {
//         if (playerInfo.isSleeping) {
//             interactPromptPanel.SetActive(false);
//             interactStatusPanel.SetActive(true);
//             interactStatusText.text = "Sleeping...";
//         } else if (playerInfo.isDoingHomework) {
//             interactPromptPanel.SetActive(false);
//             interactStatusPanel.SetActive(true);
//             interactStatusText.text = "Homework: " + playerInfo.GetHomeworkProgress() + "%";
//         } else if (playerInfo.isWorking) {
//             interactPromptPanel.SetActive(false);
//             interactStatusPanel.SetActive(true);
//             interactStatusText.text = "Working...";
//         } else if (playerInfo.isPlaying) {
//             interactPromptPanel.SetActive(false);
//             interactStatusPanel.SetActive(true);
//             interactStatusText.text = "Playing...";
//         } else {
//             interactStatusPanel.SetActive(false);
//         }
//         if (interact.action.triggered) {
//             if (playerInfo.IsBusy()) {
//                 playerInfo.CancelActions();
//             } else if (interactableTag != "") {
//                 StartInteraction(interactableTag);
//             }
//         }
//     }

//     private void OnTriggerEnter2D(Collider2D other) {
//         // Check if the layer of the object is Interactable
//         if (other.gameObject.layer == LayerMask.NameToLayer(GameConstants.INTERACTABLE_LAYER) && !playerInfo.IsBusy()) {  
//             interactPromptPanel.SetActive(true);
//             interactPromptText.text = GetPromptText(other.gameObject.tag);
//             interactableTag = other.gameObject.tag;
//         }
//     }

//     private void OnTriggerStay2D(Collider2D other) {
//         if (other.gameObject.layer == LayerMask.NameToLayer(GameConstants.INTERACTABLE_LAYER) && !playerInfo.IsBusy()) {
//             interactPromptPanel.SetActive(true);
//             interactPromptText.text = GetPromptText(other.gameObject.tag);
//             interactableTag = other.gameObject.tag;
//         }
//     }

//     private void OnTriggerExit2D(Collider2D other) {
//         if (other.gameObject.layer == LayerMask.NameToLayer(GameConstants.INTERACTABLE_LAYER)) {
//             interactPromptPanel.SetActive(false);
//             interactPromptText.text = "";
//             interactableTag = "";
//         }
//     }

//     private string GetPromptText(string tag) {
//         switch (tag) {
//             case var value when value == GameConstants.INTERACTABLE_TAG_CLASS:
//                 return "(E) Start class";
//             case var value when value == GameConstants.INTERACTABLE_TAG_SLEEP:
//                 return "(E) Sleep";
//             case var value when value == GameConstants.INTERACTABLE_TAG_HOMEWORK:
//                 return "(E) Do homework";
//             case var value when value == GameConstants.INTERACTABLE_TAG_NPC:
//                 return "(E) Chat";
//             case var value when value == GameConstants.INTERACTABLE_TAG_SHOP:
//                 return "(E) Buy food";
//             case var value when value == GameConstants.INTERACTABLE_TAG_PLAY:
//                 return "(E) Play";
//             case var value when value == GameConstants.INTERACTABLE_TAG_WORK:
//                 return "(E) Work";
//             default:
//                 return "";
//         }
//     }

//     private void StartInteraction(string tag) {
//         switch (tag) {
//             case var value when value == GameConstants.INTERACTABLE_TAG_CLASS:
//                 Debug.Log("Interacting with class");
//                 StartClass();
//                 break;
//             case var value when value == GameConstants.INTERACTABLE_TAG_SLEEP:
//                 Debug.Log("Interacting with sleep");
//                 StartSleep();
//                 break;
//             case var value when value == GameConstants.INTERACTABLE_TAG_HOMEWORK:
//                 Debug.Log("Interacting with homework");
//                 StartHomework();
//                 break;
//             case var value when value == GameConstants.INTERACTABLE_TAG_WORK:
//                 Debug.Log("Interacting with work");
//                 StartWorking();
//                 break;
//             case var value when value == GameConstants.INTERACTABLE_TAG_SHOP:
//                 BuyFood();
//                 break;
//             case var value when value == GameConstants.INTERACTABLE_TAG_PLAY:
//                 Debug.Log("Interacting with play");
//                 StartPlay();
//                 break;
//             case var value when value == GameConstants.INTERACTABLE_TAG_NPC:
//                 Debug.Log("Interacting with NPC");
//                 dialogController.SetCurrentDialogs(dialogController.npcDialogs);
//                 PlayerPrefs.SetInt("introNPCTalked", 1);
//                 // overworldUIController.talkToNPCToggle.isOn = true;
//                 break;
//             default:
//                 Debug.Log("No interaction found");
//                 break;
//         }
//     }

//     private void StartPlay()
//     {
//         playerInfo.isPlaying = true;
//     }

//     private void StartClass()
//     {
//         if (playerInfo.GetAttendanceStatus() == AttendanceStatus.ATTENDED)
//         {
//             dialogController.SetCurrentDialogs(dialogController.alreadyAttendedDialogs);
//             return;
//         }

//         if (!overworldTimeController.canAttendClass)
//         {
//             dialogController.SetCurrentDialogs(dialogController.notClassTimeDialogs);
//             return;
//         }
        
//         // Set attendance to true
//         playerInfo.SetAttendanceStatus(AttendanceStatus.ATTENDED);
//         // Play the school bell sound
//         schoolBell.Play();
//         // Save the game data
//         gameDataManager.SaveGameData(playerInfo.gameData);
//         // Open the mini-game scene
//         overworldSwitchScene.FadeOutScene(GameConstants.SCENE_MINIGAME);
//     }
    
//     private void BuyFood()
//     {
//         if (playerInfo.gameData.money < 50)
//         {
//             dialogController.SetCurrentDialogs(dialogController.notEnoughMoneyDialogs);
//             return;
//         }

//         if (playerInfo.gameData.hunger >= 90)
//         {
//             dialogController.SetCurrentDialogs(dialogController.alreadyFull);
//             return;
//         }
        
//         playerInfo.gameData.money -= 50;
//         playerInfo.gameData.hunger += 30;
//         if (playerInfo.gameData.hunger > 100)
//         {
//             playerInfo.gameData.hunger = 100;
//         }
//     }

//     private void StartSleep()
//     {
//         // Set the player to sleep
//         playerInfo.isSleeping = true;
//     }

//     private void StartHomework()
//     {
//         if (playerInfo.GetHomeworkProgress() >= 100)
//         {
//             dialogController.SetCurrentDialogs(dialogController.homeworkDone);
//             return;
//         }
//         if (playerInfo.gameData.hunger < 20)
//         {
//             dialogController.SetCurrentDialogs(dialogController.tooHungry);
//             return;
//         }
//         if (playerInfo.gameData.energy < 20)
//         {
//             dialogController.SetCurrentDialogs(dialogController.tooTiredDialogs);
//             return;
//         }
//         if (playerInfo.gameData.mood < 20)
//         {
//             dialogController.SetCurrentDialogs(dialogController.tooStressed);
//             return;
//         }
//         // Set the player to do homework
//         playerInfo.isDoingHomework = true;
//     }

//     private void StartWorking()
//     {
//         if (playerInfo.gameData.hunger < 20)
//         {
//             dialogController.SetCurrentDialogs(dialogController.tooHungry);
//             return;
//         }
//         if (playerInfo.gameData.energy < 20)
//         {
//             dialogController.SetCurrentDialogs(dialogController.tooTiredDialogs);
//             return;
//         }
//         if (playerInfo.gameData.mood < 20)
//         {
//             dialogController.SetCurrentDialogs(dialogController.tooStressed);
//             return;
//         }
//         // Set the player to work
//         playerInfo.isWorking = true;
//     }
// }

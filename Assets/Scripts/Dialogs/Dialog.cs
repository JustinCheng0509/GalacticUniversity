using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialog", menuName = "Scriptable Objects/Dialog")]
public class Dialog : ScriptableObject
{
  public string dialogID;
  public string characterName;
  public bool isLeft;
  public string text;
  public bool isSelfDialog;
  public Dialog nextDialog;  
  public List<Tutorial> associatedTutorials;
  public List<Quest> associatedQuests; 
}

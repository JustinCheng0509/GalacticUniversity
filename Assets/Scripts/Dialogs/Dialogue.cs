using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "Scriptable Objects/Dialogue")]
public class Dialogue : ScriptableObject
{
  // public string characterName;
  public bool isLeft;
  public string text;
  public bool isSelfDialogue;   
}

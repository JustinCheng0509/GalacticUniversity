using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    public float gameDuration = 90f;
    public Text timerText;

    void Update()
    {
        gameDuration -= Time.deltaTime;
        timerText.text = $"Time: {Mathf.Ceil(gameDuration)}";

        if (gameDuration <= 0)
        {
            EndGame();
        }
    }

    void EndGame()
    {
        Time.timeScale = 0;
        Debug.Log("Game Over");
    }
}

using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    private GameDataManager _gameDataManager;

    private List<Tutorial> _tutorials = new List<Tutorial>();

    private List<Tutorial> _currentTutorials = new List<Tutorial>();
    private int _currentTutorialIndex = 0;

    [SerializeField] private GameObject tutorialPanel;
    [SerializeField] private TMP_Text buttonText;

    public event Action OnTutorialCompleted;

    void Start()
    {
        _gameDataManager = FindAnyObjectByType<GameDataManager>();
        // Add all tutorials from the resources folder to the list
        _tutorials.AddRange(Resources.LoadAll<Tutorial>("Tutorials")); 
    }

    public void ShowTutorial(Tutorial tutorial)
    {
        Debug.Log("Showing tutorial: " + tutorial.title);
        // if the tutorial is already completed, do not show it again
        if (_gameDataManager.IsTutorialCompleted(tutorial.tutorialID)) return;

        _gameDataManager.CompleteTutorial(tutorial.tutorialID);
        _currentTutorials.Add(tutorial);
        _currentTutorialIndex = 0;
        Time.timeScale = 0;
        // Find the Title child of the tutorialPanel
        tutorialPanel.transform.Find("Title").GetComponent<TMPro.TextMeshProUGUI>().text = tutorial.title;
        // Find the Description child of the tutorialPanel
        tutorialPanel.transform.Find("Description").GetComponent<TMPro.TextMeshProUGUI>().text = tutorial.description;

        buttonText.text = "OK";
        
        ShowTutorialUI();
    }

    // Overload for showing a list of tutorials
    public void ShowTutorial(List<Tutorial> tutorials)
    {
        // Remove all tutorials that are already completed from the list
        tutorials.RemoveAll(tutorial => _gameDataManager.IsTutorialCompleted(tutorial.tutorialID));
        
        // If there are no tutorials left, return
        if (tutorials.Count == 0) return;

        // Add all tutorials to the completed list
        foreach (var tutorial in tutorials)
        {
            _gameDataManager.CompleteTutorial(tutorial.tutorialID);
        }
        _currentTutorials = tutorials;
        _currentTutorialIndex = 0;
        Time.timeScale = 0;

        // Find the Title child of the tutorialPanel
        tutorialPanel.transform.Find("Title").GetComponent<TMPro.TextMeshProUGUI>().text = tutorials[0].title;
        // Find the Description child of the tutorialPanel
        tutorialPanel.transform.Find("Description").GetComponent<TMPro.TextMeshProUGUI>().text = tutorials[0].description;

        ShowTutorialUI();

        // If there is only one tutorial, set the button text to OK, otherwise set it to Next
        buttonText.text = tutorials.Count == 1 ? "OK" : "Next";
    }

    // Overload for showing a tutorial by ID
    public void ShowTutorial(string tutorialID)
    {
        var tutorial = _tutorials.Find(t => t.tutorialID == tutorialID);
        if (tutorial != null)
        {
            ShowTutorial(tutorial);
        }
    }

    // Overload for showing a list of tutorials by ID
    public void ShowTutorial(List<string> tutorialIDs)
    {
        var tutorials = _tutorials.FindAll(t => tutorialIDs.Contains(t.tutorialID));
        ShowTutorial(tutorials);
    }
    
    public void NextTutorial()
    {
        _currentTutorialIndex++;
        if (_currentTutorialIndex >= _currentTutorials.Count)
        {
            HideTutorial();
            return;
        }
        // Find the Title child of the tutorialPanel
        tutorialPanel.transform.Find("Title").GetComponent<TMPro.TextMeshProUGUI>().text = _currentTutorials[_currentTutorialIndex].title;
        // Find the Description child of the tutorialPanel
        tutorialPanel.transform.Find("Description").GetComponent<TMPro.TextMeshProUGUI>().text = _currentTutorials[_currentTutorialIndex].description;

        buttonText.text = _currentTutorialIndex == _currentTutorials.Count - 1 ? "OK" : "Next";
    }

    
    public void HideTutorial()
    {
        Time.timeScale = 1;
        OnTutorialCompleted?.Invoke();
        HideTutorialUI();
        _currentTutorials.Clear();
        _currentTutorialIndex = 0;
    }

    public void ShowTutorialUI()
    {
        tutorialPanel.gameObject.SetActive(true);
    }

    public void HideTutorialUI()
    {
        tutorialPanel.gameObject.SetActive(false);
    }
}

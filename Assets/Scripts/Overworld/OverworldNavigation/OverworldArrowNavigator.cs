using UnityEngine;
using UnityEngine.AI;

public class OverworldArrowNavigator : MonoBehaviour
{
    [SerializeField] private Transform _player;
    private Transform _target;
    [SerializeField] private Transform _arrow;

    private OverworldUIQuestColumnController _questColumnController;
    private QuestController _questController;

    private NavMeshPath _path;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _path = new NavMeshPath();
        _questColumnController = FindAnyObjectByType<OverworldUIQuestColumnController>();
        _questController = FindAnyObjectByType<QuestController>();
        if (_questColumnController != null)
        {
            _questColumnController.OnQuestSelected += ShowArrowNavigator;
        }
    }

    private void ShowArrowNavigator(Quest quest)
    {
        HideArrowNavigator();
        if (quest == null)
        {
            return;
        }
        if(_questController.CheckQuestCompletion(quest) && quest.navigationCompleteTag != null && quest.navigationCompleteTag != "")
        {
            ShowArrowNavigator(quest.navigationCompleteTag);
        } else if (quest.navigationTag != null && quest.navigationTag != "")
        {
            ShowArrowNavigator(quest.navigationTag);
        }
    }

    private void HideArrowNavigator()
    {
        _target = null;
        _arrow.gameObject.SetActive(false);
    }

    public void ShowArrowNavigator(string targetTag)
    {
        GameObject targetObject = GameObject.FindGameObjectWithTag(targetTag);
        // Debug.Log("Target Object: " + targetObject);
        // Debug.Log("Target Tag: " + targetTag);
        if (targetObject != null)
        {
            _target = targetObject.transform;
        }
        _arrow.gameObject.SetActive(true);
    }

    void Update()
    {
        if (_target == null || _player == null || _arrow == null)
        {
            return;
        }
        // Calculate a new path
        if (NavMesh.CalculatePath(_player.position, _target.position, NavMesh.AllAreas, _path))
        {
            // Debug.Log("Found path length: " + _path.corners.Length);
            if (_path.corners.Length > 1)
            {
                Vector3 nextPoint = _path.corners[1]; // First corner after player position
                Vector3 direction = (nextPoint - _player.position).normalized;

                // Rotate arrow towards the direction of the next waypoint
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                _arrow.rotation = Quaternion.Euler(0, 0, angle);
            }
        }
    }
}

using UnityEngine;
using UnityEngine.AI;

public class OverworldArrowNavigator : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform target;
    [SerializeField] private Transform arrow;

    private NavMeshPath path;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        path = new NavMeshPath();
    }

    public void ShowArrowNavigator(string targetTag)
    {
        GameObject targetObject = GameObject.FindGameObjectWithTag(targetTag);
        if (targetObject != null)
        {
            target = targetObject.transform;
        }
        
    }

    void Update()
    {
        // Calculate a new path
        if (NavMesh.CalculatePath(player.position, target.position, NavMesh.AllAreas, path))
        {
            if (path.corners.Length > 1)
            {
                Vector3 nextPoint = path.corners[1]; // First corner after player position
                Vector3 direction = (nextPoint - player.position).normalized;

                // Rotate arrow towards the direction of the next waypoint
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                arrow.rotation = Quaternion.Euler(0, 0, angle);
            }
        }
    }
}

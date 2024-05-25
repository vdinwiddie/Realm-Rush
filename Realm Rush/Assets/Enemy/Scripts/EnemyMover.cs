using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Enemy))]

public class EnemyMover : MonoBehaviour
{
    List<Waypoint> path = new List<Waypoint>();
    public List<Waypoint> Pathing { get { return path; } }
    [SerializeField] [Range(0f, 5f)] float speed = 1f;

    Enemy enemy;

    // Start is called before the first frame update
    void OnEnable()
    {
        FindPath();
        MoveToStart();
        StartCoroutine(FollowPath());
    }

    private void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FindPath()
    {
        path.Clear();

        GameObject parent = GameObject.FindGameObjectWithTag("Path");
        foreach (Transform child in parent.transform)
        {
            Waypoint waypoint = child.GetComponent<Waypoint>();

            if (waypoint != null)
            {
                path.Add(waypoint);
            }
        }
    }

    void MoveToStart()
    {
        transform.position = path[0].transform.position;
    }

    void finishPath()
    {
        gameObject.SetActive(false);
        enemy.EnemySurvive();
    }

    IEnumerator FollowPath()
    {
        foreach (Waypoint waypoint in path)
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = waypoint.transform.position;
            float travelPercent = 0f;

            transform.LookAt(endPosition);

            while (travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
                yield return new WaitForEndOfFrame();
            }
        }

        finishPath();
    }
}

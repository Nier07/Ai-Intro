using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiMovement : MonoBehaviour
{
    public Transform player;
    public float chaseDistance;
    //an array of Transforms symbolised by []
    public List<Transform> waypoints;
    public int waypointIndex = 0;
    public bool chasing = false;

    public float minGoalDist = 0.1f;
    public float speed = 1.5f;

    public GameObject wapointPrefab;
    public int spawnFreq = 3;

    private void Start()
    {
        StartCoroutine(AddWaypoint());
    }


    /*void Update()
    {
        //if within player chase distance chace player else move to goal
        if (Vector2.Distance(transform.position, player.position) < chaseDistance)
        {
            chasing = true;
            AIMoveToward(player.position);
        }
        else
        {
            //moves to goal
            WaypointUpdate();
            AIMoveToward(waypoints[waypointIndex].position);
        }

        //Vector2.MoveTowards(transform.position, position[positionIndex].transform.position, 1 * Time.deltaTime);

    }*/
    public IEnumerator AddWaypoint()
    {
        while (true)
        {
            int interator = 0;
            yield return new WaitForSeconds(5);
            while (spawnFreq > interator)
            {
                interator++;
                yield return new WaitForSeconds(0.25f);
                GameObject newWaypoint = Instantiate(wapointPrefab, new Vector2(Random.Range(-5f, 5f), Random.Range(-4f, 4f)), Quaternion.identity);
                waypoints.Add(newWaypoint.transform);
            }
        }
    }

    public void AIMoveToward(Transform GoalPos)
    {
        Vector2 AiPosition = transform.position;

        //if the AI is not near the goal then dont move
        if (Vector2.Distance(AiPosition, GoalPos.position) > minGoalDist)
        {
            //direction form A to B
            // is B - A

            // Normalize converts a direction and makes the length of it = 1
            // if normalize wasnt here would make object move faster if it was further away and slower when closer
            Vector2 directionToPos0 = GoalPos.position - transform.position;
            directionToPos0.Normalize();
            transform.position += (Vector3)directionToPos0 * speed * Time.deltaTime;
        }
    }
    public void WaypointUpdate()
    {
        Vector2 AiPosition = transform.position;

        //if the AI is near the goal 
        if (Vector2.Distance(AiPosition, waypoints[waypointIndex].position) < minGoalDist)
        {
            //increment by 1
            Transform destroy = waypoints[waypointIndex];
            waypoints.RemoveAt(waypointIndex);
            Destroy(destroy.gameObject);
            waypointIndex++;
        }

        int closePoint = 0;
        float dist = 0f;

        // for each element in waypoints
        for (int i = 0; i < waypoints.Count; i++)
        {
            //creates a temporary distance to storewaypoint data
            float tempDist = Vector2.Distance(AiPosition, waypoints[i].transform.position);

            //if the temp dist is less than dist then set temp dist to dist
            if ((tempDist < dist || dist == 0))
            {
                dist = tempDist;
                closePoint = i;
            }
        }
            
        waypointIndex = closePoint;
        chasing = false;
 
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiMovement : MonoBehaviour
{
    public Transform player;
    public float chaseDistance;
    //an array of Transforms symbolised by []
    public Transform[] waypoints;
    public int waypointIndex = 0;

    public float minGoalDist = 0.1f;
    public float speed = 1.5f;

    void Update()
    {
        //if within player chase distance chace player else move to goal
        if (Vector2.Distance(transform.position, player.position) < chaseDistance)
        {

            AIMoveToward(player.position);
        }
        else
        {
            //moves to goal
            WaypointUpdate();
            AIMoveToward(waypoints[waypointIndex].position);
        }

        //Vector2.MoveTowards(transform.position, position[positionIndex].transform.position, 1 * Time.deltaTime);

    }

    private void AIMoveToward(Vector3 GoalPos)
    {
        Vector2 AiPosition = transform.position;

        //if the AI is not near the goal then dont move
        if (Vector2.Distance(AiPosition, GoalPos) > minGoalDist)
        {
            //direction form A to B
            // is B - A

            // Normalize converts a direction and makes the length of it = 1
            // if normalize wasnt here would make object move faster if it was further away and slower when closer
            Vector2 directionToPos0 = GoalPos - transform.position;
            directionToPos0.Normalize();
            transform.position += (Vector3)directionToPos0 * speed * Time.deltaTime;
        }
    }
    private void WaypointUpdate()
    {
        Vector2 AiPosition = transform.position;

        //if the AI is near the goal 
        if (Vector2.Distance(AiPosition, waypoints[waypointIndex].position) < minGoalDist)
        {
            //increment by 1
            waypointIndex++;

            //change waypoint
            if (waypoints.Length <= waypointIndex)
            {
                waypointIndex = 0;
            }
        }
    }
}

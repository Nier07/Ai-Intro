using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public enum State
    {
        Attack,
        Defense,
        Flee,
        BerryPicking
    }

    public State currentState;
    public AiMovement aiMovement;

    private void Start()
    {
        aiMovement = GetComponent<AiMovement>();
        NextState();
    }

    private void NextState()
    {
        //runs one if the cases that matches the value (in this example the value is currentState)
        switch (currentState)
        {
            case State.Attack:
                StartCoroutine(AttackState());
                break;
            case State.Defense:
                StartCoroutine(DefenseState());
                break;
            case State.Flee:
                StartCoroutine(FleeState());
                break;
            case State.BerryPicking:
                StartCoroutine(BerryPickingState());
                break;
        }
    }

    //Coroutine is a method that can be pause and returned to later
    private IEnumerator AttackState()
    {
        Debug.Log("Entering Attack");
        while (currentState == State.Attack)
        {
            if (Vector2.Distance(transform.position, aiMovement.player.position) > aiMovement.chaseDistance)
            {
                currentState = State.BerryPicking;
            }

            aiMovement.AIMoveToward(aiMovement.player);

            yield return null;
        }

        NextState();
    }

    private IEnumerator DefenseState()
    {
        Debug.Log("Entering Defense");
        while (currentState == State.Defense)
        {
            if (aiMovement.waypoints.Count > 2)
            {
                currentState = State.BerryPicking;
            }

            if (Vector2.Distance(transform.position, aiMovement.player.position) < aiMovement.chaseDistance)
            {
                currentState = State.Attack;
            }

            yield return null;
        }

        NextState();
    }

    private IEnumerator FleeState()
    {
        Debug.Log("Entering Flee");
        while (currentState == State.Flee)
        {
            Debug.Log("Currently Fleeing");

            yield return null;
        }

        NextState();
    }

    private IEnumerator BerryPickingState()
    {
        Debug.Log("Entering BerryPick");

        aiMovement.WaypointUpdate();
        while (currentState == State.BerryPicking)
        {
            //performs function from the aimovement script
            aiMovement.WaypointUpdate();
            aiMovement.AIMoveToward(aiMovement.waypoints[aiMovement.waypointIndex]);

            if (Vector2.Distance(transform.position, aiMovement.player.position) < aiMovement.chaseDistance)
            {
                currentState = State.Attack;
            }

            if (aiMovement.waypoints.Count <= 2)
            {
                currentState = State.Defense;
            }

            yield return null;
        }

        NextState();
    }
}

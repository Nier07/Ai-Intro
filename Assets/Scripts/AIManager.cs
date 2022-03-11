using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : BaseManager
{
    public enum State
    {
        FullHP,
        LowHP,
        Dead
    }

    public State currentState;
    protected PlayerManager playerManager;

    protected override void Start()
    {
        base.Start();

        playerManager = GetComponent<PlayerManager>();

        if (playerManager == null)
        {
            Debug.LogError("AIManager not found");
        }
    }

    public override void TakeTurn()
    {
        if (_health <= 0f)
        {
            currentState = State.Dead;
        }
        switch (currentState)
        {
            case State.FullHP:
                FullHPState();
                if (_health > 0)
                {
                    StartCoroutine(EndTurn());
                }
                break;
            case State.LowHP:
                LowHPState();
                if (_health > 0)
                {
                    StartCoroutine(EndTurn());
                }
                break;
            case State.Dead:
                DeadState();
                break;
        }
    }

    IEnumerator EndTurn()
    {
        yield return new WaitForSeconds(2f);
        playerManager.TakeTurn();
    }

    private void FullHPState()
    {
        if (_health < 45)
        {
            currentState = State.LowHP;
            LowHPState();
        }
        int randomAttack = Random.Range(0, 10);

        switch (randomAttack)
        {
            case int i when i > 0 && i <= 2:
                TheBomb();
                Debug.Log("The Bomb");
                break;
            case int i when i > 2 && i <= 8:
                QuickAttack();
                Debug.Log("Quick Attack");
                break;
            case int i when i > 8 && i <= 9:
                SelfDestruct();
                Debug.Log("Self Distruct");
                break;
        }
    }

    private void LowHPState()
    {
        if (_health >= 45)
        {
            currentState = State.FullHP;
            FullHPState();
        }
        int randomAttack = Random.Range(0, 10);

        switch (randomAttack)
        {
            case int i when i > 0 && i <= 1:
                SelfDestruct();
                Debug.Log("Self Distruct");
                break;
            case int i when i > 1 && i <= 8:
                EatBerries();
                Debug.Log("Eat Berries");
                break;
            case int i when i > 8 && i <= 9:
                TheBomb();
                Debug.Log("The Bomb");
                break;
        }
    }

    private void DeadState()
    {
        Debug.Log("You Monster");
    }

    public void EatBerries()
    {
        Heal(25f);
    }

    public void SelfDestruct()
    {
        DealDamage(_maxHealth);
        playerManager.DealDamage(80f);
        currentState = State.Dead;
    }

    public void QuickAttack()
    {
        playerManager.DealDamage(25f);
    }

    public void TheBomb()
    {
        playerManager.DealDamage(45f);
    }
}

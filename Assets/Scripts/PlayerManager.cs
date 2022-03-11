using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : BaseManager
{
    protected AIManager _aiManager;
    [SerializeField] protected CanvasGroup ButtonGroup;
    //protected bool isHealOverTimeRunning = false;

    protected override void Start()
    {
        base.Start();

        _aiManager = GetComponent<AIManager>();

        if (_aiManager == null)
        {
            Debug.LogError("AIManager not found");
        }
    }

    public override void TakeTurn()
    {
        ButtonGroup.interactable = true;
    }

    public void EndTurn()
    {
        ButtonGroup.interactable = false;
        _aiManager.TakeTurn();
    }

    public void EatBerries()
    {
        Heal(35f);
        EndTurn();
        // StartCoroutine(HealOverTime(3, 1f));
    }

    /*private IEnumerator HealOverTime(int times, float waitTime)
    {
        if (!isHealOverTimeRunning)
        {
            isHealOverTimeRunning = true;

            for (int i = 0; i < times; i++)
            {
                Heal(10f);
                yield return new WaitForSeconds(waitTime);
            }
            isHealOverTimeRunning = false;
        }
        else
        {
            Debug.Log("HealIsAlreadyRunning");
        }
    }*/

    public void SelfDestruct()
    {
        DealDamage(_maxHealth);
        _aiManager.DealDamage(80f);
        EndTurn();
    }

    public void QuickAttack()
    {
        _aiManager.DealDamage(25f);
        EndTurn();
    }

    public void TheBomb()
    {
        _aiManager.DealDamage(45f);
        EndTurn();
    }
}

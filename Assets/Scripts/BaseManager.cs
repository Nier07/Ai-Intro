using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//cant add abstract classes to game objects purley for inheritence
public abstract class BaseManager : MonoBehaviour
{
    //SerializeField allows unity to acsess private variables
    //Helps other classes not mess with variable but unity can
    //Protected is similar to private but inherited classes have acsess
    [SerializeField] protected float _health = 100;
    [SerializeField] protected float _maxHealth = 100;

    [SerializeField] protected Text _healthText;

    //virtual void allows the function to be overriden by child classes
    //override replaces parent classes function as long as the base function is virtual unless Base.Start() is called
    //in this case it will run the virtual start then the overrided start
    protected virtual void Start()
    {
        UpdateHealthText();
    }

    // abstract functions allows functions to not be created yet
    //makes inherted functions able to create the function
    //however all inhertied classes require defining this function
    public abstract void TakeTurn();


    public void UpdateHealthText()
    {
        if (_healthText != null)
        {
            _healthText.text = _health.ToString("0");
        }
    }

    public void DealDamage(float damage)
    {
        _health = Mathf.Max(_health - damage, 0);
        if (_health <= 0)
        {
            _health = 0;
            //some logic to stop damage, give exp, etc
            Debug.Log("Dead");
        }
        UpdateHealthText();
    }

    public void Heal(float heal)
    {
        //adds health and heal together and if heal is over max just returns max
        _health = Mathf.Min(_health + heal, _maxHealth);
        UpdateHealthText();
    }
}

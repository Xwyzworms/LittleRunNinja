using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Fire_Switcher : MonoBehaviour
{
    private Animator anim;
    public Trap_Fire myTrap;

    [SerializeField] private float countdown;
    [SerializeField] private float timeNotActive = 2;
    

    private void Start() 
    {
        anim = GetComponent<Animator>();
    }


    private void Update() 
    {
        // Going to be minus allthe time
        countdown = Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {

        if(countdown > timeNotActive) 
        {
            return;
        }

        if(collision.GetComponent<Player>() != null) 
        {
            countdown = timeNotActive;
            anim.SetTrigger("pressed");
            myTrap.FireSwitch();
        }    

    }
}

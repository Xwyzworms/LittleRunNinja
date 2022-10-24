using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Fire_Switcher : MonoBehaviour
{
    private Animator anim;
    public Trap_Fire myTrap;


    private void Start() 
    {
        anim = GetComponent<Animator>();
    }


    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if(collision.GetComponent<Player>() != null) 
        {
            anim.SetTrigger("pressed");
            myTrap.FireSwitch();
        }    

    }
}
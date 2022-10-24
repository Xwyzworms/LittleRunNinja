using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Fire : Trap
{
    public bool isWorking = false;
    public float repeatRate = 2;
    private Animator anim;


    private void Start()
    {
        anim = GetComponent<Animator>();
        //InvokeRepeating("FireSwitch",0, repeatRate);
    }

    private void Update() 
    {
        anim.SetBool("IsWorking", isWorking);
    }

    public void FireSwitch() 
    {

        Debug.Log(" FireSwitch Called");
        isWorking = !isWorking;
    }

    protected override void OnTriggerEnter2D(Collider2D collision) 
    {
        if(isWorking) 
        {
            base.OnTriggerEnter2D(collision);
        }       
        else {
        
        }
    }
}

using System.Collections;

using System.Collections.Generic;
using UnityEngine;

public class Trap_Saw : Trap
{
    private Animator anim;

    [SerializeField] private Transform[] movePoint ;
    [SerializeField] private float speed;

    private int movePointIndex = 0;

    private float coolDownTimer = 0;

    public float cooldown = 0f;

    void Start()
    {
        anim = GetComponent<Animator>();   
    }

    // Update is called once per frame
    void Update()
    {

        coolDownTimer -= Time.deltaTime;
        bool isWorking = coolDownTimer < 0;

        anim.SetBool("isWorking", isWorking);


        if(isWorking) 
            this.transform.position = Vector3.MoveTowards(this.transform.position, movePoint[movePointIndex].position, speed * Time.deltaTime);

        if(Vector2.Distance(transform.position, movePoint[movePointIndex].position) < 0.15f) 
        {
            Flip();
            coolDownTimer = cooldown;
            movePointIndex ++;
            if(movePointIndex >= movePoint.Length) 
            {
                movePointIndex = 0;
            }
        }
    }


    private void Flip() 
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y);
    }
}

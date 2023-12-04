using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeIn : MonoBehaviour
{
    private Animator animator;
    private Animation anim;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }



    public void FadeInRock()
    {
        animator.SetTrigger("Part1FadeIn");
        animator.SetTrigger("Part2FadeIn");
        animator.SetTrigger("Part3FadeIn");
    }
}

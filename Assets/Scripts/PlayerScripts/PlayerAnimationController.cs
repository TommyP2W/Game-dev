using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void startAttack()
    {
        animator.SetBool("isAttacking", true);
    }
    public void endAttack()
    {
        animator.SetBool("isAttacking", false);
    }

    // Update is called once per frame
    void Update()
    {
       if (gameObject.GetComponent<Characters>().isWalking)
        {
            animator.SetBool("isWalking", true);
        } else
        {
            animator.SetBool("isWalking", false);
        }
    }
}

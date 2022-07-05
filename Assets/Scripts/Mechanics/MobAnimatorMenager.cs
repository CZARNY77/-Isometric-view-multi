using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobAnimatorMenager : MonoBehaviour
{
    [SerializeField] RuntimeAnimatorController idleAnim;
    [SerializeField] RuntimeAnimatorController runAnim;
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void switchAnim(bool stand)
    {
        if (stand)
        {
            animator.runtimeAnimatorController = idleAnim;
        }
        else
        {animator.runtimeAnimatorController = runAnim;
            
        }
    }
}

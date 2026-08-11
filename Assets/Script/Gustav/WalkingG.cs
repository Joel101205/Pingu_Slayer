using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WalkingG : StateMachineBehaviour
{
    private Rigidbody2D rb;
    private Transform player;
    [SerializeField] private float speed;
    private Gustav gustav;
    [SerializeField] private float slashRange;
    [SerializeField] private float thrustRange;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb = animator.GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        gustav = animator.GetComponent<Gustav>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        move();

        if (Vector2.Distance(player.position, rb.position) <= slashRange)
        {
            animator.SetTrigger("Slash");
        }

        if (Vector2.Distance(player.position, rb.position) <= thrustRange &&
            Vector2.Distance(player.position, rb.position) > slashRange)
        {
            animator.SetTrigger("Thrust");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Slash");
        animator.ResetTrigger("Thrust");
    }
    
    public void move()
    {
        gustav.LookAtPlayer();
        
        Vector2 target = new Vector2(player.transform.position.x, rb.position.y);
        Vector2 newPosition = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPosition);
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walking : StateMachineBehaviour
{
    private Transform player;
    [SerializeField] private float speed;
    private Ludwig _ludwig;
    [SerializeField] private float attackRange;
    [SerializeField] private float jumpRange;

    private Rigidbody2D _rigidbody2D;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _rigidbody2D = animator.GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        _ludwig = animator.GetComponent<Ludwig>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        move();

        if (Vector2.Distance(player.position, _rigidbody2D.position) <= attackRange)
        {
            animator.SetTrigger("Slam");
        }

        if (Vector2.Distance(player.position, _rigidbody2D.position) <= jumpRange &&
            Vector2.Distance(player.position, _rigidbody2D.position) >= attackRange)
        {
            animator.SetTrigger("Jump");
        } 
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Slam");
        animator.ResetTrigger("Jump");
    }


    public void move()
    {
        _ludwig.LookAtPlayer();
        
        Vector2 target = new Vector2(player.transform.position.x, _rigidbody2D.position.y);
        Vector2 newPosition = Vector2.MoveTowards(_rigidbody2D.position, target, speed * Time.fixedDeltaTime);
        _rigidbody2D.MovePosition(newPosition);
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

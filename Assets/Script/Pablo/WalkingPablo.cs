using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingPablo : StateMachineBehaviour
{
    private Transform player;
    private Rigidbody2D _rigidbody2D;
    private Pablo _pablo;
    [SerializeField] private float speed;
    [SerializeField] private float slashRange;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        _rigidbody2D = animator.GetComponent<Rigidbody2D>();
        _pablo = animator.GetComponent<Pablo>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        move();
        
        if (_pablo.missileAvailable)
        {
            animator.ResetTrigger("Slash");
            _pablo.StartMissileCooldown();
            animator.SetTrigger("Button");
        }
        else if (Vector2.Distance(_rigidbody2D.transform.position, player.position) <= slashRange && _pablo.slashAvailable)
        {
            animator.SetTrigger("Slash");
        }
        
    }
    
    
    
    public void move()
    {
        _pablo.LookAtPlayer();
        
        Vector2 target = new Vector2(player.transform.position.x, _rigidbody2D.position.y);
        Vector2 newPosition = Vector2.MoveTowards(_rigidbody2D.position, target, speed * Time.fixedDeltaTime);
        _rigidbody2D.MovePosition(newPosition);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Button");
        animator.ResetTrigger("Slash");
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

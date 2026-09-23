using UnityEngine;

public class Walking_Gustav : StateMachineBehaviour
{
    private Gustav gustav;

    [SerializeField] private float slashRange;
    [SerializeField] private float thrustRange;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        gustav = animator.GetComponent<Gustav>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        gustav.move();
        float distanceToPlayer = Vector2.Distance(gustav.Player.position, animator.transform.position);

        if (distanceToPlayer <= slashRange)
        {
            animator.SetTrigger("Slash");
        }

        else if (distanceToPlayer <= thrustRange)
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

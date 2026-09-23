using UnityEngine;

public class Walking_Ludwig : StateMachineBehaviour
{
    private Ludwig ludwig;

    [SerializeField] private float attackRange;
    [SerializeField] private float jumpRange;
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
 
        ludwig = animator.GetComponent<Ludwig>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ludwig.move();

        float distanceToPlayer = Vector2.Distance(ludwig.Player.position, ludwig.Rb.position);

        if (distanceToPlayer <= attackRange)
        {
            animator.SetTrigger("Slam");
        }
        
        else if (distanceToPlayer <= jumpRange)
        {
            animator.SetTrigger("Jump");
        } 
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Slam");
        animator.ResetTrigger("Jump");
    }




}

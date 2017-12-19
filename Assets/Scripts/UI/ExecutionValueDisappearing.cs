using UnityEngine;

class ExecutionValueDisappearing : StateMachineBehaviour{

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex){
        animator.GetComponent<ExecutionValue>().AnimationEnded();   
    }

}

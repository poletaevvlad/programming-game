using UnityEngine;

class ExecutionValueMovement: StateMachineBehaviour {

    public float startOffset;
    public float endOffset;

    private int length;

    private ExecutionValue executionValue;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex){
        executionValue = animator.GetComponent<ExecutionValue>();
        length = 0;
        for (int i = 0; i < executionValue.lineRenderer.coordinates.Count - 1; i++) {
            length += ConnectionLineRenderer.ManhattanDistance(
                    executionValue.lineRenderer.coordinates[i], executionValue.lineRenderer.coordinates[i + 1]);
        }
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex){
        executionValue.position += Time.deltaTime * length * animatorStateInfo.speed;
        if (executionValue.position >= length - endOffset) {
            executionValue.position = length - endOffset;
            animator.SetTrigger("MovementEnded");
        }
    }

}

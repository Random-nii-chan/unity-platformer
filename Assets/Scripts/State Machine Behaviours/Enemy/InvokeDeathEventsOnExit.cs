using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InvokeDeathEventsOnExit : StateMachineBehaviour
{
    UnityEvent exitEvents;
    Enemy en;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        en = animator.gameObject.GetComponent<Enemy>();
        exitEvents = en.deathEvents;

        if (exitEvents == null) {
            exitEvents = new UnityEvent();
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        exitEvents.Invoke();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InvokeDeathEventsOnEnter : StateMachineBehaviour
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

        exitEvents.Invoke();
    }
}

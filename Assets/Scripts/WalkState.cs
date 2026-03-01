using System;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : StateMachineBehaviour
{
    private Move _characterMovement;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_characterMovement == null)
        {
            _characterMovement = animator.GetComponent<Move>();
        }

        _characterMovement.MoveToNextPoint();
    }
}
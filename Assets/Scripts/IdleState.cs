
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class IdleState : StateMachineBehaviour
{
    private Move _characterMovement;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_characterMovement == null)
        {
            _characterMovement = animator.GetComponent<Move>();
        }

        _characterMovement.StartCoroutine(IdleBeforeNextMove(animator));
    }

    private IEnumerator IdleBeforeNextMove(Animator animator)
    {
        yield return new WaitForSeconds(_characterMovement.IdleTime);
        animator.SetBool(Move.IsWalking, true);
    }
}
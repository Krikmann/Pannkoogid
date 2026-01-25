using UnityEngine;
using System.Collections;

public class IdleChild : NPCParent.IState
{
    private NPCParent npc;
    private float duration = Random.Range(0.5f,1.3f);

    public void IdleState(NPCParent npc)
    {
        this.npc = npc;
    }

    public void Enter()
    {
        npc.StartCoroutine(IdleForDuration());
        npc.GetComponent<Animator>().SetTrigger("Idle");
    }

    public void Update()
    {
    }

    public void Exit()
    {
    }

    private IEnumerator IdleForDuration()
    {
        yield return new WaitForSeconds(duration);
        npc.ChangeState();
    }
}

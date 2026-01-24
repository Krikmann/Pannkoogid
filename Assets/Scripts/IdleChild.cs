using UnityEngine;
using System.Collections;

public class IdleChild : NPCParent.IState
{
    private NPCParent npc;
    private float duration = Random.Range(0.5f,1.3f);
    private bool isIdle = false;

    public void IdleState(NPCParent npc)
    {
        this.npc = npc;
    }

    public void Enter()
    {
        isIdle = true;
        npc.StartCoroutine(IdleForDuration());
    }

    public void Update()
    {
        if (isIdle)
        {            
            Debug.Log("NPC is idling");
        }
    }

    public void Exit()
    {
        isIdle = false;
    }

    private IEnumerator IdleForDuration()
    {
        yield return new WaitForSeconds(duration);
        npc.ChangeState();
    }
}

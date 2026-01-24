using UnityEngine;
using System.Collections;

public class WalkingChild : NPCParent.IState
{
    private NPCParent npc;
    private float duration = 1f; // Duration to walk
    private bool isWalking = false;
    private Vector2 direction;


    public void WalkingState(NPCParent npc)
    {
        this.npc = npc;
    }

    public void Enter()
    {
        isWalking = true; // Start walking
        direction = Random.insideUnitCircle.normalized;
        npc.StartCoroutine(WalkForDuration());
    }

    public void Update()
    {
        // Update walking animation or movement logic here
        if (isWalking)
        {
            Debug.Log("NPC is walking...");
            npc.transform.Translate(direction * npc.moveSpeed * Time.deltaTime);
        }
    }

    public void Exit()
    {
        isWalking = false; // Stop walking actions
        // Handle any exit logic if necessary
    }

    private IEnumerator WalkForDuration()
    {
        yield return new WaitForSeconds(duration);
        npc.ChangeState();
        // After walking for the specified duration, transition to another state, e.g., Idle
    }
}

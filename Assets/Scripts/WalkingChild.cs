using UnityEngine;
using System.Collections;

public class WalkingChild : MonoBehaviour
{
    private NPCParent npc;
    private float duration = 5f; // Duration to walk
    private bool isWalking = false;

    public void WalkingState(NPCParent npc)
    {
        this.npc = npc;
    }

    public void Enter()
    {
        isWalking = true; // Start walking
        npc.StartCoroutine(WalkForDuration());
    }

    public void Update()
    {
        // Update walking animation or movement logic here
        if (isWalking)
        {
            Debug.Log("NPC is walking...");
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
        // After walking for the specified duration, transition to another state, e.g., Idle
    }
}

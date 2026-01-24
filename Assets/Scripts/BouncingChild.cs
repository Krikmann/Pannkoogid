using UnityEngine;
using System.Collections;

public class BouncingChild : NPCParent.IState
{
    public float bounceHeight = 1f; // Height of the bounce
    public float bounceSpeed = Mathf.PI; // Speed of the bounce effect

    private float duration = 1f; // Duration to walk
    private Vector2 originalPosition;
    private NPCParent npc;
    private bool isBouncing = false;

    public void BouncingState(NPCParent npc)
    {
        this.npc = npc;
    }

    public void Enter()
    {
        isBouncing = true;
        originalPosition = npc.transform.position;
        npc.StartCoroutine(BounceForDuration());
    }

    public void Update()
    {
        if (isBouncing)
        {
            float newY = Mathf.Sin(Time.time * bounceSpeed) * bounceHeight;

            npc.transform.position = new Vector2(originalPosition.x, originalPosition.y + newY);
            Debug.Log("NPC is bouncing");
        }
    }

    public void Exit()
    {
        isBouncing = false;
    }

    private IEnumerator BounceForDuration()
    {
        yield return new WaitForSeconds(duration);
        npc.ChangeState();
    }
}

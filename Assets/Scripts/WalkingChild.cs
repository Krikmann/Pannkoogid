using UnityEngine;
using System.Collections;

public class WalkingChild : NPCParent.IState
{
    private NPCParent npc;
    private float duration = Random.Range(1f,2.3f); // Duration to walk
    private bool isWalking = false;
    private Vector2 direction;
    private bool sussyBool;
    private float moveSpeed;

    public void WalkingState(NPCParent npc)
    {
        this.npc = npc;
        sussyBool = npc.sussyBoolList[0];
        moveSpeed = npc.moveSpeed;
        if (sussyBool)
        {
            moveSpeed *= 10;
        }
    }

    public void Enter()
    {
        isWalking = true;
        direction = Random.insideUnitCircle.normalized;
        npc.StartCoroutine(WalkForDuration());
    }

    public void Update()
    {
        if (isWalking)
        {
/*

KRISTO, siin toimub kõndimine

*/
            Debug.Log("NPC is walking... " + direction + " Speed: " + moveSpeed);
            Vector2 newPosition = (Vector2)npc.transform.position + direction * moveSpeed * Time.deltaTime;

            // Check if the new position is inside the polygon collider
            if (!npc.outsideCollider.OverlapPoint(newPosition))
            {
                // Invert direction if outside the collider
                direction = -direction; 
                newPosition = (Vector2)npc.transform.position + direction * moveSpeed * Time.deltaTime;
            }
            foreach (var insideCollider in npc.insideColliders)
            {
                if (insideCollider.OverlapPoint(newPosition))
                {
                    // Adjust direction if colliding with an inner collider
                    direction = -direction;
                    newPosition = (Vector2)npc.transform.position + direction * moveSpeed * Time.deltaTime;
                    break; // Exit the loop if we adjust the direction
                }
            }

            // Update the position
            npc.transform.position = newPosition;
        }
    }

    public void Exit()
    {
        isWalking = false;
    }

    private IEnumerator WalkForDuration()
    {
        yield return new WaitForSeconds(duration);
        npc.ChangeState();
    }
}

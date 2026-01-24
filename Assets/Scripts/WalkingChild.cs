using UnityEngine;
using System.Collections;

public class WalkingChild : NPCParent.IState
{
    private NPCParent npc;
    private float duration = Random.Range(1f,2.3f); // Duration to walk
    private bool isWalking = false;
    private Vector2 direction;
    private bool susSuund;
    private bool susAnima;
    private float moveSpeed = Random.Range(1f,2f);  

    public void WalkingState(NPCParent npc)
    {
        this.npc = npc;
        susSuund = npc.sussyBoolList[0];
        susAnima = npc.sussyBoolList[1];
    }

    public void Enter()
    {
        isWalking = true;
        direction = Random.insideUnitCircle.normalized;
        if (susSuund)
        {
            direction = SnapToCardinal(direction);
        }
        Animator animator = npc.GetComponent<Animator>();
        if (susAnima == false) animator.SetTrigger("Bounce");
        else animator.SetTrigger("Walk");
        npc.StartCoroutine(WalkForDuration());
    }

    public void Update()
    {
        if (isWalking)
        {

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

    private Vector2 SnapToCardinal(Vector2 direction)
    {
        // Check the absolute values of x and y to determine the nearest cardinal direction
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            // Snap to left or right
            return direction.x > 0 ? Vector2.right : Vector2.left;
        }
        else
        {
            // Snap to up or down
            return direction.y > 0 ? Vector2.up : Vector2.down;
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

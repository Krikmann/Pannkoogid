using System.Collections; 
using UnityEngine;

public class NPCParent : MonoBehaviour
{
    // Start is called before the first frame update
    public PolygonCollider2D outsideCollider;
    public PolygonCollider2D[] insideColliders;

    public interface IState{
        public void Enter();
        public void Update();
        public void Exit();
    }
    private IState currentState; // Current state reference
    private string[] states = { "move", "speak", "idle", "bounce"};
    private WalkingChild walkingState;
    private SpeakingChild speakingState;
    private IdleChild idleState;
    private BouncingChild bouncingState;

    public float moveSpeed;
    private void Start()
    {
        // Initialize and change to the walking state
        walkingState = new WalkingChild();
        walkingState.WalkingState(this); // Pass the NPC reference
        speakingState = new SpeakingChild();
        speakingState.SpeakingState(this);
        idleState = new IdleChild();
        idleState.IdleState(this);
        bouncingState = new BouncingChild();
        bouncingState.BouncingState(this);
        ChangeState(walkingState); // Start with walking state
    }
    private void Update()
    {
        currentState?.Update(); // Call Update method of the current state
    }
    public void ChangeState(IState newState = null)
    {
        currentState?.Exit(); // Exit the current state first
        if (newState == null) 
        {
            int chosenIndex = Random.Range(0, states.Length);
            string currentAction = states[chosenIndex];
            switch (currentAction)
            {
                case "move":
                    newState = walkingState; // Use existing instance
                    break;
                case "speak":
                    newState = speakingState; // Use existing instance
                    break;                
                case "idle":
                    newState = idleState; // Use existing instance
                    break;                
                case "bounce":
                    newState = bouncingState; // Use existing instance
                    break;
                // Add cases for other actions (e.g., Speak, Idle)
            }
        }
        currentState = newState; // Change to the new state
        currentState.Enter(); // Enter the new state
    }
}
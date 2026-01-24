using System.Collections; 
using UnityEngine;
using System.Collections.Generic; // Add this line

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
    private string[] states = { "move", "speak", "idle"};
    private List<bool> sussyBool;
    private WalkingChild walkingState;
    private SpeakingChild speakingState;
    private IdleChild idleState;

    public float moveSpeed;
    private void Start()
    {
        sussyBool = CreateRandomBooleanList(states.Length);
        walkingState = new WalkingChild();
        walkingState.WalkingState(this);
        speakingState = new SpeakingChild();
        speakingState.SpeakingState(this);
        idleState = new IdleChild();
        idleState.IdleState(this);
        ChangeState(walkingState); // Start with walking state
    }
    private List<bool> CreateRandomBooleanList(int size)
    {
        List<bool> tempList = new List<bool>();

        for (int i = 0; i < size; i++)
        {
            bool randomValue = Random.Range(0f, 1f) > 0.5f; // Randomly assigns true or false
            tempList.Add(randomValue);
        }

        return tempList;
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
            }
        }
        currentState = newState; // Change to the new state
        currentState.Enter(); // Enter the new state
    }
}
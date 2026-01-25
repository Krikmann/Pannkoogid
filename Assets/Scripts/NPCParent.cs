using System.Collections;
using UnityEngine;
using System.Collections.Generic; // Add this line

public class NPCParent : MonoBehaviour
{
    public static int DirectionIndex = 0;
    public static int BounceIndex = 1;
    public static int ClothesIndex = 2;
    public static int ERMTestIndex = 3;
    public static int IDTestIndex = 4;
    
    
    // Start is called before the first frame update
    public PolygonCollider2D outsideCollider;
    public PolygonCollider2D[] insideColliders;
    public List<int> impostorTells = new(); // tells 

    public int tellListLength = 5; // we have 5 possible tells

/*
tell nimekiri:
directionality; liikumis_anima; riided; "ERM" test; ID ask;
*/
    public string visitorName;
    //nimekiri nimedest
    public bool isImpostor;

    public interface IState
    {
        public void Enter();
        public void Update();
        public void Exit();
    }

    private IState currentState; // Current state reference
    private string[] states = { "move", "idle" };
    public List<bool> sussyBoolList;

    private WalkingChild walkingState;

//    private SpeakingChild speakingState;
    private IdleChild idleState;

    public float moveSpeed;

    private void Start()
    {
        sussyBoolList = CreateRandomBooleanList(tellListLength);
        walkingState = new WalkingChild();
        walkingState.WalkingState(this);
//        speakingState = new SpeakingChild();
//        speakingState.SpeakingState(this);
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
        int nameIndexTemp = Random.Range(0,9);
        if (sussyBoolList[4])
        {
            nameIndexTemp += 10;
        }
        //this.visitorName = list nimeedega [nameIndexTemp];
        // no random for impostor tells
        foreach (int tell in impostorTells)
        {
            tempList[tell] = isImpostor;
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
//                case "speak":
//                    newState = speakingState; // Use existing instance
//                    break;
                case "idle":
                    newState = idleState; // Use existing instance
                    break;
            }
        }

        currentState = newState; // Change to the new state
        currentState.Enter(); // Enter the new state
    }
}
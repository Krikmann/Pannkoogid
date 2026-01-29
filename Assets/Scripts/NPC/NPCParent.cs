using UnityEngine;
using System.Collections.Generic;

public class NPCParent : MonoBehaviour
{
    public static int DirectionIndex = 0;
    public static int BounceIndex = 1;
    public static int SpriteIndex = 2;
    public static int ErmTestIndex = 3;
    public static int IDTestIndex = 4;

    public PolygonCollider2D outsideCollider;
    public PolygonCollider2D[] insideColliders;
    public List<int> impostorTells = new(); // tells 

    public int tellListLength = 5; // we have 5 possible tells

/*
tell nimekiri:
directionality; liikumis_anima; riided; "ERM" test; ID ask;
*/
    public string visitorName;

    public List<string> manNames = new()
    {
        "Martin", "Andres", "Toomas", "Margus", "Kristjan"
    };
    public List<string> womanNames = new()
    {
        "Anna", "Maria", "Katrin", "Tiina", "Laura"
    };
    
    public List<string> wrongManNames = new()
    {
        "Martn", "Anederes", "Tooomas", "Märgus", "Krisgjan"
    };
    public List<string> wrongWomanNames = new()
    {
        "Änna", "Mariaia", "Katfrin", "Viiina", "L-aura"
    };
    
    
    //nimekiri nimedest
    public bool isImpostor;
    public bool isMale;

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
    private IdleChild idleState;

    public float moveSpeed;

    private void Start()
    {
        sussyBoolList = CreateRandomBooleanList(tellListLength);
        visitorName = ChooseName();
        
        walkingState = new WalkingChild();
        walkingState.WalkingState(this);
        
        idleState = new IdleChild();
        idleState.IdleState(this);
        
        ChangeState(walkingState); // Start with walking state
    }

    private string ChooseName()
    {
        if (isMale)
        {
            if (sussyBoolList[IDTestIndex]) return wrongManNames[Random.Range(0,wrongManNames.Count)];
            return manNames[Random.Range(0,manNames.Count)];
        }
        if (sussyBoolList[IDTestIndex]) return wrongWomanNames[Random.Range(0,wrongWomanNames.Count)];
        return womanNames[Random.Range(0,womanNames.Count)];
    }

    private List<bool> CreateRandomBooleanList(int size)
    {
        List<bool> tempList = new List<bool>();
        tempList.AddRange(new bool[size]);  // add size x false
        foreach (int tell in impostorTells) 
            tempList[tell] = isImpostor;    // impostor does tells differently
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
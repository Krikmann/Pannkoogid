
using System.Collections; 
using UnityEngine;

public class NPCParent : MonoBehaviour
{
    // Start is called before the first frame update
    private string[] states = { "move", "speak", "idle", "test"};
    string state = "idle";
    bool working = false;

    public float moveSpeed;
    private Vector2 targetPosition;
    void Start()
    {
        targetPosition = new Vector2(0.0f, 0.0f);
        StartCoroutine(WaitAndPrint());
    }

    // Update is called once per frame
    void Update()
    {
        if (!working){
            int ChosenAction = Random.Range(0,states.Length);
            Debug.Log(states[ChosenAction]);
            state = states[ChosenAction];
        }
        PerformRandomAction();
    }

    IEnumerator WaitAndPrint() 
    {
        yield return new WaitForSeconds(5);
        Debug.Log("Waited 5 seconds!");
    }

    void PerformRandomAction(){

        switch (state)
        {
            case "idle": IdleAction(); break;
            case "move": MoveAction(); break;
        }
    }

    void MoveAction()
    {
        float elapsedTime = 0f;
        working = true;
        Vector2 direction = Random.insideUnitCircle;

        while (elapsedTime < 5f)
        {
            elapsedTime += Time.deltaTime;
            transform.Translate(direction * moveSpeed * Time.deltaTime);
            WaitAndPrint();
        }
        working = false;
    }
    void IdleAction()
    {
        float elapsedTime = 0f;
        working = true;
                while (elapsedTime < 5000f)
        {
            elapsedTime += Time.deltaTime;
            WaitAndPrint();
        }
        working = false;
    }
}
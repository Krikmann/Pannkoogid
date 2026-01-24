using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class VisitorSpawner : MonoBehaviour
{
    Scene scene;
    [Tooltip(
        "0 - directionality\n" +
        "1 - bounce\n" +
        "2 - riided\n" +
        "3 - ERM... test\n" +
        "4 - ID kaardil nimi")]
    public List<int> tells;
    public int amountTells;
    public List<int> chosenTells;
    public int visitorCount;
    public GameObject visitorPrefab;

    public GameObject visitorPanel;

    public PolygonCollider2D visitorBounds;
    
    private void Start()
    {
        scene = SceneManager.GetActiveScene();
        Debug.Log("Active Scene name is: " + scene.name + "\nActive Scene index: " + scene.buildIndex);
        switch (scene.name)
        {
            case ("Level1"): amountTells = 3    ; visitorCount = 10; break;
            case ("Level2"): amountTells = 3    ; visitorCount = 30; break;
            case ("Level3"): amountTells = 2    ; visitorCount = 30; break;
            case ("Level4"): amountTells = 2    ; visitorCount = 50; break;
            case ("Level5"): amountTells = 1    ; visitorCount = 50; break;
        }
        for (int i = 0; i < amountTells; i++)
        {
            int temp = Random.Range(0,4);
            if (chosenTells.Contains(temp))
            {
                i--;
            } else
            {
                chosenTells.Add(temp);
                tells.Add(temp);
            }
        }
        for (int i = 0; i < visitorCount; i++)
        {
            GameObject visitor = Instantiate(visitorPrefab);
            visitor.transform.position = new Vector3(
                Random.Range(-2f, 2f),
                Random.Range(-2, 2f),
                0.0f
            );
            visitor.GetComponent<Selectable>().visitorPanel = visitorPanel;
            NPCParent npc = visitor.GetComponent<NPCParent>();
            npc.outsideCollider = visitorBounds;
            npc.impostorTells = tells;
            if (i == 0) npc.isImpostor = true;
        }
    }
}
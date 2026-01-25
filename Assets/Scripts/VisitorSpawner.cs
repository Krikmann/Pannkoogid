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
    private int amountTells;
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
            case ("Level2"): amountTells = 3    ; visitorCount = 15; break;
            case ("Level3"): amountTells = 2    ; visitorCount = 15; break;
            case ("Level4"): amountTells = 2    ; visitorCount = 25; break;
            case ("Level5"): amountTells = 1    ; visitorCount = 25; break;
        }
        
        // Init visitors
        for (int i = 0; i < visitorCount; i++)
        {
            GameObject visitor = Instantiate(visitorPrefab);
            visitor.transform.position = GetRandomPointInBounds(visitorBounds);
            visitor.GetComponent<Selectable>().visitorPanel = visitorPanel;
            NPCParent npc = visitor.GetComponent<NPCParent>();
            npc.outsideCollider = visitorBounds;
            npc.impostorTells = tells;
            if (i == 0) npc.isImpostor = true;  // first visitor is sus
        }
    }
    
    Vector2 GetRandomPointInBounds(PolygonCollider2D bounds)
    {
        Vector2 point = new Vector2(
            Random.Range(bounds.bounds.min.x, bounds.bounds.max.x),
            Random.Range(bounds.bounds.min.y, bounds.bounds.max.y)
        );

        if (bounds.OverlapPoint(point))
            return point;
        return GetRandomPointInBounds(bounds); // try again
    }
}
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class VisitorSpawner : MonoBehaviour
{
    [Tooltip(
        "0 - directionality\n" +
        "1 - bounce\n" +
        "2 - riided\n" +
        "3 - ERM... test\n" +
        "4 - ID kaardil nimi")]
    public List<int> tells;
    public int visitorCount;
    public GameObject visitorPrefab;
    public GameObject visitorPanel;
    public PolygonCollider2D visitorBounds;
    
    private void Start()
    {
        
        
        // Init visitors
        for (int i = 0; i < visitorCount; i++)
        {
            GameObject visitor = Instantiate(visitorPrefab);
            visitor.transform.position = GetRandomPointInBounds(visitorBounds);
            visitor.GetComponent<VisitorSelector>().visitorPanel = visitorPanel;
            NPCParent npc = visitor.GetComponent<NPCParent>();
            npc.outsideCollider = visitorBounds;
            npc.impostorTells = tells;
            npc.isMale = Random.value < 0.5f;   // coin flip
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
        return GetRandomPointInBounds(bounds); // try agai
    }
}
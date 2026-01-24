using System.Collections.Generic;
using UnityEngine;
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

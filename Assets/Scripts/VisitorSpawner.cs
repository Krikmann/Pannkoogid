using UnityEngine;

public class VisitorSpawner : MonoBehaviour
{
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
            visitor.GetComponent<NPCParent>().outsideCollider = visitorBounds;
        }
    }
}

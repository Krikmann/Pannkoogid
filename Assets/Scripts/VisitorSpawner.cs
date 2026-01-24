using System;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;

public class VisitorSpawner : MonoBehaviour
{

    Scene scene;
    public int uniqueTellIndex;
    public int visitorCount;
    public GameObject visitorPrefab;

    public GameObject visitorPanel;

    public PolygonCollider2D visitorBounds;
    
    private void Start()
    {
        scene = SceneManager.GetActiveScene();
        Debug.Log("Active Scene name is: " + scene.name + "\nActive Scene index: " + scene.buildIndex);
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
            npc.uniqueTellIndex = uniqueTellIndex;
            if (i == 0) npc.isImpostor = true;
        }
    }
}

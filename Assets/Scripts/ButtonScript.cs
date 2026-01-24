using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    public GameObject visitor;

    private void Awake()
    {
        GameEvent.current.onZoomedIn += SetCurrentVisitor;
    }

    private void OnDestroy()
    {
        if(GameEvent.current != null)
        {
            GameEvent.current.onZoomedIn -= SetCurrentVisitor;
        }
    }

    private void SetCurrentVisitor(GameObject visitor)
    {
        this.visitor = visitor;
    }


    public void Test1()
    {
        NPCParent npc = visitor.GetComponent<NPCParent>();
        if (npc.sussyBoolList[3]) //ERM index
        {
            GlobalReferences.audioManager.playFakeERM();
        }
        else
        {
            GlobalReferences.audioManager.playERM();
        }
    }

    public void Test2()
    {
        Debug.Log("Test2");
    }

    public void Suudi()
    {
        Debug.Log("Suudi");
    }
}

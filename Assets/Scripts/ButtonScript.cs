using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    public GameObject visitor;
    public TextMeshProUGUI IdCardText;
    public Image CharacterImage;

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
        SpriteRenderer SR = visitor.GetComponentInChildren<SpriteRenderer>();
        if(SR != null )
        {
            CharacterImage.sprite = SR.sprite;
        }

        NPCParent npc = visitor.GetComponent<NPCParent>();
        if (npc.sussyBoolList[4]) // Erm index
        {
            IdCardText.text = "Impostor kaardil";
        }
        else
        {
            IdCardText.text = "Visitor kaardil";
        }
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
       
    }

    public void Suudi()
    {
        Debug.Log("Suudi");
    }
}

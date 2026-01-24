using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    public GameObject visitor;
    public TextMeshProUGUI IdCardText;
    public Image CharacterImage;

    private bool IDCardOpen = false;
    public GameObject IDCardPanel;

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

    private void Start()
    {
        IDCardPanel = gameObject.transform.GetChild(2).gameObject;
        IDCardPanel.transform.localScale = new Vector3(0, 0, 0);
        IDCardPanel.transform.localScale = new Vector3(0, 0, 0);
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
        Animator animator = IDCardPanel.GetComponent<Animator>();
        if (IDCardOpen) animator.SetTrigger("Close");
        else  animator.SetTrigger("Open");
        IDCardOpen = !IDCardOpen;
    }

    public void CloseIDCard()
    {
        IDCardPanel = gameObject.transform.GetChild(2).gameObject;
        IDCardPanel.transform.localScale = new Vector3(0, 0, 0);
        IDCardPanel.transform.localScale = new Vector3(0, 0, 0);

        Animator animator = IDCardPanel.GetComponent<Animator>();
        if (IDCardOpen)
        {
            animator.SetTrigger("Close");
            IDCardOpen = !IDCardOpen;
        }

    }

    public void Suudi()
    {
        Debug.Log("Suudi");
    }
}

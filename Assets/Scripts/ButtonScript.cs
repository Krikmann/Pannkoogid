using System;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    public GameObject visitor;
    public Image CharacterImage;
    public TextMeshProUGUI IdCardText;
    public GameObject IDCardPanel;
    public GameObject SyydiPanel;
    private bool IDCardOpen = false;


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
        IDCardPanel.transform.localScale = new Vector3(0, 0, 0);
        IDCardPanel.transform.localScale = new Vector3(0, 0, 0);
        IDCardOpen = false;
    }

    public void SyydiAnimationOver()
    {
        SyydiPanel.SetActive(false);
        
        if (visitor.GetComponent<NPCParent>().isImpostor)  // WIN
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);   // Load next scene
        }
        else  // LOSE
        {
            Health hp = GameObject.Find("Player").GetComponent<Health>();
            hp.currentHP--;
            Debug.Log("Lives: " +  hp.currentHP);
            if (hp.currentHP == 0)
            {
                hp.currentHP = hp.startingHP;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    public void Suudi()
    {
        SyydiPanel.SetActive(true);
        Animator animator = SyydiPanel.GetComponent<Animator>();
        animator.SetTrigger("Syydi");
    }
}

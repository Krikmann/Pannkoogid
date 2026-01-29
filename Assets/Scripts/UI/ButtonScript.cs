using System;
using System.Collections;
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
    private bool IDCardOpen;

    private int guessesDone = 0;


    private void Awake()
    {
        GameEvent.current.onZoomedIn += SetCurrentVisitor;
        GameEvent.current.onRequestedZoomOut += CloseIDCard;
    }

    private void OnDestroy()
    {
        if (GameEvent.current != null)
        {
            GameEvent.current.onZoomedIn -= SetCurrentVisitor;
            GameEvent.current.onRequestedZoomOut -= CloseIDCard;
        }
    }

    private void Start()
    {
        IDCardPanel.transform.localScale = new Vector3(0, 0, 0);
        IDCardPanel.transform.localPosition = new Vector3(0, 0, 0);
    }

    private void SetCurrentVisitor(GameObject vis)
    {
        visitor = vis;
        SpriteRenderer sr = vis.GetComponentInChildren<SpriteRenderer>();
        if (sr != null) CharacterImage.sprite = sr.sprite;

        NPCParent npc = vis.GetComponent<NPCParent>();
        IdCardText.text = npc.visitorName;
    }


    public void Test1()
    {
        NPCParent npc = visitor.GetComponent<NPCParent>();
        if (npc.sussyBoolList[NPCParent.ErmTestIndex])
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
        else animator.SetTrigger("Open");
        IDCardOpen = !IDCardOpen;
    }

    public void CloseIDCard()
    {
        IDCardPanel.transform.localScale = new Vector3(0, 0, 0);
        IDCardPanel.transform.localPosition = new Vector3(0, 0, 0);
        IDCardOpen = false;
    }

    public void SyydiAnimationOver()
    {
        SyydiPanel.SetActive(false);

        if (visitor.GetComponent<NPCParent>().isImpostor) // WIN
        {
            GlobalReferences.audioManager.playCORRECT();
            StartCoroutine(WaitForAudioToFinishWin());
        }
        else // LOSE
        {
            if (guessesDone == 2) //3rd guess lose
            {
                guessesDone = 0;
                GlobalReferences.audioManager.playFAHHH();
                StartCoroutine(WaitForAudioToFinishLose());
            }
            else
            {
                GlobalReferences.audioManager.playINCORRECT();
                guessesDone++;
            }
        }
    }


    IEnumerator WaitForAudioToFinishLose()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Restart same scene
    }

    IEnumerator WaitForAudioToFinishWin()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // Load next scene
    }

    public void Suudi()
    {
        SyydiPanel.SetActive(true);
        Animator animator = SyydiPanel.GetComponent<Animator>();
        animator.SetTrigger("Syydi");
    }
}
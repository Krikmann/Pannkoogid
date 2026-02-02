using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    public static ButtonScript Instance;
    private static readonly int Close = Animator.StringToHash("Close");
    private static readonly int Open = Animator.StringToHash("Open");
    private static readonly int Syydi = Animator.StringToHash("Syydi");

    private GameObject _visitor;
    public Image idCardCharacterImage;
    public TextMeshProUGUI idCardText;
    public GameObject idCardPanel;
    public GameObject syydiPanel;
    private bool _idCardOpen;

    private int _guessesDone;
    public bool isZoomedIn;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        idCardPanel.transform.localScale = new Vector3(0, 0, 0);
        idCardPanel.transform.localPosition = new Vector3(0, 0, 0);
        gameObject.SetActive(false);
    }

    public void SetCurrentVisitor(GameObject vis)
    {
        _visitor = vis;
        SpriteRenderer sr = vis.GetComponentInChildren<SpriteRenderer>();
        if (sr != null)
            idCardCharacterImage.sprite = sr.sprite;

        NPCParent npc = vis.GetComponent<NPCParent>();
        idCardText.text = npc.visitorName;
    }

    public void OnZoomOutButtonClicked()
    {
        _visitor.GetComponent<VisitorSelector>().ZoomOut();
        isZoomedIn = false;
    }


    public void OnErmTestButtonClicked()
    {
        NPCParent npc = _visitor.GetComponent<NPCParent>();
        if (npc.sussyBoolList[NPCParent.ErmTestIndex]) GlobalReferences.audioManager.playFakeERM();
        else GlobalReferences.audioManager.playERM();
    }

    public void OnIDCardButtonClicked()
    {
        Animator animator = idCardPanel.GetComponent<Animator>();
        if (_idCardOpen) animator.SetTrigger(Close);
        else animator.SetTrigger(Open);
        _idCardOpen = !_idCardOpen;
    }

    public void CloseIDCard()
    {
        idCardPanel.transform.localScale = new Vector3(0, 0, 0);
        idCardPanel.transform.localPosition = new Vector3(0, 0, 0);
        _idCardOpen = false;
    }


    public void StartSyydiAnimation()
    {
        syydiPanel.SetActive(true);
        Animator animator = syydiPanel.GetComponent<Animator>();
        animator.SetTrigger(Syydi);
    }

    public void SyydiAnimationOver()
    {
        syydiPanel.SetActive(false);

        if (_visitor.GetComponent<NPCParent>().isImpostor) // WIN
        {
            GlobalReferences.audioManager.playCORRECT();
            StartCoroutine(WaitForAudioToFinishWin());
        }
        else // LOSE
        {
            if (_guessesDone == 2) //3rd guess lose
            {
                _guessesDone = 0;
                GlobalReferences.audioManager.playFAHHH();
                StartCoroutine(WaitForAudioToFinishLose());
            }
            else
            {
                GlobalReferences.audioManager.playINCORRECT();
                _guessesDone++;
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

    public GameObject GetVisitor()
    {
        return _visitor;
    }
}
using UnityEngine;
using System.Collections;

public class SpeakingChild : NPCParent.IState
{
    private NPCParent npc;
    private float duration = 1f;
    private bool isSpeaking = false;
    public float volume=0.5f;
    private bool sussyBool;

    public void SpeakingState(NPCParent npc)
    {
        this.npc = npc;
        sussyBool = npc.sussyBoolList[1];
        if (sussyBool)
        {
            volume *= 2;
        }
    }

    public void Enter()
    {
        isSpeaking = true;
        npc.StartCoroutine(SpeakForDuration());
    }

    public void Update()
    {
        if (isSpeaking)
        {
            Debug.Log("NPC is speaking...");
            //play soung
        }
    }

    public void Exit()
    {
        isSpeaking = false;
    }

    private IEnumerator SpeakForDuration()
    {
        yield return new WaitForSeconds(duration);
        npc.ChangeState();
    }
}

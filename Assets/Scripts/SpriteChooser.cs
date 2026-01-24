using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpriteChooser : MonoBehaviour
{
    [SerializeField] private Sprite[] eestiSprites;
    [SerializeField] private Sprite[] valismaaSprites;

    private SpriteRenderer _spriteRenderer;

    private Vector2 lastPos;
    
    private void Start()
    {
        lastPos = transform.position;
        
        // Choose sprite
        NPCParent npc = gameObject.GetComponent<NPCParent>();
        _spriteRenderer = gameObject.GetComponentInChildren<SpriteRenderer>();

        _spriteRenderer.sprite = npc.sussyBoolList[npc.uniqueTellIndex] ?   // kas on sussy käitumine 
            valismaaSprites[Random.Range(0, valismaaSprites.Length)] :  // vali sussy sprite
            eestiSprites[Random.Range(0, eestiSprites.Length)]; // tavaline käitumine
    }

    private void Update()
    {
        Vector2 pos = transform.position;
        Vector2 delta =  pos - lastPos;
        if (Mathf.Abs(delta.x) > 0.00001f) 
            _spriteRenderer.flipX = delta.x < 0;
        
        lastPos = pos;
    }
}
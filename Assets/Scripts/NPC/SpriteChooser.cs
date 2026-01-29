using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpriteChooser : MonoBehaviour
{
    [SerializeField] private Sprite[] manSprites;
    [SerializeField] private Sprite[] womanSprites;

    private SpriteRenderer _spriteRenderer;

    private Vector2 _lastPos;

    private NPCParent _npc;
    
    private void Start()
    {
        _lastPos = transform.position;
        
        // Choose sprite
        _npc = gameObject.GetComponent<NPCParent>();
        _spriteRenderer = gameObject.GetComponentInChildren<SpriteRenderer>();

        _spriteRenderer.sprite = _npc.isMale ?
            manSprites[Random.Range(0, manSprites.Length)] :
            womanSprites[Random.Range(0, womanSprites.Length)];
    }

    private void Update()
    {
        // rotate sprite towards facing direction
        Vector2 pos = transform.position;
        Vector2 delta =  pos - _lastPos;
        if (Mathf.Abs(delta.x) > 0.00001f) 
            _spriteRenderer.flipX = _npc.sussyBoolList[NPCParent.SpriteIndex] 
                ? delta.x > 0   // Impostor walking backwards 
                : delta.x < 0;  // Normal walk
        _lastPos = pos;
    }
}
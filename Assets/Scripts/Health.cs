using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public static Health Instance;
    public int startingHP;
    public int currentHP;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }
}

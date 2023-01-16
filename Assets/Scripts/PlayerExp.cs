using System;
using UnityEngine;

public class PlayerExp : MonoBehaviour
{
    public int minExp = 0;
    public int currentExp;
    public int levelUp = 1000;

    public Expbar expBar;

    public static PlayerExp instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de PlayerExp dans la scène");
            return;
        }
        instance = this; //ceci est un singleton : permet de lire cette class dans tout les autres scripts
    }
    void Start()
    {
        currentExp = minExp;
        expBar.SetMinExp(minExp);
        Inventory.instance.levelCount = 1;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            TakeExp(275);
        }
        if(currentExp >= 1000)
        {
            Inventory.instance.AddLevel(1);
            currentExp = minExp + currentExp - 1000;
            expBar.SetExp(currentExp);
        }
    }

    public void TakeExp(int xp)
    {
            currentExp += xp;
            expBar.SetExp(currentExp);
    }
}

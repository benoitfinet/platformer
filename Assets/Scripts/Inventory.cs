using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public int coinsCount;
    public int levelCount;
    public int xpCount;

    public Text coinsCountText;
    public static Inventory instance;

    public Text levelCountText;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de Inventory dans la scène");
            return;
        }
        instance = this; //ceci est un singleton : permet de lire cette class dans tout les autres scripts
    }

    public void AddLevel(int level)
    {
        levelCount += level;
        levelCountText.text = levelCount.ToString();
    }

    public void AddXP(int xpTotal)
    {
        xpCount += xpTotal;
    }

    public void AddCoins(int count)
    {
        coinsCount += count; //va envoyé directement à inventory le nombre de pièces ramassées
        coinsCountText.text = coinsCount.ToString();
    }

    public void RemoveCoins(int count)
    {
        coinsCount -= count;
        coinsCountText.text = coinsCount.ToString();
    }
}

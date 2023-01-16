using UnityEngine;

public class CurrentSceneManager : MonoBehaviour
{
    public bool isPlayerPresentByDefault = false;

    public int coinsPickedUpInThisSceneCount;

    public int levelUpInThisScene;

    public static CurrentSceneManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de CurrentSceneManager dans la scène");
            return;
        }
        instance = this; //ceci est un singleton : permet de lire cette class dans tout les autres scripts
    }
}

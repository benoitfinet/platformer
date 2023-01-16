using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyOnLoadScene : MonoBehaviour
{

    public GameObject[] objects;

    public static DontDestroyOnLoadScene instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de DontDestroyOnLoadScene dans la scène");
            return;
        }
        instance = this; //ceci est un singleton : permet de lire cette class dans tout les autres scripts

        foreach (var element in objects)
        {
            DontDestroyOnLoad(element);
        }
    }

    public void RemoveFromDontDestroyOnLoad()
    {
        foreach (var element in objects)
        {
            SceneManager.MoveGameObjectToScene(element, SceneManager.GetActiveScene());
        }
    }
}

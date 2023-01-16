using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverUI;

    public static GameOverManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de GameOverManager dans la scène");
            return;
        }
        instance = this; //ceci est un singleton : permet de lire cette class dans tout les autres scripts
    }

    public void OnPlayerDeath()
    {

        if (CurrentSceneManager.instance.isPlayerPresentByDefault)
        {
            DontDestroyOnLoadScene.instance.RemoveFromDontDestroyOnLoad();
        }
        StartCoroutine(youDied());
    }

    public IEnumerator youDied()
    {
        yield return new WaitForSeconds(1f);
        gameOverUI.SetActive(true);
    }

    public void RetryButton()
    {
        Inventory.instance.RemoveCoins(CurrentSceneManager.instance.coinsPickedUpInThisSceneCount);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);//recharge la scene actuelle
        gameOverUI.SetActive(false); //enlève le menu de gameover quand on clique sur le boutton
        PlayerHealth.instance.Respawn();
    }

    public void MainMenuButton()
    {
        DontDestroyOnLoadScene.instance.RemoveFromDontDestroyOnLoad();
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}

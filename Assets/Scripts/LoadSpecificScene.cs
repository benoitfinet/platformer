using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadSpecificScene : MonoBehaviour
{

    public string sceneName;
    public Animator fadeSystem;

    public Animator animator;

    public Text interactUI;

    private bool isInRange;

    public AudioClip endOfLevelSound;

    private void Awake()
    {
        fadeSystem = GameObject.FindGameObjectWithTag("FadeSystem").GetComponent<Animator>();
        interactUI = GameObject.FindGameObjectWithTag("InteractUI").GetComponent<Text>();
    }

    public void Update()
    {
        if(isInRange && Input.GetKeyDown(KeyCode.E))
        {
            PlayerMovement.instance.isUsingDoor = true;
            StartCoroutine(loadNextScene());
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            interactUI.enabled = true;
            isInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            interactUI.enabled = false;
            isInRange = false;
            PlayerMovement.instance.isUsingDoor = false;
        }
    }

    public IEnumerator loadNextScene()
    {
        AudioManager.instance.PlayClipAt(endOfLevelSound, transform.position);
        animator.SetTrigger("OpenDoor");
        fadeSystem.SetTrigger("FadeIn");
        PlayerMovement.instance.enabled = false;
        PlayerMovement.instance.rb.bodyType = RigidbodyType2D.Static;
        PlayerMovement.instance.enabled = false;
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(sceneName);
        PlayerMovement.instance.enabled = true;
        PlayerMovement.instance.isUsingDoor = false;
        PlayerMovement.instance.enabled = true;
        PlayerMovement.instance.rb.bodyType = RigidbodyType2D.Dynamic;
    }
}

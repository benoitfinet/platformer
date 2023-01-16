using UnityEngine;
using System.Collections;
public class DeathZone : MonoBehaviour
{
    private Transform playerSpawn;
    private Animator fadeSystem;
    private void Awake()
    {
        playerSpawn = GameObject.FindGameObjectWithTag("PlayerSpawn").transform;
        fadeSystem = GameObject.FindGameObjectWithTag("FadeSystem").GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            BetterJump.instance.fallMultiplier = -5f;
            StartCoroutine(ReplacePlayer(collision));
        }
    }

    private IEnumerator ReplacePlayer(Collider2D collision)
    {
        fadeSystem.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1f);
        collision.transform.position = playerSpawn.position;
        BetterJump.instance.fallMultiplier = 2.5f;
    }
}

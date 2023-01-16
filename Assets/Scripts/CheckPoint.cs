using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private Transform playerSpawn;
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void Awake()
    {
        playerSpawn = GameObject.FindGameObjectWithTag("PlayerSpawn").transform;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerSpawn.position = transform.position;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            anim.SetTrigger("CheckPointTrigger");
        }
    }
}

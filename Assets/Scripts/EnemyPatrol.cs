using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyPatrol : MonoBehaviour
{

    public float speed;
    public Transform[] waypoints;

    public Slider slider;

    public int damageOnCollision = 20;
    public int expOnDeath = 100;

    public int maxHealth = 100;
    int currentHealth;
    public SpriteRenderer graphics;

    public Animator anim;

    public Transform distanceotherEnemy;
    public float distanceotherEnemyRange = 0.5f;

    private Transform target;
    private int destPoint = 0;

    public AudioClip killSound;

    void Start()
    {
        target = waypoints[0];
        currentHealth = maxHealth;
        slider.maxValue = currentHealth;
    }



    void Update()
    {
        Vector3 dir = target.position - transform.position;
        if (!this.anim.GetCurrentAnimatorStateInfo(0).IsName("CrabAttack") && !this.anim.GetCurrentAnimatorStateInfo(0).IsName("CrabHurt"))
        { //si l'animation en question est en train de jouer ou pas
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World); //normalized = mettre le vecteur à 1
        }

        //Si l'ennemi est quasiment arrivé à sa destination :
        if (Vector3.Distance(transform.position, target.position) < 0.3f)
        {
            destPoint = (destPoint + 1) % waypoints.Length; //Permet de définir une sorte de boucle où le destpoint reviendra au premier waypoint une fois l'array parcouru
            target = waypoints[destPoint];
            graphics.flipX = !graphics.flipX;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        slider.value = currentHealth;

        anim.SetTrigger("Hurt");

        if(currentHealth <= 0)
        {
            AudioManager.instance.PlayClipAt(killSound, transform.position);
            Die();
            StartCoroutine(RemoveLifeBar());
        }
    }

    void Die()
    {
        anim.SetBool("IsDead", true);
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }

    IEnumerator RemoveLifeBar()
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
        PlayerExp.instance.TakeExp(expOnDeath);
        Debug.Log(expOnDeath);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.transform.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(damageOnCollision);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(distanceotherEnemy.position, distanceotherEnemyRange);
    }
}

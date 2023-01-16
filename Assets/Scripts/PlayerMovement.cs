using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;

    public bool isUsingDoor;
    public bool isJumping;
    public bool isGrounded;
    public bool isAttacking = false;

    public LayerMask enemyLayers;

    public int attackDamage = 40;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask collisionLayers;
    public CapsuleCollider2D playerCollider;

    public bool canReceiveInput;
    public bool inputReceived;

    public Rigidbody2D rb; // Référence pour le déplacement du personnage, "force dans tel direction pour le déplacer"
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    private Vector3 velocity = Vector3.zero;
    private float horizontalMovement;

    public static PlayerMovement instance;

    public AudioClip attackSound;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de PlayerMovement dans la scène");
            return;
        }
        instance = this; //ceci est un singleton : permet de lire cette class dans tout les autres scripts
    }

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.fixedDeltaTime; //Calcul du mouvement : Déplacement en fonction du clavier, mult par la vitesse. Time.deltaTime = aussi longtemps qu'on appuie sur la touche

        Flip(rb.velocity.x);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            isJumping = true;
        }

        Attack();
        

        if (isGrounded == false)
        {
            animator.SetBool("IsJumping", true);
        } else
        {
            animator.SetBool("IsJumping", false);
        }

        float characterVelocity = Mathf.Abs(rb.velocity.x); //renvoie toujours une vitesse positive
        animator.SetFloat("Speed", characterVelocity);

    }

    void FixedUpdate()
    {
        MovePlayer(horizontalMovement);

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, collisionLayers);//Créé une zone entre les deux éléments, boite de colision pour savoir si ça touche quelque chose (et donc ici passé isGrounded à true ou false pour savoir si l'on peut sauter)

    }

    public void Attack()
    {
        if (Input.GetKeyDown(KeyCode.A) && !isAttacking)
        {
            isAttacking = true;
            AudioManager.instance.PlayClipAt(attackSound, transform.position);
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

            foreach (Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<EnemyPatrol>().TakeDamage(attackDamage);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    void MovePlayer(float _horizontalMovement)
    {
        if (!isUsingDoor)
        {
            Vector3 targetVelocity = new Vector2(_horizontalMovement, rb.velocity.y);//Estime la velocité de la cible, cad estimer à quel endroit le personnage doit faire son prochain mouvement
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, .05f);

            if(isJumping)
            {
                rb.AddForce(new Vector2(0f, jumpForce));
                isJumping = false;
            }
        }
    }

    void Flip(float _velocity)
    {
        if (_velocity > 0.1f)
        {
            transform.localScale = new Vector3(1, 1, 1);
        } else if(_velocity < -0.1f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}

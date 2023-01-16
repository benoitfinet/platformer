using UnityEngine;

public class BetterJump : MonoBehaviour
{

    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    Rigidbody2D rb;

    public static BetterJump instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de BetterJump dans la scène");
            return;
        }
        rb = GetComponent<Rigidbody2D>();
        instance = this; //ceci est un singleton : permet de lire cette class dans tout les autres scripts
    }

    void Update()
    {
        if(rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        } else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }
}

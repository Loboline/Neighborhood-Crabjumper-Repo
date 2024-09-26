using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    [SerializeField] private LayerMask platformsLayerMask;
    private Rigidbody2D rigidbody2d;
    private CapsuleCollider2D CapsuleCollider2d;
    public Animator animator;
    public bool isJumping = true;
    public GameObject GameOver, RestartButton; //Skapar var för Gameobjects


    // Start is called before the first frame update

    void Start()
    {
        GameOver.SetActive(false); //sätter knapp att vara icke aktiva från början (då playern inte är död än)
        RestartButton.SetActive(false); //sätter knapp att vara icke aktiva från början (då playern inte är död än)
    }

    private void Awake()
    {
        rigidbody2d = transform.GetComponent<Rigidbody2D>();
        CapsuleCollider2d = transform.GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
     void Update()
    {

        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("isJumping", true);
            float jumpVelocity = 40f;
            rigidbody2d.velocity = Vector2.up * jumpVelocity; //istället för up kan vi också sätta t.ex left, right, one (snett upåt), zero m.m.
        } else
        {
            animator.SetBool("isJumping", false);
        }

        HandleMovement(); // Behövs här för att Update håller utkik hela tiden efter om något händer. T.ex. en knapptryckning. Tror det heter att den kallar på en funktion.
    }

    private bool IsGrounded()
    {
        RaycastHit2D raycastHit2d = Physics2D.BoxCast(CapsuleCollider2d.bounds.center, CapsuleCollider2d.bounds.size, 0f, Vector2.down, .1f, platformsLayerMask);
        Debug.Log(raycastHit2d.collider);
        return raycastHit2d.collider != null;
    }

  void HandleMovement () // Denna funktion fixar movement i sidoled. Kom ihåg! En void returnar ingen info, utan används oftast för att göra en action av något slag
    {
        float moveSpeed = 10f;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rigidbody2d.velocity = new Vector2(-moveSpeed, rigidbody2d.velocity.y);
            animator.SetFloat("Horizontal", Input.GetAxis("Horizontal"));  // Denna rad behövs för att animatorn ska veta att gubben rör sig, och åt vilket håll.
        } else
            {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                rigidbody2d.velocity = new Vector2(+moveSpeed, rigidbody2d.velocity.y);
                animator.SetFloat("Horizontal", Input.GetAxis("Horizontal"));
            }
            else
            {
                rigidbody2d.velocity = new Vector2(0, rigidbody2d.velocity.y);
                animator.SetFloat("Horizontal", Input.GetAxis("Horizontal"));

            }
        }

    }

    void OnCollisionEnter2D (Collision2D col)
    {
        if (col.gameObject.tag.Equals("Crab"))
        {
            GameOver.SetActive(true);
            RestartButton.SetActive(true);
            gameObject.SetActive(false); //hänvisar till hjälten som scriptet förhoppningsvis sitter på
            MainManager.Instance.CheckHighscore(); //Sparar den sist valda namnet i MainManager.
            MainManager.Instance.Save(); //Sparar den sist valda namnet i MainManager.
        }
    }
}

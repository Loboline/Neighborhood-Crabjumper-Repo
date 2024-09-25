using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crab : MonoBehaviour
{

    [SerializeField] private LayerMask platformsLayerMask;
    private Rigidbody2D rigidbody2d;
    private BoxCollider2D boxCollider2d;
    public int crabmove = 2;

    // Start is called before the first frame update
    private void Awake()
    {
        rigidbody2d = transform.GetComponent<Rigidbody2D>();
        boxCollider2d = transform.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement(); // Behövs här för att Update håller utkik hela tiden efter om något händer. T.ex. en knapptryckning. Tror det heter att den kallar på en funktion.
    }

    private bool IsGrounded()
    {
        RaycastHit2D raycastHit2d = Physics2D.BoxCast(boxCollider2d.bounds.center, boxCollider2d.bounds.size, 0f, Vector2.down, .1f, platformsLayerMask);
        Debug.Log(raycastHit2d.collider);
        return raycastHit2d.collider != null;
    }

    void HandleMovement() // Denna funktion fixar movement i sidoled. Kom ihåg! En void returnar ingen info, utan används oftast för att göra en action av något slag
    {
        float moveSpeed = 10f;
        rigidbody2d.velocity = new Vector2(-moveSpeed, rigidbody2d.velocity.y);
    }
}

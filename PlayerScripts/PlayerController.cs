using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Movement variables
    public float speed;
    public float runSpeedMultiplier;
    private Rigidbody2D rb;
    private Vector2 moveVelocity;

    //Animation variables
    [SerializeField]
    public Animator playerAnimator;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (Input.GetKey("space") == false)
        {
            runSpeedMultiplier = 1;
        } else //Running while space is held
        {
            runSpeedMultiplier = 2;
        }

        moveVelocity = moveInput.normalized * speed;

        //ANIMATION COMPONENTS
        playerAnimator.SetFloat("Vertical", Input.GetAxisRaw("Vertical"));
        playerAnimator.SetFloat("Horizontal", Input.GetAxisRaw("Horizontal"));


        //MOVEMENT: The part that makes the player move.
        if (GetComponent<ObjectProperties>().onSlope == false)
        {
            rb.MovePosition(rb.position + moveVelocity * runSpeedMultiplier * Time.fixedDeltaTime);
        }
        else if (GetComponent<ObjectProperties>().onSlope == true)
        {
            rb.MovePosition(rb.position + (moveVelocity + (Vector2.up*GetComponent<ObjectProperties>().diffFloorHeight*18)) * runSpeedMultiplier * Time.fixedDeltaTime);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectProperties : MonoBehaviour
{

    [SerializeField]
    public float height;

    public float currFloorHeight;
    public float desiredFloorHeight;
    public float diffFloorHeight;
    public float floorHeightThreshold;
    public Rigidbody2D rb;

    public float yval;
    public Vector2 heightpos;

    public bool onSlope;

    //Damage and recovery
    public bool invincible;
    public float recoveryTime;
    //THE FOLLOWING IS TRUE IF THE hitRecovery COROUTINE IS RUNNING.
    private bool hitRecoveryCR_running;

    //NOTE: Bounciness must be a number between 0 and 1.
    [SerializeField]
    public float bounciness;

    void Start()
    {
        yval = transform.position.y;

        onSlope = false;
        invincible = false;
        hitRecoveryCR_running = false;
        recoveryTime = 1f;

        rb = GetComponent<Rigidbody2D>();

        heightpos = new Vector2(transform.position.x, transform.position.y + height);

        //transform.position = new Vector2(transform.position.x, transform.position.y + height);
        transform.position = heightpos; 

    }

    private void Update()
    {
        diffFloorHeight = 0;

        if (invincible == true)
        {
            if (hitRecoveryCR_running == false)
            {
                StartCoroutine(hitRecovery(recoveryTime));
                GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f);
            }

        }

        //Checks if the player is currently on a slope.

        if (this.gameObject.tag == "Player")
        {
            onSlope = GameObject.Find("PlayerLegs").GetComponent<onSlopeOrNot>().onSlope;
        } else
        {
            onSlope = this.gameObject.GetComponent<onSlopeOrNot>().onSlope;
        }

        if (onSlope == true)
        {

            //NOTE: Only if the slope is bottom to up from left to right.
            
            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                desiredFloorHeight = currFloorHeight + floorHeightThreshold;
            }
            else if (Input.GetAxisRaw("Horizontal") < 0)
            {
                desiredFloorHeight = currFloorHeight - floorHeightThreshold;
            }
        }

        //FLOORHEIGHT PART: Adjust the object's position to match its floorheight.
        if (currFloorHeight != desiredFloorHeight)
        {
            adjustFloorHeight();
        }



        //THE FOLLOWING CODE IS TO CALCULATE THE YVAL ACCORDING TO THE HEIGHT OF THE OBJECT.
        //THE ACTUAL HEIGHT IS CALCULATED IN GRAVITY.CS, ALONG WITH THE GRAVITY CODE.

        
        height = GetComponent<Gravity>().height;
        heightpos = GetComponent<Gravity>().heightpos;
        

        if (height > 0)
        {
            yval = transform.position.y - height;
        } else if (height == 0)
        {
            //This is what changes the yval incorrectly. Must make it so that it knows when the yval isn't as it should be.
            yval = transform.position.y;
        } else
        {
            height = 0;
        }

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Slope")
        {
            floorHeightThreshold = collision.GetComponent<newSlopeScript>().floorHeightThreshold;
        }

        if (collision.tag == "Platform")
        {
            desiredFloorHeight = collision.GetComponent<platformScript>().floorHeight;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Slope")
        {
            floorHeightThreshold = 0;
        }
    }

    IEnumerator hitRecovery(float waitTime)
    {
        hitRecoveryCR_running = true;

        yield return new WaitForSeconds(waitTime);
        invincible = false;
        GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);

        hitRecoveryCR_running = false;
    }

    public void adjustFloorHeight()
    {
        diffFloorHeight = desiredFloorHeight - currFloorHeight;
        currFloorHeight = desiredFloorHeight;
        return;
    }

    public void changeYVal(float newYVal)
    {
        this.yval = newYVal;
        return;
    }


}

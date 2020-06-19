using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    public float height;
    [SerializeField]
    private float fallCounter;
    [SerializeField]
    private float gravityScale;
    public Vector2 heightpos;
    private float yval;
    private float bounciness;

    //The maximum height of the bounce, reset every bounce.
    private float maxHeight;

    // Start is called before the first frame update
    void Start()
    {
        heightpos = GetComponent<ObjectProperties>().heightpos;
        height = GetComponent<ObjectProperties>().height;
        bounciness = GetComponent<ObjectProperties>().bounciness;
        maxHeight = 0;
    }

    private void Update()
    {
        height = GetComponent<ObjectProperties>().height;
        heightpos = GetComponent<ObjectProperties>().heightpos;

        yval = GetComponent<ObjectProperties>().yval;

        //JUMP!
        if ((Input.GetKeyDown(KeyCode.L) == true) && (gameObject.tag != "Player"))
        {
            Jump(20);
        }


        //Update the maximum height:
        if (maxHeight < height)
        {
            maxHeight = height;
        }

        if (height > 0)
        {
            height = height - (fallCounter * Time.fixedDeltaTime);
            if (height <= 0) {
                height = 0;
            } else
            {
                fallCounter = fallCounter + gravityScale;
            }
        } else if (height == 0)
        {
            //NOTE: This means that all objects must have bounciness above 0.1 if you want them to bounce.
            if (((maxHeight < 0.5) && (maxHeight > -0.5))||(bounciness <= 0.1))
            {
                fallCounter = 0;
                maxHeight = 0;
            }

            if (fallCounter != 0)
            {
                fallCounter = -fallCounter*bounciness;
                fallCounter = fallCounter + gravityScale;
            }

            height = height - (fallCounter * Time.fixedDeltaTime);
            maxHeight = 0;
        }

        //heightpos.x = transform.position.x;
        
        //heightpos.y = yval + height;

        if (height > 0)
        {
            transform.position = new Vector2(transform.position.x, yval + height);
        }

    }

    public void Jump(float force)
    {
        fallCounter = -force;
        height = height - (fallCounter * Time.fixedDeltaTime);
        return;
    }
}

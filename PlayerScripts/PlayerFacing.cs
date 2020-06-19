using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFacing : MonoBehaviour
{
    private bool freeKey;
    public enum facingDir { UP, DOWN, LEFT, RIGHT };
    public facingDir playerFacingDir;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        freeKey = true;
        playerFacingDir = facingDir.DOWN;
    }

    // Update is called once per frame
    void Update()
    { 


        //If player is not moving, set the freeKey variable to true to indicate that the direction he/she faces can change.
        if ((Input.GetKey(KeyCode.DownArrow) == false)&&(Input.GetKey(KeyCode.UpArrow) == false)&&(Input.GetKey(KeyCode.LeftArrow) == false)&&(Input.GetKey(KeyCode.RightArrow) == false))
        {
            freeKey = true;
        }

        //This sequence of if/else if's detect if the player lets go of a directional key but is still moving.
        if ((playerFacingDir == facingDir.DOWN) && (Input.GetKey(KeyCode.DownArrow) == false))
        {
            freeKey = true;
        } else if ((playerFacingDir == facingDir.UP) && (Input.GetKey(KeyCode.UpArrow) == false)) {
            freeKey = true;
        } else if ((playerFacingDir == facingDir.LEFT) && (Input.GetKey(KeyCode.LeftArrow) == false)) {
            freeKey = true;
        } else if ((playerFacingDir == facingDir.RIGHT) && (Input.GetKey(KeyCode.RightArrow) == false)) {
            freeKey = true;
        }

        //If the key is free, the direction of the player's movement can now be set.
        //If they are moving up, for example, and they start moving right at the same time, they'll still face the first direction
        //they faced even if they're moving diagonally.
        if (freeKey == true)
        {
            if (Input.GetKey(KeyCode.DownArrow) == true)
            {
                freeKey = false;
                playerFacingDir = facingDir.DOWN;
                //Debug.Log("Player facing DOWN");
            }
            else if (Input.GetKey(KeyCode.UpArrow) == true)
            {
                freeKey = false;
                playerFacingDir = facingDir.UP;
                //Debug.Log("Player facing UP");
            }
            else if (Input.GetKey(KeyCode.LeftArrow) == true)
            {
                freeKey = false;
                playerFacingDir = facingDir.LEFT;
                //Debug.Log("Player facing LEFT");
            } 
            else if (Input.GetKey(KeyCode.RightArrow) == true)
            {
                freeKey = false;
                playerFacingDir = facingDir.RIGHT;
                //Debug.Log("Player facing RIGHT");
            }
        }

    }
}

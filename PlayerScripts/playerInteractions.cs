using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerInteractions : MonoBehaviour
{
    private string otherObjectType;
    private GameObject player;

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    //THIS IS TO TEST USING SECONDARY ITEMS.
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J)) {
            if (player.GetComponent<playerInventory>().equippedSecondary != "")
            {
                useSecondaryObject(player.GetComponent<playerInventory>().equippedSecondaryObject, player.GetComponent<playerInventory>().equippedSecondary);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PickupObject")
        {
            otherObjectType = collision.gameObject.GetComponent<interactObjectInfo>().objectType;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //If the object is "pickup-able"
        if (collision.gameObject.tag == "PickupObject")
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                if (otherObjectType == "STICK")
                {
                    pickUpPrimary(collision.gameObject, "STICK");
                }
                if (otherObjectType == "PINECONE")
                {
                    pickUpSecondary(collision.gameObject, "PINECONE");
                }
            }
        }
    }

    private int pickUpPrimary(GameObject item, string objectType)
    {
        //Set active
        if (objectType == "STICK")
        {
            player.GetComponent<playerInventory>().obtainItem(item, "STICK");
        }

        //Removes the item from the scene.
        item.SetActive(false);
        return 1;
    }

    private int pickUpSecondary(GameObject item, string objectType)
    {
        if (objectType == "PINECONE")
        {
            player.GetComponent<playerInventory>().obtainItem(item, "PINECONE");
        }

        item.SetActive(false);
        return 1;
    }

    
    private void useSecondaryObject(GameObject item, string objectType)
    {
        //Throw pinecone in the air.
        if (objectType == "PINECONE")
        {
            player.GetComponent<playerInventory>().dropItem("SECONDARY");
            Vector3 playerLegsPos = this.transform.position;


            item.GetComponent<ObjectProperties>().height = 15;

            //Spawn pinecone in the scene view, throw it upwards.
            if (player.GetComponent<PlayerFacing>().playerFacingDir == PlayerFacing.facingDir.DOWN)
            {
                item.GetComponent<ObjectProperties>().changeYVal(playerLegsPos.y - 7);
                //item.GetComponent<ObjectProperties>().yval = playerLegsPos.y - 5;
                item.transform.position = new Vector3(playerLegsPos.x, playerLegsPos.y-5, item.transform.position.z);
            } else if (player.GetComponent<PlayerFacing>().playerFacingDir == PlayerFacing.facingDir.UP) {
                item.GetComponent<ObjectProperties>().changeYVal(playerLegsPos.y + 5);
                item.transform.position = new Vector3(playerLegsPos.x, playerLegsPos.y+5, item.transform.position.z);
            }
            else if (player.GetComponent<PlayerFacing>().playerFacingDir == PlayerFacing.facingDir.LEFT) {
                item.GetComponent<ObjectProperties>().changeYVal(playerLegsPos.y-5);
                item.transform.position = new Vector3(playerLegsPos.x-5, playerLegsPos.y, item.transform.position.z);
            }
            else if (player.GetComponent<PlayerFacing>().playerFacingDir == PlayerFacing.facingDir.RIGHT) {
                item.GetComponent<ObjectProperties>().changeYVal(playerLegsPos.y-5);
                item.transform.position = new Vector3(playerLegsPos.x+5, playerLegsPos.y, item.transform.position.z);
            }
           

            item.SetActive(true);
            item.GetComponent<Gravity>().Jump(30);



        }

    }

}

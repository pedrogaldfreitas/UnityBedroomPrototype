using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onSlopeOrNot : MonoBehaviour
{

    //SLOPE VARIABLES
    public bool onSlope;

    // Start is called before the first frame update
    void Start()
    {
        onSlope = false;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Slope")
        {
            onSlope = true;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Slope")
        {
            onSlope = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionRendererSort : MonoBehaviour
{
    [SerializeField]
    private int SortingOrderBase;
    //"offset" fixes the 'origin' of the player object.
    [SerializeField]
    private int offset;
    private Renderer myRenderer;

    private float height;
    private GameObject shadow;


    private void Awake()
    {
        myRenderer = gameObject.GetComponent<Renderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        shadow = null;
        //Find the Shadow child object of the current object.
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            if (child.tag == "Shadow")
            {
                shadow = transform.GetChild(i).gameObject;
                break;
            }
        }
    }

    private void Update()
    {
        if ((this.gameObject.tag != "Shadow")&&(this.gameObject.tag != "bodypart"))
        {
            height = GetComponent<ObjectProperties>().height;
        }
    }

    void LateUpdate()
    {
        if (height > 0)
        {
            //FIX: Instead of giving the shadows a PositionRendererSort.cs component, make the original object's positionrenderersort also change the shadow's sorting order.
            //This way, the shadow can be rid of its Gravity.cs, objectproperties.cs, and positionrenderersort.cs components.
            myRenderer.sortingOrder = (int)(SortingOrderBase - GetComponent<ObjectProperties>().yval - offset);
            //myRenderer.sortingOrder = (shadow.GetComponent<Renderer>().sortingOrder);
        }
        else
        {
            myRenderer.sortingOrder = (int)(SortingOrderBase - transform.position.y - offset);
        }

    }
}

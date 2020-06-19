using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bodyPartScript : MonoBehaviour
{
    private int footSortingOrder;
    private Transform parentTransform;
    private Renderer thisRenderer;
    private Renderer legsRenderer;

    private void Start()
    {
        /* 
         * Goal: Find character leg object's sorting order, as it is the only one with
         *       the positionRendererSort component. Then, set this object's
         *       sorting order to the same as the legs' sorting order every frame update.
        */
        thisRenderer = GetComponent<Renderer>();
        parentTransform = transform.parent;
        if (this.gameObject.tag == "bodypart")
        {
            legsRenderer = parentTransform.Find("PlayerLegs").gameObject.GetComponent<Renderer>();
        }
        

    }

    private void Update()
    {
        if (this.gameObject.tag == "bodypart")
        {
            thisRenderer.sortingOrder = legsRenderer.sortingOrder + 1;
        }
    }
}

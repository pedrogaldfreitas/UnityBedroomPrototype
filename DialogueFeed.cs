using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueFeed : MonoBehaviour
{

    //Eg.: diag

    // VICTOR/DAY1MORN
    public Dialogue diagV1M0;
    public Dialogue diagV1M1;
    public Dialogue diagV1M2;
    public Dialogue diagV1M3;

    private void Start()
    {
        
    }

    public Dialogue getProperDialogueOptionVictor()
    {
        Dialogue diag;

        //PUT CODE INSTEAD:
        diag = diagV1M0;

        return diag;
    }

}

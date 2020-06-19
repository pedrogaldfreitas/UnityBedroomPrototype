using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquirrelAI : MonoBehaviour
{

    public float speed;
    public Vector2 moveSpot;

    public enum State {WANDER, CHASE, ATTACK};
    public State squirrelState;

    private float waitTime;
    private float startWaitTime;
    public float minX;
    public float minY;
    public float maxX;
    public float maxY;

    // Start is called before the first frame update
    void Start()
    {

        startWaitTime = Random.Range(0f, 5f);
        waitTime = startWaitTime;
        minX = transform.position.x - 9;
        maxX = transform.position.x + 9;
        minY = transform.position.y - 9;
        maxY = transform.position.y + 9;
        moveSpot = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
    }

    // Update is called once per frame
    void Update()
    {
        //WANDER STATE
        if (squirrelState == State.WANDER)
        {
            //MOVE
            if (GetComponent<ObjectProperties>().height > 0)
            {
                //To ensure the squirrel jumps properly while moving.
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(moveSpot.x, moveSpot.y + GetComponent<ObjectProperties>().height), speed * Time.deltaTime);
            } else
            {
                transform.position = Vector2.MoveTowards(transform.position, moveSpot, speed * Time.deltaTime);
            }

            //WAIT
            if (Vector2.Distance(transform.position, moveSpot) < 0.2f)
            {

                if (waitTime <= 0)
                {
                    moveSpot = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
                    startWaitTime = Random.Range(0f, 5f);
                    waitTime = startWaitTime;
                }
                else
                {
                    waitTime -= Time.deltaTime;
                }
            }
        }

        //CHASE STATE
        if (squirrelState == State.CHASE)
        {
            if (GetComponent<ObjectProperties>().height > 0)
            {
                //ERROR: Squirrel's y position doesn't move while height > 0.
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(GameObject.Find("PlayerLegs").transform.position.x, GameObject.Find("PlayerLegs").transform.position.y + GetComponent<ObjectProperties>().height), speed * Time.deltaTime*3);
            } else
            {
                transform.position = Vector2.MoveTowards(transform.position, GameObject.Find("PlayerLegs").transform.position, speed * Time.deltaTime * 3);
            }
        }
    }

}

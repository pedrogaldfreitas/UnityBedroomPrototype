using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private float timeBtwAttack;
    public float startTimeBtwAttack;

    public LayerMask whatIsEnemies;
    private GameObject attackPos;
    public Transform attackCirclePos;
    public float attackRange;

    public Animator attackAnimator;

    private playerInventory playerInventory;

    private PlayerFacing PlayerFacing;

    public int damage;

    private void Start()
    {
        timeBtwAttack = 0;
        attackPos = GameObject.Find("AttackPos");
        PlayerFacing = GetComponent<PlayerFacing>();

        playerInventory = this.GetComponent<playerInventory>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInventory.activePrimary == "STICK")
        {
            if ((Input.GetKeyDown(KeyCode.Z) == true) && (timeBtwAttack <= 0))
            {
                attackAnimator.SetTrigger("Zkey");

                //If player is facing forwards, move the attack position to the proper position.
                if (PlayerFacing.playerFacingDir == PlayerFacing.facingDir.DOWN)
                {
                    Debug.Log("Attacked Down.");
                    attackPos.transform.position = new Vector3(transform.position.x, transform.position.y - 0.19f, transform.position.z);
                }
                else if (PlayerFacing.playerFacingDir == PlayerFacing.facingDir.UP)
                {
                    Debug.Log("Attacked Up.");
                    attackPos.transform.position = new Vector3(transform.position.x, transform.position.y - 0.19f, transform.position.z);
                }
                else if (PlayerFacing.playerFacingDir == PlayerFacing.facingDir.LEFT)
                {
                    Debug.Log("Attacked Left.");
                    attackPos.transform.position = new Vector3(transform.position.x - 0.16f, transform.position.y - 0.19f, transform.position.z);
                }
                else if (PlayerFacing.playerFacingDir == PlayerFacing.facingDir.RIGHT)
                {
                    Debug.Log("Attacked Right.");
                    attackPos.transform.position = new Vector3(transform.position.x + 0.16f, transform.position.y - 0.19f, transform.position.z);
                }


                //The actual attack code.

                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackCirclePos.position, attackRange, whatIsEnemies);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    if (PlayerFacing.playerFacingDir == PlayerFacing.facingDir.DOWN)
                    {

                        if (enemiesToDamage[i].transform.position.y <= attackCirclePos.position.y)
                        {
                            enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(damage);
                        }
                    }
                    else if (PlayerFacing.playerFacingDir == PlayerFacing.facingDir.UP)
                    {
                        if (enemiesToDamage[i].transform.position.y >= attackCirclePos.position.y)
                        {
                            enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(damage);
                        }
                    }
                    else if (PlayerFacing.playerFacingDir == PlayerFacing.facingDir.LEFT)
                    {
                        if (enemiesToDamage[i].transform.position.x <= attackCirclePos.position.x)
                        {
                            enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(damage);
                        }
                    }
                    else if (PlayerFacing.playerFacingDir == PlayerFacing.facingDir.RIGHT)
                    {
                        if (enemiesToDamage[i].transform.position.x >= attackCirclePos.position.x)
                        {
                            enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(damage);
                        }
                    }
                }
                timeBtwAttack = startTimeBtwAttack;
            }


            if (timeBtwAttack > 0)
            {
                timeBtwAttack -= Time.deltaTime;
            }
            else if (timeBtwAttack < 0)
            {
                timeBtwAttack = 0;
            }
        }
        

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackCirclePos.position, attackRange);
    }

}

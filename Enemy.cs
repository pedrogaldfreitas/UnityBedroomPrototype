using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    public int touchDamage;
    public int enemyHealth;


    void Update()
    {
        if (enemyHealth <= 0) {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if ((collision.gameObject.tag == "Player")&&(collision.GetType() == typeof(BoxCollider2D)))
        {
            DamageOther(collision.gameObject, touchDamage);
        }
    }

    public void TakeDamage(int dmg)
    {
        enemyHealth -= dmg;
        Debug.Log("Enemy damaged, health at " + enemyHealth + ".");
        return;
    }

    //Currently only works when damaging the player.
    public void DamageOther(GameObject other, int dmg)
    {
        if ((other.tag == "Player")&&(other.GetComponent<ObjectProperties>().invincible == false))
        {
            other.GetComponent<PlayerHealth>().damageHealth(dmg);
            other.GetComponent<ObjectProperties>().invincible = true;
        }
        return;
    }
}

using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    private Transform player;
    //PlayerHealth playerHealth;
    //EnemyHealth enemyHealth;
    UnityEngine.AI.NavMeshAgent nav;


    void Awake ()
    {
        player = GameObject.FindWithTag(TagNames.Player).transform;
        //playerHealth = player.GetComponent <PlayerHealth> ();
        //enemyHealth = GetComponent <EnemyHealth> ();
        nav = GetComponent <UnityEngine.AI.NavMeshAgent> ();
    }


    void Update ()
    {
        //if(enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
        //{
            nav.SetDestination (player.position);
        //}
        //else
        //{
        //    nav.enabled = false;
        //}
    }
}
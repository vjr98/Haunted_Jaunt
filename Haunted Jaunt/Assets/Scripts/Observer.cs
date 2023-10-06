using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{
    public Transform player; //to detect the player
    bool m_IsPlayerInRange; //to detect if player is in range
    public GameEnding gameEnding; //ends game

    //method for when player gets within trigger's range
    void OnTriggerEnter(Collider other)
    {
        if(other.transform == player)
        {
            m_IsPlayerInRange = true;
        }
    }

    //method for when player leaves trigger's range
    void OnTriggerExit(Collider other)
    {
        if(other.transform == player)
        {
            m_IsPlayerInRange = false;
        }
    }

    //method which updates every 1 frame
    void Update()
    {
        //checks to see if player is in range
        if (m_IsPlayerInRange)
        {
            //sets the direction of enemy view
            Vector3 direction = player.position - transform.position + Vector3.up;
            //new ray
            Ray ray = new Ray(transform.position, direction);
            //defines raycast var
            RaycastHit raycastHit;

            //define ray &
            if (Physics.Raycast(ray, out raycastHit))
            {
                //ends game if enemy hits player
                if(raycastHit.collider.transform == player)
                {
                    gameEnding.CaughtPlayer();
                }
            }
        }
    }
}

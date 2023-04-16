using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    ThirdPersonController[] players;
    ThirdPersonController nearestPlayer;

    public float speed;

    private void Start()
    {
          
        players = FindObjectsOfType<ThirdPersonController>(); //finds all players in the scene, because we are going to use them all.

    }

    private void  Update()
    {

        float distanceOne = Vector3.Distance(transform.position, players[0].transform.position);
        float distanceTwo = Vector3.Distance(transform.position, players[1].transform.position);

        if(distanceOne < distanceTwo)
        {
             nearestPlayer = players[0];
        }
        else
        {
            nearestPlayer = players[1];
        }
        if (nearestPlayer != null)
        {
            transform.position =Vector3.MoveTowards(transform.position, nearestPlayer.transform.position, speed * Time.deltaTime);
        }

    
    }
}

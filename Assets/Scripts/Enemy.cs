using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;
using Photon.Pun;

public class Enemy : MonoBehaviour
{
    ThirdPersonController[] players;
    ThirdPersonController nearestPlayer;

    public float speed;

    Score score;

    public GameObject deathFX;
    PhotonView view;  

    private void Start()
    {
          
        players = FindObjectsOfType<ThirdPersonController>(); //finds all players in the scene, because we are going to use them all.
        score = FindObjectOfType<Score>(); //finds the score object.
        view = GetComponent<PhotonView>();

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

    private void OnCollisionEnter(Collision collision)
    {
        if(PhotonNetwork.IsMasterClient)
        {
        if (collision.gameObject.tag == "GoldenRay")
        {
            score.AddScore();
            view.RPC("SpawnParticle", RpcTarget.All);
            PhotonNetwork.Destroy(this.gameObject);
        }
        }
    }
[PunRPC]
    void SpawnParticle()
    {
        Instantiate(deathFX, transform.position, Quaternion.identity);
    }

}

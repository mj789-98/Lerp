using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
   public GameObject player;
   public float minX, minZ, maxX, maxZ, minY, maxY;

    private void Start()
    {
   Vector3 randomPosition = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), Random.Range(minZ, maxZ));

   PhotonNetwork.Instantiate(player.name, randomPosition, Quaternion.identity);
  

    }
}

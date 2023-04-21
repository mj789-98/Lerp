using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMesh : MonoBehaviour
{
    [SerializeField] private Transform movePositionTransform;
    public NavMeshAgent navMeshAgent;
  private void Awake(){
      GetComponent<NavMeshAgent>(); 
  }

  private void Update(){

      navMeshAgent.destination = movePositionTransform.position;
  }  
}

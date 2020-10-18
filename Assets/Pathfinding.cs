using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    public Transform[] points;

    private UnityEngine.AI.NavMeshAgent nav;
    private int destPoint;
    private Vector3 initialPos;
    private bool isReset;
    private Renderer renderer;

    // Start is called before the first frame update
    void Start()
    {
      nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
      initialPos = transform.position;
      GoToNextPoint();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
      if(!nav.pathPending && nav.remainingDistance < 0.5f){
        // Change material color of points after arrived
        if(!isReset) {
          ChangeColor();
          if(destPoint > 0) {
            GoToNextPoint();
          } else {
            Reset();
          }
        } else {
          GoToNextPoint();
          isReset = false;
        }
      }
    }

    void GoToNextPoint(){
      if (points.Length == 0)
      {
        return;
      }

      nav.destination = points[destPoint].position;
      destPoint = (destPoint + 1) % points.Length;
    }

    void Reset(){
      nav.destination = initialPos;
      isReset = true;
    }

    void ChangeColor(){
      if(destPoint > 0)
        renderer = points[destPoint- 1].GetChild(0).GetComponent<Renderer>();
      else
        renderer = points[points.Length - 1].GetChild(0).GetComponent<Renderer>();

      if(renderer.material.color == Color.blue)
        renderer.material.color = Color.red;
      else
        renderer.material.color = Color.blue;
    }
}

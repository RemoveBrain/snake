using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class tail : MonoBehaviour
{
    public Vector3 followPos;
    public int followDelay;
    public Transform parent;
    public Queue<Vector3> parentPos;
    private int sw=0;   //최초 한번 분류 스위치
    public ParticleSystem tail_e;
    void Start()
    {
        //transform.position = parent.position;
        sw=0;
    }
    void Awake()
    {
        parentPos = new Queue<Vector3>();
    }
    void Update ()
    {
        Watch();
       // Follow();
    }
    void Watch()
    {
        if(!parentPos.Contains(parent.position))
            parentPos.Enqueue(parent.position);

        if(parentPos.Count > followDelay) {     
            followPos = parentPos.Dequeue();
            Follow();
            
        }
    }
    void Follow()
    {
        transform.position = followPos;
        transform.rotation = parent.rotation;
        if(sw==0)
        {
            tail_e = transform.GetChild(0).GetComponent<ParticleSystem>();  // 현재 오브젝트의 자식의 ParticleSystem 컴포넌트를 찾기. 
            tail_e.Play();
            sw=1;
        }
    }
    
}
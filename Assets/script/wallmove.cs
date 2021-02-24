using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallmove : MonoBehaviour
{
    float a=0,c=0;
    // Start is called before the first frame update
    void Start()
    {
        a=Random.Range(-1f,1f);
        c=Random.Range(-1f,1f);
        right();  
    }

    // Update is called once per frame 
    void Update()
    {
        transform.Translate(new Vector3(a,0,c) * Time.deltaTime * 0.5f);
    }
    
    void right()
    {
        a*=-1;
        c*=-1;
        Invoke("left",1);
    }
    void left()
    {
        a*=-1;
        c*=-1;
        Invoke("right",1);
    }
    public void Random_ac()
    {
        a=Random.Range(-1f,1f);
        c=Random.Range(-1f,1f);
    }
}

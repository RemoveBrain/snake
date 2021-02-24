using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class eatcube : MonoBehaviour
{
    // Start is called before the first frame update
    public int l = 1,t=25,G=0;
    //public GameObject obj;
    public GameObject[] tai;
    public int inq=0;
    public GameObject eat;
    public GameObject feed;
    public GameObject item;
    private GameObject Des;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other) {
        if(other.tag=="Player") 
        {
            float x=0,y=0,z=0; // 포지션
            float a=0,c=0; // 로테이션
            int r=1;
            r=Random.Range(1,6);  // 랜덤 면 지정
            G+=Random.Range(1,2); // 아이템 발생 게이지
            if(l==r)            // 같은면 안나오게하기
            {
                r=r%6+1;
            }
            l=r;
            a=0;c=0;
            switch (r) { 
                    case 1:
            //노란면
            x=Random.Range(-0.455f,0.443f);
            y=0.53f;
            z=Random.Range(-0.455f,0.461f);
                    c=0;
                    break;
                    case 2:
            //초록면
            x=-0.53f;
            y=Random.Range(0.443f,-0.414f);
            z=Random.Range(0.442f,-0.44f);
                    c=90;
                    break;
                    case 3:
            //빨간면
            x=Random.Range(-0.452f,0.436f);
            y=Random.Range(0.462f,-0.448f);
            z=-0.53f;
                    a=270;
                    break;
                    case 4:
            //파란면
            x=0.53f;
            y=Random.Range(0.458f,-0.444f);
            z=Random.Range(-0.459f,0.445f);
                    c=270;
                    break;
                    case 5:
            //하얀면
            x=Random.Range(0.455f,-0.455f);
            y=Random.Range(0.456f,-0.434f);
            z=0.53f;
                    a=90;
                    break;
                    case 6:
            //보라면
            x=Random.Range(0.447f,-0.445f);
            y=-0.53f;
            z=Random.Range(0.447f,-0.429f);
                    c=180;
                    break;
            }
            
            
            Des=Instantiate(eat,transform.position,Quaternion.identity);  // 먹는 이펙트 복제해서 가져오기
            Destroy(Des,5);                                                // 5초뒤 삭제
            
            transform.position = new Vector3(2*x,2*y,2*z);
            transform.rotation = Quaternion.Euler (new Vector3 (a, 0, c));

            item.transform.position=transform.position;
            item.transform.rotation=transform.rotation;

            Des=Instantiate(feed,transform.position,transform.rotation);  // 생성 이펙트 복제해서 가져오기
            Destroy(Des,5);
            //===================================================================

                tai[inq+1] = Instantiate(tai[inq],new Vector3(0,0,0),Quaternion.identity);    // 꼬리 새로 만들기
                tai[inq+1].GetComponent<tail>().followDelay += t;
                inq+=1;
            

        } //태그-player

        
        

        




    }

}

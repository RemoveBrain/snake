using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class item : MonoBehaviour
{
    public int code=0;
    public bool skill=false;

    /*
     * 아이템 목록
     * 1. 점프
     * 2. 베리어
     * 3. 포탈
     * 4. 조각모음기능 (체력확장)
     * 5. 시간 추가
     * 6. 체력 회복
     * ==
     * 1. 꼬리 늘리기
     * 2. 시간 감소
     * 3. 속도 대폭 증가   ( sw값 1정도로 )
     * 4. 벽 생성
     */
    public GameObject obj;  // 플레이어 큐브
    public GameObject obj2;  // 카메라.
    public GameObject eat;
    private GameObject Des;
    public GameObject feed;
    public GameObject wall;
    public Text objText;
    public Button btn;
    public Slider Timer;
    public ParticleSystem bar;
    // Start is called before the first frame update
    void Start()
    {
        btn.interactable = false;
    }



    // Update is called once per frame
    void Update()
    {
        if(Timer.value==0) // 타임오버시 게임 종료
        {
            automove call = GameObject.Find("moving").GetComponent<automove>();
            call.gameoverPanel.SetActive(true);
            call.start=0;
            call.Motion=true;
            call.animator.SetBool("Lose",true);
        }
    }
    public void UseSkill()
    {
        if(skill) //아이템 사용 (버튼으로)
        {
            skill=false;

            automove call = GameObject.Find("moving").GetComponent<automove>();
            eatcube call2 = GameObject.Find("feed").GetComponent<eatcube>();
            if(call.Motion==false)
            {
                switch(code)
                {
                    case 1:jump();objText.text="NoSkill";btn.interactable = false;break;
                    case 2:barrier();objText.text="NoSkill";btn.interactable = false;break;
                    case 3:ready_portal();objText.text="NoSkill";btn.interactable = false;break;
                    case 4:if(call2.inq>4){HpCap();objText.text="NoSkill";btn.interactable = false;} break;
                }
            }

        }
    }
    void OnTriggerEnter(Collider other)   // 아이템 상자 먹었을때. 1~4는 사용형 그이후는 즉시발동
    {
        if(other.tag=="Player")
        {
            if(!skill)
            { 
                code = Random.Range(1,10);
                switch(code)
                {
                    case 1:objText.text="jump";skill=true;btn.interactable = true;break; 
                    case 2:objText.text="barrier";skill=true;btn.interactable = true;break;
                    case 3:objText.text="portal";skill=true;btn.interactable = true;break;
                    case 4:objText.text="HpCap";skill=true;btn.interactable = true;break;
                    case 5:objText.text="HpRecovery";HpRecovery();break;
                    case 6:objText.text="TimePlus";TimePlus();break;
                    case 7:objText.text="TimeMinus";TimeMinus();break;
                    case 8:objText.text="SpeedUp";SpeedUp();break;
                    case 9:objText.text="TailPlus";TailPlus();break;
                    case 10:objText.text="Wall";Wall();break;
                }
            }
            //=================== 아이템 큐브 먹은후 아이템큐브 멀리 보내고 피드큐브 다시 복귀 =============
            float x=0,y=0,z=0; // 포지션
            float a=0,c=0; // 로테이션
            int r=1;
            r=Random.Range(1,6);  // 랜덤 면 지정
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
            Destroy(Des,2);

            feed.transform.position = new Vector3(2*x,2*y,2*z); 
            feed.transform.rotation = Quaternion.Euler (new Vector3 (a, 0, c));

            transform.position = new Vector3(-10,-10,-10);
            //===============================================================================================
        }
    }

    void jump()
    {
        automove call = GameObject.Find("moving").GetComponent<automove>();

        call.animator.SetTrigger("S_jump");
        call.Invoke("ND",0.35f);
        call.Motion = true;
        call.NoDamage = true;
    }
    void barrier()
    {
        bar.Play();
        automove call = GameObject.Find("moving").GetComponent<automove>();

        call.NoDamage = true;
        call.Invoke("ND",1);
    }
    void portal()
    {
        float a,b,c;
        float a1=0,b1=0,c1=0;
        float a2=0,b2=0,c2=0;
        a2=obj2.transform.position.x*-1;
        b2=obj2.transform.position.y*-1;
        c2=obj2.transform.position.z*-1;

        obj2.transform.Rotate(Vector3.right*180.0f);
        obj2.transform.position = new Vector3(a2,b2,c2);
        
        a=obj.transform.position.x;
        b=obj.transform.position.y;
        c=obj.transform.position.z;
        a=a*a;
        b=b*b;
        c=c*c;
        if(a>b && a>c)
        {
            a1=obj.transform.position.x * -1.0f;
            b1=0f;
            c1=0f;
        }
        if(b>a && b>c)
        {
            a1=0f;
            b1=obj.transform.position.y * -1.0f;
            c1=0f;
        }
        if(c>b && c>a)
        {
            a1=0f;
            b1=0f;
            c1=obj.transform.position.z * -1.0f;
        }
        obj.transform.Rotate(Vector3.right*180.0f);
        obj.transform.position = new Vector3(a1,b1,c1);
    }
    void ready_portal()
    {

        automove call = GameObject.Find("moving").GetComponent<automove>();

        call.start=0;
        call.sw=0f;
        call.animator.SetFloat("speed",0.0f);
        call.animator.SetTrigger("Reflesh");
        call.Motion = true;
        call.NoDamage = true;

            
        Invoke("portal",1);
    }
    void HpCap()
    {
        automove call = GameObject.Find("moving").GetComponent<automove>();
        call.HPMAX+=1;
        
        call.empty[call.HPMAX-1].SetActive(true);

        eatcube call2 = GameObject.Find("feed").GetComponent<eatcube>();
        for(int i=0;i<4;i++)
        {
            Destroy(call2.tai[call2.inq-i]);
        }
        call2.inq-=4;
    }
    void HpRecovery()
    {
        automove call = GameObject.Find("moving").GetComponent<automove>();

        call.HP+=1;
        if(call.HP>call.HPMAX)call.HP=call.HPMAX;
        for(int i=0;i<call.HP;i++)  // HP  이미지 교체
        {
             call.fill[i].SetActive(true);
             call.empty[i].SetActive(false);
        }
    }
    void TimePlus()
    {
        Timer.value +=18; 
    }
    void TimeMinus()
    {
        Timer.value -=18;
    }
    void SpeedUp()
    {
        automove call = GameObject.Find("moving").GetComponent<automove>();
        call.sw=0.005f;
        call.animator.SetFloat("speed",1.0f);
        call.Invoke("NS",5);
    }
    void TailPlus()
    {
        eatcube call = GameObject.Find("feed").GetComponent<eatcube>();
        
        call.tai[call.inq+1] = Instantiate(call.tai[call.inq],new Vector3(0,0,0),Quaternion.identity);    // 꼬리 새로 만들기
        call.tai[call.inq+1].GetComponent<tail>().followDelay += call.t;
        call.inq+=1;

    }
    public void Wall()
    {
        float x=0,y=0,z=0; // 포지션
        float a=0,c=0; // 로테이션
        int r=1;
        wallmove call = GameObject.Find("wall").GetComponent<wallmove>();
        
        call.Random_ac();


        r=Random.Range(1,6);  // 랜덤 면 지정
        switch (r) { 
                    case 1:
            //노란면
            x=Random.Range(-0.455f,0.443f);
            y=1.06f;
            z=Random.Range(-0.455f,0.461f);
                    c=0;
                    break;
                    case 2:
            //초록면
            x=-1.06f;
            y=Random.Range(0.443f,-0.414f);
            z=Random.Range(0.442f,-0.44f);
                    c=90;
                    break;
                    case 3:
            //빨간면
            x=Random.Range(-0.452f,0.436f);
            y=Random.Range(0.462f,-0.448f);
            z=-1.06f;
                    a=270;
                    break;
                    case 4:
            //파란면
            x=1.06f;
            y=Random.Range(0.458f,-0.444f);
            z=Random.Range(-0.459f,0.445f);
                    c=270;
                    break;
                    case 5:
            //하얀면
            x=Random.Range(0.455f,-0.455f);
            y=Random.Range(0.456f,-0.434f);
            z=1.06f;
                    a=90;
                    break;
                    case 6:
            //보라면
            x=Random.Range(0.447f,-0.445f);
            y=-1.06f;
            z=Random.Range(0.447f,-0.429f);
                    c=180;
                    break;
            }

        
        wall.transform.position = new Vector3(x,y,z); 
        wall.transform.rotation = Quaternion.Euler (new Vector3 (a, 0, c));

           
    }

    public void Test()
    {
        code=(code%11)+1;
                switch(code)
                {
                    case 1:objText.text="jump";skill=true;btn.interactable = true;break; 
                    case 2:objText.text="barrier";skill=true;btn.interactable = true;break;
                    case 3:objText.text="portal";skill=true;btn.interactable = true;break;
                    case 4:objText.text="HpCap";skill=true;btn.interactable = true;break;
                    case 5:objText.text="HpRecovery";HpRecovery();break;
                    case 6:objText.text="TimePlus";TimePlus();break;
                    case 7:objText.text="TimeMinus";TimeMinus();break;
                    case 8:objText.text="SpeedUp";SpeedUp();break;
                    case 9:objText.text="TailPlus";TailPlus();break;
                    case 10:objText.text="Wall";Wall();break;
                }
    }
}

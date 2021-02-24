using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;


public class automove : MonoBehaviour {

    public int HP=4;//초기값 4. 0되면 end
    public float start=0; // 출발 유무
    public float sw=0f; // 이동 속도 추가값
    private float x=0,y=0,z=0;
    public Animator animator;
    public CharacterController pcController;
    public bool NoDamage = false;  //무적
    //public bool J_NoDamage = false; //점프 무적
    public bool Motion = false; // 현재 모션중
    //public bool Jump = false; 
    public GameObject obj;
    public GameObject obj2;
    public GameObject gameoverPanel;
    public GameObject[] fill;
    public GameObject[] empty;
    private Vector3 Pos;
    public ParticleSystem crash;
    
    void Start()
    {
        animator.SetFloat("speed",0.0f);
        gameoverPanel.SetActive(false);
        for(int i=0;i<4;i++)fill[i].SetActive(true);
        for(int i=0;i<4;i++)empty[i].SetActive(false);

    }
	void Update () {
        if(Input.GetKeyDown("up") && !Motion)
        {
            if(start==0)
            { 
                animator.SetFloat("speed",0.5f);
                start=1;
            }
            else if(start==1)
            {
                animator.SetFloat("speed",1.0f);
                sw=0.3f;
            }
        }
       /*else if(Input.GetKeyDown(KeyCode.Space) && !Motion) //사용 점프 
        {
            animator.SetTrigger("S_jump");
            Invoke("ND",0.35f);
            Motion = true;
            NoDamage = true;
        }*/
        else if(Input.GetKeyDown("right") && !Motion && start==1)
        {
            transform.Rotate(Vector3.up*90.0f);
        }
        else if(Input.GetKeyDown("left") && !Motion && start==1)
        {
            transform.Rotate(Vector3.down*90.0f);
        }

        transform.Translate(new Vector3(0,0,start) * Time.deltaTime * (0.7f+sw));  
	}
    
    void OnTriggerExit(Collider other) {  //낭떠러지 점프
        
        if(other.tag == "Field")
        {
            start=0;
            sw=0f;
            animator.SetFloat("speed",0.0f);
            animator.SetTrigger("Jump");
            Motion = true;
            NoDamage = true;
            InvokeRepeating("rot",0f,0.01f);
            Invoke("rotcancel",0.9f);
        }
        x%=360;
        y%=360;
        z%=360;
    }
    
    void OnTriggerEnter(Collider other)  //넘어짐
    {
        if(other.tag=="tail" && !NoDamage)
        {
            HP-=1;
            crash.Play();
            if(HP<=0)  // 게임 끝
            {
                gameoverPanel.SetActive(true);
                start=0;
                Motion=true;
                animator.SetBool("Lose",true);

            }
            else 
            { 
                start=0;
                sw=0f;
                Motion = true;
                NoDamage = true;
                animator.SetFloat("speed",0.0f);
                animator.SetTrigger("Down");
            }
            
            for(int i=3;i>=HP;i--)  // HP 줄어들시 이미지 교체
            {
                fill[i].SetActive(false);
                empty[i].SetActive(true);
            }
        }
    }
    void rot() // 낭떠러지 회전
    {
        transform.Rotate(Vector3.right*1.0f);
    }
    void rotcancel() // 회전 캔슬.
    {
        float a=0,b=0,c=0;

        CancelInvoke("rot");
        obj.transform.position = transform.position;     // 카메라 위치 복제
        obj.transform.rotation = transform.rotation;

        obj2.transform.localPosition = new Vector3 (0, 2, 0); // 모델 위치 조정
        obj2.transform.rotation = transform.rotation;
        
        obj.transform.Translate (0, 2.7f, -1.0f);  // 카메라 위치 조정 
        obj.transform.Rotate(60,0,0);
        a= obj.transform.position.x;
        b= obj.transform.position.y;
        c= obj.transform.position.z;
        a= a*a;
        b= b*b;
        c= c*c;
        if(a<b && a<c)
        {
            obj.transform.localPosition = new Vector3 (0, obj.transform.position.y, obj.transform.position.z);
        }
        else if (b<a && b<c)
        {
            obj.transform.localPosition = new Vector3 (obj.transform.position.x, 0, obj.transform.position.z);
        }
        else if(c<a && c<b)
        {
            obj.transform.localPosition = new Vector3 (obj.transform.position.x, obj.transform.position.y, 0);
        }
    }
    void ND()  // 무적 해제
    {
        NoDamage = false;
       // Motion = false;
    }
}

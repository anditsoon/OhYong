using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // 걷기 변수
    public float walkSpeed = 7f;
    // 달리기 변수
    public float runSpeed = 20f;
    public bool isRunning = false;
    // 이동 속도 변수
    private float moveSpeed;

    // 캐릭터 콘트롤러 변수
    CharacterController cc;

    // 중력 변수
    float gravity = -80f;
    // 수직 속력 변수
    float yVelocity = 0;
    // 점프력 변수
    public float jumpPower = 1f;
    // 점프 상태 변수
    public bool isJumping = false; 

    // 모양의 게임 오브젝트
    public GameObject model;

    // 애니메이터
    Animator anim;

    private void Start()
    {
        // 캐릭터 콘트롤러 컴포넌트 받아오기
        cc = GetComponent<CharacterController>();

        moveSpeed = walkSpeed;

        anim = transform.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // w, a, s, d 키를 입력하면 캐릭터를 그 방향으로 이동시키고 싶다
        // [spacebar] 키를 누르면 캐릭터를 수직으로 점프시키고 싶다
        // [shift] 키를 누르면 캐릭터가 달리게 하고 싶다

        // 1. 사용자의 입력을 받는다
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // 2. 이동 방향을 설정한다
        Vector3 dir = new Vector3(h, 0, v);
        dir = dir.normalized;

        // 2-1. 메인 카메라를 기준으로 방향을 변환한다
        dir = Camera.main.transform.TransformDirection(dir);


        #region 점프

        // 2-2. 만일, 점프 중이었고, 다시 바닥에 착지했다면
        if (cc.collisionFlags == CollisionFlags.Below)
        {
            // 만일, 점프 중이었다면,
            if (isJumping)
            {
                // 점프 전 상태로 초기호한다
                isJumping = false;
                // 캐릭터 수직 속도를 0으로 만든다
                yVelocity = 0;
               
            }
        }

        // 2-3. 만일, 키보드 [spacebar] 키를 입력했고, 점프를 하지 않은 상태라면,
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            // 캐릭터 수직 속도에 점프력을 적용하고 점프 상태로 변경한다
            yVelocity = jumpPower;
            isJumping = true;
        }

        // 2-3. 캐릭터 수직 속도에 중력 값을 적용한다
        yVelocity += gravity * Time.deltaTime;
        dir.y = yVelocity;

        Vector3 modelDir = dir;
        modelDir.y = 0;

        #endregion


        #region 달리기
        // 4-1. 만일, 러닝 중이었고, shift 키를 뗐다면 (다시 걷기로 돌아온다면)
        if (isRunning && Input.GetButtonUp("Fire3"))
        {
            // 러닝 전 상태로 초기화한다
            isRunning = false;
            // 캐릭터 속도를 걷기 속도로 되돌린다
            moveSpeed = walkSpeed;
            anim.SetBool("isSprint", false);
        }

        // 4-2. 만일, 키보드 [shift] 키를 입력했고, 러닝을 하지 않은 상태라면,
        if (Input.GetButtonDown("Fire3") && !isRunning)
        {
            // 캐릭터 수직 속도에 점프력을 적용하고 러닝 상태로 변경한다
            moveSpeed = runSpeed;
            isRunning = true;
            anim.SetBool("isSprint", true);

        }

        #endregion



        //3. 이동 속도에 맞춰 이동한다
        // p = p0 + vt
        cc.Move(dir * moveSpeed * Time.deltaTime);

    
        // dir 의 크기가 0보다 크면 (움직일때만)
        if (modelDir.magnitude > 0)
        {
            // 움직이는 방향을 모양의 앞방향으로 설정
            model.transform.forward = modelDir;
            anim.SetBool("isMove", true);
        }
        else
        {
            anim.SetBool("isMove", false);
        }
    }
}

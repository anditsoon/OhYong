using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    // 이동 속도 변수
    public float moveSpeed = 7f;

    // 캐릭터 콘트롤러 변수
    CharacterController cc;

    // 중력 변수
    float gravity = -20f;

    // 수직 속력 변수
    float yVelocity = 0;

    // 점프력 변수
    public float jumpPower = 10f;

    // 점프 상태 변수
    //public bool isJumping = 

    // 모양의 게임오브젝트
    public GameObject model;

    private void Start()
    {
        // 캐릭터 콘트롤러 컴포넌트 받아오기
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // w, a, s, d 키를 입력하면 캐릭터를 그 방향으로 이동시키고 싶다
        // [spacebar] 키를 누르면 캐릭터를 수직으로 점프시키고 싶다

        // 1. 사용자의 입력을 받는다
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // 2. 이동 방향을 설정한다
        Vector3 dir = new Vector3(h, 0, v);
        dir = dir.normalized;

        // 2-1. 메인 카메라를 기준으로 방향을 변환한다
        dir = Camera.main.transform.TransformDirection(dir);
       

        //3. 이동 속도에 맞춰 이동한다
        // p = p0 + vt
        cc.Move(dir * moveSpeed * Time.deltaTime);

        // dir 의 크기가 0보다 크면 (움직일떄만)
        if(dir.magnitude > 0)
        {
            // 움직이는 방향을 모양의 앞방향으로 설정
            model.transform.forward = dir;

        }
    }
}

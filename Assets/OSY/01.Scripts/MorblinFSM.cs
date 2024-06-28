using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class MorblinFSM : MonoBehaviour
{

    //FSM State
    enum MorblinState
    {
        Idle, Move, BackJump, Return, Attack1, Attack2, Damaged, Die
    }
    // 에너미 상태 변수
    MorblinState m_State;

    // 활동 반경 25
    public float activeDistance = 25;
    // 플레이어 발견 범위
    public float findDistance = 30f;
    // 이동 가능 범위
    public float moveDistance = 20f;
    // 공격 가능 범위
    public float attackDistance = 10f;
    // BackJump 거리
    public float backJumpDistance = 5f;

    // 이동 속도
    public float moveSpeed = 5;

    // 스폰 Transform
    Vector3 originPos;
    // 플레이어 플레이어 오브젝트
    public GameObject player;
    // 플레이어와의 거리
    float playerDistance;
    // 원래 위치와의 거리
    float originDistance;

    // Attack1로 갈지 Attack2로 갈지 랜덤함수
    int attackRandom = Random.Range(0, 2);



    void Start()
    {
        // 초반 상태는 아이들 상태
        m_State = MorblinState.Idle;

        // 원래 위치를 시작지점으로 지정한다.
        originPos = transform.position;

    }
    void Update()
    {
        switch (m_State)
        {
            case MorblinState.Idle:
                Idle();
                break;
            case MorblinState.Move:
                Move();
                break;
            case MorblinState.BackJump:
                BackJump();
                break;
            case MorblinState.Return:
                Return();
                break;
            case MorblinState.Attack1:
                //Attack1();
                break;
            case MorblinState.Attack2:
                //Attack2();
                break;
            case MorblinState.Damaged:
                //Damaged();
                break;
            case MorblinState.Die:
                //Die();
                break;
        }

        // 플레이어와의 거리 계속 계산
        playerDistance = Vector3.Distance(transform.position, player.transform.position);
        // 원래위치와 거리 계속 계산
        originDistance = Vector3.Distance(transform.position, originPos);

       


    }

    void Idle()
    {
        print("Idle");
        if (originDistance>activeDistance)
        {
            //리턴해
            m_State = MorblinState.Return;
        }
        // 그렇지 않다면
        else
        {
            if(playerDistance > findDistance)
            {
            
            }
            else if(playerDistance > moveDistance)
            {
                // 바라본다
                LookPlayer();
                // 느낌표 띄운다
                        
            }
            else if(playerDistance > attackDistance)
            {
                m_State = MorblinState.Move;
            }
            else if(playerDistance > backJumpDistance)
            {

                attackRandom = Random.Range(0, 2);

                // 만약 attackRandom 이 0이면 Attack1로 간다
                if (attackRandom == 0)
                {
                    //m_State = MorblinState.Attack1;

                    // 바로 공격할 수 있게 미리 시간을 돌려놓는다
                    //anim.SetTrigger("MoveToAttack1Delay");
                }
                else
                {
                   // m_State = MorblinState.Attack2;
                    // 바로 공격할 수 있게 미리 시간을 돌려놓는다
                    //anim.SetTrigger("MoveToAttack2Delay");
                }
            }
            else
            {
                // 백점프해라
            }
        }
    }
    void Move()
    {

        // 바라본다
        LookPlayer();

        // 만약 움직 거리보다 작으면서 공격거리보다 크다면
        if (playerDistance <= moveDistance && playerDistance > attackDistance)
        {
            Vector3 dir = player.transform.position - transform.position;
            dir.Normalize();

            //플레이어쪽으로 움직여
            transform.position += dir * moveSpeed * Time.deltaTime; 
        }
        // 그렇지 않다면
        else
        {
            // 아이들로 돌아가
            m_State = MorblinState.Idle;
        }

    }
    void BackJump()
    {

    }
    void Return()
    {
        if(activeDistance>1)
        {
            Vector3 dir = originPos - transform.position;
            dir.Normalize();
            transform.position += dir * 10 * Time.deltaTime;
            print("return");
        }
        else
        {
            m_State = MorblinState.Idle;
        }
    }

    void LookPlayer()
    {
        // 바라본다
        Vector3 dir = player.transform.position - transform.position;
        transform.forward = dir;

    }

}

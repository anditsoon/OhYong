using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyFSM : MonoBehaviour
{
    //애니메이터 변수
    Animator anim;


    // FSM State
    enum EnemyState
    {
        Idle, Move, Attack1, Attack2, BackJump, Return, Damaged, Die
    }

    // 에너미 상태 변수
    EnemyState m_State;

    // 플레이어 발견 범위
    public float findDistance = 30f;
    // 이동 가능 범위
    public float moveDistance = 20f;
    // 공격 가능 범위
    public float attackDistance = 10f;
    // BackJump 거리
    public float backJumpDistance = 5f;
   

    // 플레이어 트랜스폼
    public Transform playerTransform;


    // 이동속도
    public float moveSpeed = 3f;

    // 캐릭터 컨트롤러 컴포넌트
    CharacterController cc;

    // Attack1로 갈지 Attack2로 갈지 랜덤함수
    int attackRandom = Random.Range(0, 2);


    // 누적시간
    float currentTime = 0;

    // 초기 위치 저장용 변수
    Vector3 originPos;
    Quaternion originRot;


    // 공격딜레이 시간
    float attackDelay = 2f;

    // 에너미의 체력
    public int hp = 15;

    bool nowreturn = false;


    void Start()
    {
        // 초반 상태는 아이들 상태
        m_State = EnemyState.Idle;

        // 플레이어의 트랜스폼 컴포넌트 가져오기
        playerTransform = GameObject.Find("Player").transform;

        // 자신의 초기 위치와 외전값
        originPos = transform.position;
        originRot = transform.rotation;

        //캐릭터 컴포넌트 받아오기
        cc = GetComponent<CharacterController>();

        // 자식 오브젝트로부터 애니메이터 변수 받아오기
        anim = transform.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // 현재 상태를 체크해 해당 상태별로 정해진 기능을 수행하게 하고 싶다.
        switch (m_State)
        {
            case EnemyState.Idle:
                Idle();
                break;
            case EnemyState.Move:
                Move();
                break;
            case EnemyState.Attack1:
                Attack1();
                break;
            case EnemyState.Attack2:
                Attakc2();
                break;
            case EnemyState.BackJump:
                BackJump();
                break;
            case EnemyState.Return:
                Return();
                break;
            case EnemyState.Damaged:
                //Damaged();
                break;
            case EnemyState.Die:
                //Die();
                break;
        }

        print(m_State);

    }

    void Idle()
    {
        // 만약 플레이어와의 거리가 액션 시작 범위 이내라면 
        if (Vector3.Distance(transform.position, playerTransform.position) > findDistance)
        {
            m_State = EnemyState.Return;
        }
        else if(Vector3.Distance(transform.position,playerTransform.position)> attackDistance)
        {
            //Move로 바꾼다
            m_State = EnemyState.Move;
            anim.SetTrigger("IdleToMove");
        }
        else
        {
            //BackJump로 바꾼다.
            m_State = EnemyState.BackJump;
        }
    }
    void Move()
    {
        // 만약 moveDistance보다 크면 아이들로 간다
        if (Vector3.Distance(transform.position, playerTransform.position) >moveDistance)
        {
            m_State = EnemyState.Idle;
        }
        // moveDistance보다 작고 attackDistance보다 크면 움직여
        else if (Vector3.Distance(transform.position, playerTransform.position) > attackDistance)
        {
            Vector3 dir = (playerTransform.position - transform.position).normalized;
            // 캐릭터 컨트롤러 이용해서 이동하기
             cc.Move(dir * moveSpeed * Time.deltaTime);

            transform.forward = dir;
        }
        else if(Vector3.Distance(transform.position,playerTransform.position)>backJumpDistance)
        {
            // 다음 공격을 위해 랜덤 설정을 다시 초기화
            attackRandom = Random.Range(0, 2);

            // 만약 attackRandom 이 0이면 Attack1로 간다
            if (attackRandom == 0)
            {
                m_State = EnemyState.Attack1;

                // 바로 공격할 수 있게 미리 시간을 돌려놓는다
                currentTime = attackDelay;
                anim.SetTrigger("MoveToAttack1Delay");
            }
            else
            {
                m_State = EnemyState.Attack2;
                // 바로 공격할 수 있게 미리 시간을 돌려놓는다
                currentTime = attackDelay;
                anim.SetTrigger("MoveToAttack2Delay");
            }
        }
        else
        {
            m_State = EnemyState.BackJump;
            anim.SetTrigger("MoveToBackJump");
        }


    }
    void Attack1()
    {
        // 만약 거리가 어택거리보다 크다면 Move로
        if (Vector3.Distance(transform.position, playerTransform.position) > attackDistance)
        {
            m_State = EnemyState.Move;
            // 이동 애니메이션 플레이
            anim.SetTrigger("AttackToMove");
            currentTime = 0;
        }
        // 그렇지 않고 백점프 거리보다 크면 공격해
        else if (Vector3.Distance(transform.position, playerTransform.position) > backJumpDistance)
        {
            // 일정 시간마다 플레이어를 공격한다.
            currentTime += Time.deltaTime;
            if (currentTime > attackDelay)
            {
                // 공격을 한다
                anim.SetTrigger("StartAttack1");
                // 시간 초기화
                currentTime = 0;
            }
        }
        // 백점프 거리보다 작다면 백점프로 상태를 전환
        else
        {
            m_State = EnemyState.BackJump;
            currentTime = 0;
            anim.SetTrigger("Attack1ToBackJump");
        }

    }
    void Attakc2()
    {
        // 만약 거리가 어택거리보다 크다면 Move로
        if (Vector3.Distance(transform.position, playerTransform.position) > attackDistance)
        {
            m_State = EnemyState.Move;
            currentTime = 0;
            anim.SetTrigger("Attack2ToMove");
        }
        // 그렇지 않고 백점프 거리보다 크면 공격해
        else if (Vector3.Distance(transform.position, playerTransform.position) > backJumpDistance)
        {
            // 일정 시간마다 플레이어를 공격한다.
            currentTime += Time.deltaTime;
            if (currentTime > attackDelay)
            {
                // 공격을 한다
                // 공격을 한다
                anim.SetTrigger("StartAttack2");
                // 시간 초기화
                currentTime = 0;
            }
        }
        // 백점프 거리보다 작다면 백점프로 상태를 전환
        else
        {
            m_State = EnemyState.BackJump;
            currentTime = 0;
            anim.SetTrigger("Attack2ToBackJump");
        }
    }
    void BackJump()
    {
        StartCoroutine(BackJumpProcess());
        

    }
    void Return()
    {
        // 만약 초기 위치에서의 거리가 0.1f 이사이면 초기 위치쪽으로 이동한다.
        // 그렇지 않다면 자신의 위치를 초기 위치로 조정하고 현재 상태를 대기로 전환한다.
        if (Vector3.Distance(transform.position, originPos) > 0.1f)
        {
            Vector3 dir = (originPos - transform.position).normalized;
            cc.Move(dir * moveSpeed * Time.deltaTime);

            //방향을 복귀 지점으로 전환한다.
            transform.forward = dir;
            
            nowreturn = true;
            
        }
        else
        {
            transform.position = originPos;
            // HP 회복하고
            // hp = maxHp;
            // 상태를 전환한다.
            m_State = EnemyState.Idle;
            nowreturn = false;

            // 대기 애니메이션으로 전환하는 트랜지션을 호출한다.
            anim.SetTrigger("MoveToIdle");
        }
    }
    void Damaged()
    {
        // 피격 상태를 처리하기위한 코루틴을 실핸한다.
        StartCoroutine(DamageProcess());
    }
    void Die()
    {
        // 진행중인 피격 코루틴을 중지한다.
        StopAllCoroutines();

        // 죽음상태를 처리하기 위한 코루틴을 실행한다.
        StartCoroutine(DieProcess());
    }
    // 백점프 처리용 코루틴 함수
    IEnumerator BackJumpProcess()
    {
        // Y는 고정한다.

        Vector3 startPosition = transform.position;
        Vector3 dir = (transform.position - playerTransform.position).normalized;
        Vector3 targetPosition = startPosition + dir * backJumpDistance;

        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            Vector3 move = dir * moveSpeed * Time.deltaTime;
            cc.Move(move);

            // 목표 위치를 초과하지 않도록 현재 위치를 업데이트
            if (Vector3.Distance(transform.position, targetPosition) <= 0.1f ||
                Vector3.Dot(dir, (targetPosition - transform.position)) < 0)
            {
                break;
            }

            yield return null; // 한 프레임 대기
        }

        // 정확한 목표 위치로 설정
        transform.position = targetPosition;

        // 현재 상태를 백점프로 전환한다.
        m_State = EnemyState.Move;
        anim.SetTrigger("BackJumpToMove");
    }
    // 데미지 처리용 코루틴 함수
    IEnumerator DamageProcess()
    {
        yield return new WaitForSeconds(0.5f);

        // 현재 상태를 이동 상태로 전환한다.
        m_State = EnemyState.Move;
    }
    IEnumerator DieProcess()
    {
        // 이동하지 않게 만들고
        cc.enabled = false;
        // 2초 기다렸다가
        yield return new WaitForSeconds(2);
        // 파괴된다
        Destroy(gameObject);
    }
    // 데미지 실행 함수
    public void HitEnemy(int hitPower)
    {
        // 만약 이미 피격 상태거나 사망상태거나 복귀상태라면 아무런 처리를 하지 않는다
        if (m_State == EnemyState.Damaged || m_State == EnemyState.Die || m_State == EnemyState.Return)
        {
            return;
        }
        // 플레이어가 준 데미지 만큼 HP를 줄인다.
        hp -= hitPower;

        // 만약 체력이 0보다 크다면 피격상태로 전환한다.
        if (hp > 0)
        {
            m_State = EnemyState.Damaged;
            Damaged();
        }
        // 그렇지 않다면 죽음상태로 전환한다.
        else
        {
            m_State = EnemyState.Die;
            Die();
        }
    }

}

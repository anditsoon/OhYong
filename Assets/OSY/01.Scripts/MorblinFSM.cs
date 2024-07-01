using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorblinFSM : MonoBehaviour
{
    enum MorblinState
    {
        Idle, Move, BackJump, Return, Attack1, Attack2, Damaged, Die
    }

    MorblinState m_State;

    #region 거리
    public float findStart = 50;
    public float moveStart = 20;
    public float attackStart = 10;
    public float backStart = 5;
    #endregion

    #region 가져오기
    public GameObject player;
    public Vector3 originPos;
    float distanceToPlayer;
    Animator anim;
    #endregion

    int random = 0;

    void Start()
    {
        m_State = MorblinState.Idle;
        originPos = this.transform.position;
        player = GameObject.Find("Player");
        anim = transform.GetComponentInChildren<Animator>();
    }

    void Update()
    {
        Vector3 thisToPlayer = player.transform.position - transform.position;
        distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
        transform.forward = thisToPlayer;

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
                Attack1();
                break;
            case MorblinState.Attack2:
                Attack2();
                break;
            case MorblinState.Damaged:
                Damaged();
                break;
            case MorblinState.Die:
                Die();
                break;
        }
    }

    void Idle()
    {
        if (distanceToPlayer > findStart)
        {
            print("아무것도안해");
        }
        else if (distanceToPlayer > moveStart)
        {
            print("바라본다");
        }
        else if (distanceToPlayer > attackStart)
        {
            m_State = MorblinState.Move;
            anim.SetTrigger("IdleToMove");
        }
        else if (distanceToPlayer > backStart)
        {
            random = Random.Range(0, 2);
            if (random > 0)
            {
                m_State = MorblinState.Attack1;
            }
            else
            {
                m_State = MorblinState.Attack2;
            }
        }
        else if (distanceToPlayer <= backStart && distanceToPlayer > 0)
        {
            m_State = MorblinState.BackJump;
        }
    }

    void Move()
    {
        print("이동 중");
        if (distanceToPlayer <= moveStart && distanceToPlayer > attackStart)
        {
            // 플레이어를 향해 이동하는 로직 구현
            Vector3 moveDirection = (player.transform.position - transform.position).normalized;
            transform.position += moveDirection * Time.deltaTime * 3; // 3은 이동 속도, 필요에 따라 조정 가능
        }
        else
        {
            m_State = MorblinState.Idle;
        }
    }

    void BackJump()
    {
        print("뒤로 점프");
        if (distanceToPlayer <= backStart)
        {
            StartCoroutine(BackJumpCoroutine());
        }
    }

    void Return()
    {
        print("원래 위치로 돌아감");
        if (Vector3.Distance(transform.position, originPos) > 0.1f)
        {
            Vector3 returnDirection = (originPos - transform.position).normalized;
            transform.position += returnDirection * Time.deltaTime * 3; // 3은 이동 속도, 필요에 따라 조정 가능
        }
        else
        {
            m_State = MorblinState.Idle;
        }
    }

    void Attack1()
    {
        print("공격1");
        if (distanceToPlayer <= attackStart && distanceToPlayer > backStart)
        {
            // 애니메이션의 길이를 가져와서 넣는다.
            float animDuration = 2.0f;
            // 코루틴 실행 2초후 아이들로
            StartCoroutine(DelayCoroutine(animDuration));
        }
        else
        {
            m_State = MorblinState.Idle;
        }
    }

    void Attack2()
    {
        print("공격2");
        if (distanceToPlayer <= attackStart && distanceToPlayer > backStart)
        {
            // 애니메이션의 길이를 가져와서 넣는다.
            float animDuration= 3.0f;
            // 코루틴 실행 3초후 아이들로
            StartCoroutine(DelayCoroutine(animDuration));
            
        }
        else
        {
            m_State = MorblinState.Idle;
        }
    }

    void Damaged()
    {
        // 애니메이션의 길이를 가져와서 넣는다.
        float animDuration = 1.0f;
        // 코루틴 실행 1초후 아이들로
        StartCoroutine(DelayCoroutine(animDuration));
    }

    void Die()
    {
        print("사망");
        // 사망 로직 구현
    }

    #region 코루틴함수
    IEnumerator BackJumpCoroutine()
    {
        Vector3 backDirection = (transform.position - player.transform.position).normalized;
        Vector3 startPosition = transform.position;
        Vector3 endPosition = startPosition + backDirection * 5.0f; // 5.0f는 이동 거리, 필요에 따라 조정 가능

        float duration = 1.0f; // 5초 동안 뒤로 이동
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        m_State = MorblinState.Idle;
    }
    IEnumerator DelayCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        m_State = MorblinState.Idle;
    }

  

    #endregion
}
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyFSM : MonoBehaviour
{

    // FSM State
    enum EnemyState
    {
        Idle, Move, Attack1, Attack2, BackJump, Return, Damaged, Die
    }

    // ���ʹ� ���� ����
    EnemyState m_State;

    // �÷��̾� �߰� ����
    public float findDistance = 15f;
    // �̵� ���� ����
    public float moveDistance = 10f;
    // ���� ���� ����
    public float attackDistance = 5f;

    // �÷��̾� Ʈ������
    public Transform playerTransform;


    // �̵��ӵ�
    public float moveSpeed = 3f;

    // ĳ���� ��Ʈ�ѷ� ������Ʈ
    CharacterController cc;

    // Attack1�� ���� Attack2�� ���� �����Լ�
    int attackRandom = Random.Range(0, 2);

    // BackJump �Ÿ�
    public float backJumpDistance = 2f;

    // �����ð�
    float currentTime = 0;

    // �ʱ� ��ġ ����� ����
    Vector3 originPos;


    // ���ݵ����� �ð�
    float attackDelay = 2f;

    // ���ʹ��� ü��
    public int hp = 15;

    bool nowreturn = false;


    void Start()
    {
        // �ʹ� ���´� ���̵� ����
        m_State = EnemyState.Idle;

        // �÷��̾��� Ʈ������ ������Ʈ ��������
        playerTransform = GameObject.Find("Player").transform;

        originPos = transform.position;

        //ĳ���� ������Ʈ �޾ƿ���
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // ���� ���¸� üũ�� �ش� ���º��� ������ ����� �����ϰ� �ϰ� �ʹ�.
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

        if(nowreturn == false)
        {
            transform.forward = playerTransform.position - transform.position;
        }
        else if(nowreturn == true)
        {
            transform.forward = originPos-transform.position;
        }
    }

    void Idle()
    {
        // ���� �÷��̾���� �Ÿ��� �׼� ���� ���� �̳���� 
        if (Vector3.Distance(transform.position, playerTransform.position) > findDistance)
        {
            //Move�� �ٲ۴�
            m_State = EnemyState.Return;
        }
        else
        {
            m_State = EnemyState.Move;
        }
    }
    void Move()
    {
        // ���� moveDistance���� ũ�� ���̵�� ����
        if (Vector3.Distance(transform.position, playerTransform.position) >moveDistance)
        {
            m_State = EnemyState.Idle;
        }
        // moveDistance���� �۰� attackDistance���� ũ�� ������
        else if (Vector3.Distance(transform.position, playerTransform.position) > attackDistance)
        {
            Vector3 dir = (playerTransform.position - transform.position).normalized;
            // ĳ���� ��Ʈ�ѷ� �̿��ؼ� �̵��ϱ�
           cc.Move(dir * moveSpeed * Time.deltaTime);
        }
        else
        {
            // ���� ������ ���� ���� ������ �ٽ� �ʱ�ȭ
            attackRandom = Random.Range(0, 2);

            // ���� attackRandom �� 0�̸� Attack1�� ����
            if (attackRandom == 0)
            {
                m_State = EnemyState.Attack1;

                // �ٷ� ������ �� �ְ� �̸� �ð��� �������´�
                currentTime = attackDelay;
            }
            else
            {
                m_State = EnemyState.Attack2;
                // �ٷ� ������ �� �ְ� �̸� �ð��� �������´�
                currentTime = attackDelay;
            }
        }


    }
    void Attack1()
    {
        // ���� �Ÿ��� ���ðŸ����� ũ�ٸ� Move��
        if (Vector3.Distance(transform.position, playerTransform.position) > attackDistance)
        {
            m_State = EnemyState.Move;
            currentTime = 0;
        }
        // �׷��� �ʰ� ������ �Ÿ����� ũ�� ������
        else if (Vector3.Distance(transform.position, playerTransform.position) > backJumpDistance)
        {
            // ���� �ð����� �÷��̾ �����Ѵ�.
            currentTime += Time.deltaTime;
            if (currentTime > attackDelay)
            {
                // ������ �Ѵ�
                // �ð� �ʱ�ȭ
                currentTime = 0;
            }
        }
        // ������ �Ÿ����� �۴ٸ� �������� ���¸� ��ȯ
        else
        {
            m_State = EnemyState.BackJump;
            currentTime = 0;
        }

    }
    void Attakc2()
    {
        // ���� �Ÿ��� ���ðŸ����� ũ�ٸ� Move��
        if (Vector3.Distance(transform.position, playerTransform.position) > attackDistance)
        {
            m_State = EnemyState.Move;
            currentTime = 0;
        }
        // �׷��� �ʰ� ������ �Ÿ����� ũ�� ������
        else if (Vector3.Distance(transform.position, playerTransform.position) > backJumpDistance)
        {
            // ���� �ð����� �÷��̾ �����Ѵ�.
            currentTime += Time.deltaTime;
            if (currentTime > attackDelay)
            {
                // ������ �Ѵ�
                // �ð� �ʱ�ȭ
                currentTime = 0;
            }
        }
        // ������ �Ÿ����� �۴ٸ� �������� ���¸� ��ȯ
        else
        {
            m_State = EnemyState.BackJump;
            currentTime = 0;
        }
    }
    void BackJump()
    {
        StartCoroutine(BackJumpProcess());
        Vector3 dir = (transform.position - playerTransform.position).normalized;
        // �������� �ڷ�ƾ�� �����Ѵ�.
        cc.Move(dir * moveSpeed * 1.9f);
        // ����� �� Move�� ���� ��ȯ

    }
    void Return()
    {
       

        // ���� �ʱ� ��ġ������ �Ÿ��� 0.1f �̻��̸� �ʱ� ��ġ������ �̵��Ѵ�.
        // �׷��� �ʴٸ� �ڽ��� ��ġ�� �ʱ� ��ġ�� �����ϰ� ���� ���¸� ���� ��ȯ�Ѵ�.
        if (Vector3.Distance(transform.position, originPos) > 0.1f)
        {
            Vector3 dir = (originPos - transform.position).normalized;
            cc.Move(dir * moveSpeed * Time.deltaTime);
            nowreturn = true;
            
        }
        else
        {
            transform.position = originPos;
            // HP ȸ���ϰ�
            // hp = maxHp;
            // ���¸� ��ȯ�Ѵ�.
            m_State = EnemyState.Idle;
            nowreturn = false;
        }
    }
    void Damaged()
    {
        // �ǰ� ���¸� ó���ϱ����� �ڷ�ƾ�� �����Ѵ�.
        StartCoroutine(DamageProcess());
    }
    void Die()
    {
        // �������� �ǰ� �ڷ�ƾ�� �����Ѵ�.
        StopAllCoroutines();

        // �������¸� ó���ϱ� ���� �ڷ�ƾ�� �����Ѵ�.
        StartCoroutine(DieProcess());
    }
    // ������ ó���� �ڷ�ƾ �Լ�
    IEnumerator BackJumpProcess()
    {
        yield return new WaitForSeconds(2f);

        // ���� ���¸� �̵����·� ��ȯ�Ѵ�.
        m_State = EnemyState.Move;
    }
    // ������ ó���� �ڷ�ƾ �Լ�
    IEnumerator DamageProcess()
    {
        yield return new WaitForSeconds(0.5f);

        // ���� ���¸� �̵� ���·� ��ȯ�Ѵ�.
        m_State = EnemyState.Move;
    }
    IEnumerator DieProcess()
    {
        // �̵����� �ʰ� �����
        cc.enabled = false;
        // 2�� ��ٷȴٰ�
        yield return new WaitForSeconds(2);
        // �ı��ȴ�
        Destroy(gameObject);
    }
    // ������ ���� �Լ�
    public void HitEnemy(int hitPower)
    {
        // ���� �̹� �ǰ� ���°ų� ������°ų� ���ͻ��¶�� �ƹ��� ó���� ���� �ʴ´�
        if (m_State == EnemyState.Damaged || m_State == EnemyState.Die || m_State == EnemyState.Return)
        {
            return;
        }
        // �÷��̾ �� ������ ��ŭ HP�� ���δ�.
        hp -= hitPower;

        // ���� ü���� 0���� ũ�ٸ� �ǰݻ��·� ��ȯ�Ѵ�.
        if (hp > 0)
        {
            m_State = EnemyState.Damaged;
            Damaged();
        }
        // �׷��� �ʴٸ� �������·� ��ȯ�Ѵ�.
        else
        {
            m_State = EnemyState.Die;
            Die();
        }
    }

}

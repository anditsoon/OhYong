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
    // ���ʹ� ���� ����
    MorblinState m_State;

    // Ȱ�� �ݰ� 25
    public float activeDistance = 25;
    // �÷��̾� �߰� ����
    public float findDistance = 30f;
    // �̵� ���� ����
    public float moveDistance = 20f;
    // ���� ���� ����
    public float attackDistance = 10f;
    // BackJump �Ÿ�
    public float backJumpDistance = 5f;

    // �̵� �ӵ�
    public float moveSpeed = 5;

    // ���� Transform
    Vector3 originPos;
    // �÷��̾� �÷��̾� ������Ʈ
    public GameObject player;
    // �÷��̾���� �Ÿ�
    float playerDistance;
    // ���� ��ġ���� �Ÿ�
    float originDistance;

    // Attack1�� ���� Attack2�� ���� �����Լ�
    int attackRandom = Random.Range(0, 2);



    void Start()
    {
        // �ʹ� ���´� ���̵� ����
        m_State = MorblinState.Idle;

        // ���� ��ġ�� ������������ �����Ѵ�.
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

        // �÷��̾���� �Ÿ� ��� ���
        playerDistance = Vector3.Distance(transform.position, player.transform.position);
        // ������ġ�� �Ÿ� ��� ���
        originDistance = Vector3.Distance(transform.position, originPos);

       


    }

    void Idle()
    {
        print("Idle");
        if (originDistance>activeDistance)
        {
            //������
            m_State = MorblinState.Return;
        }
        // �׷��� �ʴٸ�
        else
        {
            if(playerDistance > findDistance)
            {
            
            }
            else if(playerDistance > moveDistance)
            {
                // �ٶ󺻴�
                LookPlayer();
                // ����ǥ ����
                        
            }
            else if(playerDistance > attackDistance)
            {
                m_State = MorblinState.Move;
            }
            else if(playerDistance > backJumpDistance)
            {

                attackRandom = Random.Range(0, 2);

                // ���� attackRandom �� 0�̸� Attack1�� ����
                if (attackRandom == 0)
                {
                    //m_State = MorblinState.Attack1;

                    // �ٷ� ������ �� �ְ� �̸� �ð��� �������´�
                    //anim.SetTrigger("MoveToAttack1Delay");
                }
                else
                {
                   // m_State = MorblinState.Attack2;
                    // �ٷ� ������ �� �ְ� �̸� �ð��� �������´�
                    //anim.SetTrigger("MoveToAttack2Delay");
                }
            }
            else
            {
                // �������ض�
            }
        }
    }
    void Move()
    {

        // �ٶ󺻴�
        LookPlayer();

        // ���� ���� �Ÿ����� �����鼭 ���ݰŸ����� ũ�ٸ�
        if (playerDistance <= moveDistance && playerDistance > attackDistance)
        {
            Vector3 dir = player.transform.position - transform.position;
            dir.Normalize();

            //�÷��̾������� ������
            transform.position += dir * moveSpeed * Time.deltaTime; 
        }
        // �׷��� �ʴٸ�
        else
        {
            // ���̵�� ���ư�
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
        // �ٶ󺻴�
        Vector3 dir = player.transform.position - transform.position;
        transform.forward = dir;

    }

}

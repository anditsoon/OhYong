using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // �ȱ� ����
    public float walkSpeed = 7f;
    // �޸��� ����
    public float runSpeed = 20f;
    public bool isRunning = false;
    // �̵� �ӵ� ����
    private float moveSpeed;

    // ĳ���� ��Ʈ�ѷ� ����
    CharacterController cc;

    // �߷� ����
    float gravity = -80f;
    // ���� �ӷ� ����
    float yVelocity = 0;
    // ������ ����
    public float jumpPower = 1f;
    // ���� ���� ����
    public bool isJumping = false; 

    // ����� ���� ������Ʈ
    public GameObject model;

    // �ִϸ�����
    Animator anim;

    private void Start()
    {
        // ĳ���� ��Ʈ�ѷ� ������Ʈ �޾ƿ���
        cc = GetComponent<CharacterController>();

        moveSpeed = walkSpeed;

        anim = transform.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // w, a, s, d Ű�� �Է��ϸ� ĳ���͸� �� �������� �̵���Ű�� �ʹ�
        // [spacebar] Ű�� ������ ĳ���͸� �������� ������Ű�� �ʹ�
        // [shift] Ű�� ������ ĳ���Ͱ� �޸��� �ϰ� �ʹ�

        // 1. ������� �Է��� �޴´�
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // 2. �̵� ������ �����Ѵ�
        Vector3 dir = new Vector3(h, 0, v);
        dir = dir.normalized;

        // 2-1. ���� ī�޶� �������� ������ ��ȯ�Ѵ�
        dir = Camera.main.transform.TransformDirection(dir);


        #region ����

        // 2-2. ����, ���� ���̾���, �ٽ� �ٴڿ� �����ߴٸ�
        if (cc.collisionFlags == CollisionFlags.Below)
        {
            // ����, ���� ���̾��ٸ�,
            if (isJumping)
            {
                // ���� �� ���·� �ʱ�ȣ�Ѵ�
                isJumping = false;
                // ĳ���� ���� �ӵ��� 0���� �����
                yVelocity = 0;
               
            }
        }

        // 2-3. ����, Ű���� [spacebar] Ű�� �Է��߰�, ������ ���� ���� ���¶��,
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            // ĳ���� ���� �ӵ��� �������� �����ϰ� ���� ���·� �����Ѵ�
            yVelocity = jumpPower;
            isJumping = true;
        }

        // 2-3. ĳ���� ���� �ӵ��� �߷� ���� �����Ѵ�
        yVelocity += gravity * Time.deltaTime;
        dir.y = yVelocity;

        Vector3 modelDir = dir;
        modelDir.y = 0;

        #endregion


        #region �޸���
        // 4-1. ����, ���� ���̾���, shift Ű�� �ôٸ� (�ٽ� �ȱ�� ���ƿ´ٸ�)
        if (isRunning && Input.GetButtonUp("Fire3"))
        {
            // ���� �� ���·� �ʱ�ȭ�Ѵ�
            isRunning = false;
            // ĳ���� �ӵ��� �ȱ� �ӵ��� �ǵ�����
            moveSpeed = walkSpeed;
            anim.SetBool("isSprint", false);
        }

        // 4-2. ����, Ű���� [shift] Ű�� �Է��߰�, ������ ���� ���� ���¶��,
        if (Input.GetButtonDown("Fire3") && !isRunning)
        {
            // ĳ���� ���� �ӵ��� �������� �����ϰ� ���� ���·� �����Ѵ�
            moveSpeed = runSpeed;
            isRunning = true;
            anim.SetBool("isSprint", true);

        }

        #endregion



        //3. �̵� �ӵ��� ���� �̵��Ѵ�
        // p = p0 + vt
        cc.Move(dir * moveSpeed * Time.deltaTime);

    
        // dir �� ũ�Ⱑ 0���� ũ�� (�����϶���)
        if (modelDir.magnitude > 0)
        {
            // �����̴� ������ ����� �չ������� ����
            model.transform.forward = modelDir;
            anim.SetBool("isMove", true);
        }
        else
        {
            anim.SetBool("isMove", false);
        }
    }
}

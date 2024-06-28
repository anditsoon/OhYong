using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    // 애니메이터
    Animator anim;

    bool isAttack = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = transform.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAttack && Input.GetMouseButtonDown(0))
        {
            isAttack = true;
            anim.SetTrigger("IdleToAttack");
        }
        else if (isAttack && Input.GetMouseButtonUp(0))
        {
            isAttack = false;
            anim.SetTrigger("AttackToIdle");
        }
    }
}

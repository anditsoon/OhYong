using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class WeaponDestroy : MonoBehaviour
{

    // 폭발 이펙트 프리팹 변수
    public GameObject wpDestroyEffect;

    // 버튼을 눌렀나 안 눌렀나
    bool isRotating = false;

    private void Update()
    {

        if(Input.GetButtonDown("Fire2"))
        {
            isRotating = true;
        }
        // rotate 를 버튼이 눌렸을 때만 하고 싶다
        if (isRotating)
        {
            transform.Rotate(transform.right, 30f, Space.World);
            
        }
        
    }

    // 충돌했을 때의 처리
    private void OnCollisionEnter(Collision collision)
    {

        // 이펙트 프리팹을 생성한다
        GameObject eff = Instantiate(wpDestroyEffect);
        // 이펙트 프리팹의 위치는 무기 오브젝트 자신의 위치와 동일하다
        eff.transform.position = transform.position;

        // 자기 자신을 제거한다
        Destroy(gameObject);

       
    }
}

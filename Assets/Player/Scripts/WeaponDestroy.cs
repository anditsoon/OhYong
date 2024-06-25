using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDestroy : MonoBehaviour
{

    // 폭발 이펙트 프리팹 변수
    public GameObject wpDestroyEffect;

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

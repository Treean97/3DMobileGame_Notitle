using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomGravity : MonoBehaviour
{
    public Vector3 _GravityPointTF = Vector3.zero;  // 중력의 중심점
    public float _GravityForce = 10f; // 중력의 크기
    public float _Damping = 1f;

    private Rigidbody _Rig;

    void Start()
    {
        _Rig = this.gameObject.GetComponent<Rigidbody>();        
    }

    void FixedUpdate()
    {
        if (_GravityPointTF != null && _Rig != null)
        {
            // 중력 중심점에서 오브젝트까지의 방향
            Vector3 tDirectionToCenter = _GravityPointTF - transform.position;

            // 중력 방향으로 힘을 가함
            _Rig.AddForce(tDirectionToCenter.normalized * _GravityForce);

            // 감속 효과 적용 (중심에 가까워질수록 멈추기 위함)
            _Rig.velocity *= 1f - (_Damping * Time.fixedDeltaTime);
        }
    }
}

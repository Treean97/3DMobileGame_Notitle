using EzySlice;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlicerThrower : MonoBehaviour
{
    Vector3 _TargetPos;

    Vector3 _ThrowVec;
    public float _ThrowSpeed;
    float _ThrowAngleRange = 20f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SetTarget();
            ThrowSlicer();
        }
    }

    void SetTarget()
    {
        _TargetPos = Input.mousePosition;

        // ���� ���� ���ϱ�
        Ray tRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        _ThrowVec = tRay.direction.normalized;
    }

    void ThrowSlicer()
    {
        // ������Ʈ ��������
        GameObject tSlicer = ObjectPoolManager._Inst.GetObject("Slicer");

        // ������Ʈ ��ġ ����
        tSlicer.transform.position = Camera.main.transform.position;

        // ���� ȸ�� �� ����
        float tRandom = Random.Range(90 - _ThrowAngleRange, 90 + _ThrowAngleRange);
        tSlicer.transform.rotation = Quaternion.Euler(0, 0, tRandom);

        // ������
        tSlicer.GetComponent<ThrowObject>().SetThrowVector(_ThrowVec);
    }

}


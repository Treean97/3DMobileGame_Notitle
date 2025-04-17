using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class ObjectShooter : MonoBehaviour
{
    // ���������� ������Ʈ �ܰ� 1~n
    [SerializeField]
    int _SpawnableMaxObjectNum;

    public Transform _ShooterTF;        // �߻� ��ġ (ī�޶� ��ġ �Ǵ� �ٸ� ����)
    public float _ShootForce = 0;               // �߻� ��
    public float _PowerScale;              // �߻� �� ����
    public float _MaxShootForce = 15f;    // �ִ� �߻� ��
    public float _MinShootForce = 5f;    // �ִ� �߻� ��
    public float _UpwardForce = 5f;       // ���� ��

    [SerializeField]
    AdditionalForceManager _AdditionalForceManager; // �ܺ� ��

    [SerializeField]
    float _MaxShootDelay;

    float _CurShootDelay;

    bool _CanShoot = false;

    
    //public int _Resolution = 30;           // ������ �׸� ���׸�Ʈ ����
    //public float _TimeStep = 0.1f;         // ���� ���ø� ����
    //public LineRenderer _LineRenderer;     // ���� ������
    


    private void Start()
    {
        _ShootForce = 0;
        //_LineRenderer = this.gameObject.GetComponent<LineRenderer>();

        // �ܺ� �� ����
        _AdditionalForceManager.SetAdditionalForce();

        // �߻� ������ ����
        _CurShootDelay = _MaxShootDelay;
    }

    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.Space))
        //{
        //    _LineRenderer.enabled = true;
        //}

        //if (Input.GetKey(KeyCode.Space))
        //{
        //    DrawTrajectory();
        //}

        if(_CurShootDelay < _MaxShootDelay)
        {
            _CurShootDelay += Time.deltaTime;
        }
        else
        {
            _CanShoot = true;
        }

        if (Input.GetKey(KeyCode.Space) && _CanShoot)
        {
            // _LineRenderer.enabled = false;

            SettingShootPower();
        }

        if (Input.GetKeyUp(KeyCode.Space) && _CanShoot)
        {
            // �߻�
            ShootObject();

            // �ܺ� �� ����
            _AdditionalForceManager.SetAdditionalForce();
        }
    }

    void SettingShootPower()
    {
        _ShootForce += Time.deltaTime * _PowerScale;

    }

    void ShootObject()
    {
        // ���� ������Ʈ ����
        // GameObject tPlanetGO = Instantiate(_PlanetGO, _ShooterTF.position, _ShooterTF.rotation);
        int tRandomInt = Random.Range(0, _SpawnableMaxObjectNum);

        StringBuilder tSB = new StringBuilder();
        tSB.Append("ShootObject_");
        tSB.Append(tRandomInt);

        GameObject tShootObjectGO = ObjectPoolManager._Inst.GetObject(tSB.ToString());

        // ��ġ ����
        tShootObjectGO.transform.position = _ShooterTF.position;

        Rigidbody _Rig = tShootObjectGO.GetComponent<Rigidbody>();

        // ī�޶� ���� �������� �߻� + ���� �� ����
        Vector3 forceDirection 
            = _ShooterTF.forward * (_ShootForce + _MinShootForce)
            + Vector3.up * _UpwardForce
            + _AdditionalForceManager.GetAdditionalForceVector;
        _Rig.AddForce(forceDirection, ForceMode.Impulse);

        // �� �ʱ�ȭ
        _ShootForce = 0f;

        // �߻� ���� �߰�
        GameSceneScoreManager._Inst.AddScoreByShoot();

        // �߻� ������ ���
        _CanShoot = false;
        _CurShootDelay = 0;
    }


    //void DrawTrajectory()
    //{
    //    // ������ �׸� �迭 �ʱ�ȭ
    //    Vector3[] tPoints = new Vector3[_Resolution];

    //    // �ʱ� �ӵ� (ī�޶� ���� + ���� ��)
    //    Vector3 tInitialVelocity = _ShooterTF.forward * _ShootForce + Vector3.up * _UpwardForce;

    //    // �������� �׸��� ���� ������ ��� (�߷� ����)
    //    for (int i = 0; i < _Resolution; i++)
    //    {
    //        float tTime = i * _TimeStep; // �� ������ �ð�
    //        tPoints[i] = CalculatePositionAtTime(_ShooterTF.position, tInitialVelocity, tTime);
    //    }

    //    // ���� �������� ���� �׸���
    //    _LineRenderer.positionCount = _Resolution;
    //    _LineRenderer.SetPositions(tPoints);
    //}

    // Ư�� �ð��� ���� ��ġ ��� (������ � ����)
    //Vector3 CalculatePositionAtTime(Vector3 tStart, Vector3 tInitialVelocity, float tTime)
    //{
    //    // �߷� �߽��� (Vector3.zero)
    //    Vector3 tGravityCenter = Vector3.zero;

    //    // �߻�ü�� �߷� �߽��� ���� ���� ���� ��� (Vector3.zero���� �����ϴ� ����)
    //    Vector3 tDirectionToGravityCenter = tGravityCenter - tStart;

    //    // �߷� �������� �� ��� (�߷� ������ ���� ���� ��)
    //    Vector3 tGravityEffect = tDirectionToGravityCenter.normalized * Physics.gravity.magnitude * tTime * tTime * 0.5f;

    //    // �ʱ� �ӵ��� ���� ��ġ ��ȭ + �߷� �߽��������� ��ġ ��ȭ
    //    Vector3 tPosition = tStart + tInitialVelocity * tTime + tGravityEffect;

    //    return tPosition;
    //}
}

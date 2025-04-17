using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class ObjectShooter : MonoBehaviour
{
    // 생성가능한 오브젝트 단계 1~n
    [SerializeField]
    int _SpawnableMaxObjectNum;

    public Transform _ShooterTF;        // 발사 위치 (카메라 위치 또는 다른 지점)
    public float _ShootForce = 0;               // 발사 힘
    public float _PowerScale;              // 발사 힘 배율
    public float _MaxShootForce = 15f;    // 최대 발사 힘
    public float _MinShootForce = 5f;    // 최대 발사 힘
    public float _UpwardForce = 5f;       // 상향 힘

    [SerializeField]
    AdditionalForceManager _AdditionalForceManager; // 외부 힘

    [SerializeField]
    float _MaxShootDelay;

    float _CurShootDelay;

    bool _CanShoot = false;

    
    //public int _Resolution = 30;           // 궤적을 그릴 세그먼트 개수
    //public float _TimeStep = 0.1f;         // 궤적 샘플링 간격
    //public LineRenderer _LineRenderer;     // 라인 렌더러
    


    private void Start()
    {
        _ShootForce = 0;
        //_LineRenderer = this.gameObject.GetComponent<LineRenderer>();

        // 외부 힘 세팅
        _AdditionalForceManager.SetAdditionalForce();

        // 발사 딜레이 세팅
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
            // 발사
            ShootObject();

            // 외부 힘 세팅
            _AdditionalForceManager.SetAdditionalForce();
        }
    }

    void SettingShootPower()
    {
        _ShootForce += Time.deltaTime * _PowerScale;

    }

    void ShootObject()
    {
        // 랜덤 오브젝트 생성
        // GameObject tPlanetGO = Instantiate(_PlanetGO, _ShooterTF.position, _ShooterTF.rotation);
        int tRandomInt = Random.Range(0, _SpawnableMaxObjectNum);

        StringBuilder tSB = new StringBuilder();
        tSB.Append("ShootObject_");
        tSB.Append(tRandomInt);

        GameObject tShootObjectGO = ObjectPoolManager._Inst.GetObject(tSB.ToString());

        // 위치 설정
        tShootObjectGO.transform.position = _ShooterTF.position;

        Rigidbody _Rig = tShootObjectGO.GetComponent<Rigidbody>();

        // 카메라 전방 방향으로 발사 + 상향 힘 적용
        Vector3 forceDirection 
            = _ShooterTF.forward * (_ShootForce + _MinShootForce)
            + Vector3.up * _UpwardForce
            + _AdditionalForceManager.GetAdditionalForceVector;
        _Rig.AddForce(forceDirection, ForceMode.Impulse);

        // 힘 초기화
        _ShootForce = 0f;

        // 발사 점수 추가
        GameSceneScoreManager._Inst.AddScoreByShoot();

        // 발사 딜레이 대기
        _CanShoot = false;
        _CurShootDelay = 0;
    }


    //void DrawTrajectory()
    //{
    //    // 궤적을 그릴 배열 초기화
    //    Vector3[] tPoints = new Vector3[_Resolution];

    //    // 초기 속도 (카메라 전방 + 상향 힘)
    //    Vector3 tInitialVelocity = _ShooterTF.forward * _ShootForce + Vector3.up * _UpwardForce;

    //    // 포물선을 그리기 위한 물리적 계산 (중력 포함)
    //    for (int i = 0; i < _Resolution; i++)
    //    {
    //        float tTime = i * _TimeStep; // 각 지점의 시간
    //        tPoints[i] = CalculatePositionAtTime(_ShooterTF.position, tInitialVelocity, tTime);
    //    }

    //    // 라인 렌더러로 궤적 그리기
    //    _LineRenderer.positionCount = _Resolution;
    //    _LineRenderer.SetPositions(tPoints);
    //}

    // 특정 시간에 대한 위치 계산 (포물선 운동 공식)
    //Vector3 CalculatePositionAtTime(Vector3 tStart, Vector3 tInitialVelocity, float tTime)
    //{
    //    // 중력 중심점 (Vector3.zero)
    //    Vector3 tGravityCenter = Vector3.zero;

    //    // 발사체와 중력 중심점 간의 방향 벡터 계산 (Vector3.zero에서 시작하는 방향)
    //    Vector3 tDirectionToGravityCenter = tGravityCenter - tStart;

    //    // 중력 방향으로 힘 계산 (중력 방향을 향해 당기는 힘)
    //    Vector3 tGravityEffect = tDirectionToGravityCenter.normalized * Physics.gravity.magnitude * tTime * tTime * 0.5f;

    //    // 초기 속도에 의한 위치 변화 + 중력 중심점으로의 위치 변화
    //    Vector3 tPosition = tStart + tInitialVelocity * tTime + tGravityEffect;

    //    return tPosition;
    //}
}

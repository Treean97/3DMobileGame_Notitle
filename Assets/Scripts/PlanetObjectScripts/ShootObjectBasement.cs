using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootObjectBasement : MonoBehaviour
{
    // 오브젝트 데이터
    [SerializeField]
    ShootObjects _ShootObjectData;
    public ShootObjects GetShootObjectData { get { return _ShootObjectData; } }

    bool _IsStable = false;
    public bool GetIsStable {  get { return _IsStable; } }

    private void Start()
    {
        // transform.localScale = _ShootObjectData.GetShootObjectScale;
        
    }
    
    // 풀에서 재생성 시 다시 IsStable = false;
    private void OnEnable()
    {
        _IsStable = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 충돌 체크
    // 1. 나와 같은 ID인지
    // 2. 같다면 둘다 비활성화 후 다음 ID 오브젝트 풀링 -> 생성할 위치는 ? 
   
    private void OnCollisionEnter(Collision collision)
    {
        // 첫 접촉 시 Stable 상태로 변경
        if(_IsStable == false)
        {
            _IsStable = true;
        }        

        //중앙 구슬이라면 처리하지 않음
        if (collision.gameObject.CompareTag("ZeroPointSphere"))
        {
            return;
        }

        if (collision.gameObject.GetComponent<ShootObjectBasement>()._ShootObjectData.GetShootObjectID == this._ShootObjectData.GetShootObjectID)
        {
            // 둘 중 한 곳에서만 작동할 수 있게 해시코드로 제한
            if(this.gameObject.GetHashCode() < collision.gameObject.GetHashCode())
            {
                // 충돌시 함수 수행
                GameSceneManager._Inst.CollisionPlanet(this.gameObject, collision.gameObject);

                // 점수 추가 (합체된 오브젝트의 ID로 점수 환산)
                GameSceneScoreManager._Inst.AddScoreByCombine(GetShootObjectData.GetShootObjectID);
                
            }
        }
    }
}

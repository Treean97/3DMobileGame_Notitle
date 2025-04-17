using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootObjectBasement : MonoBehaviour
{
    // ������Ʈ ������
    [SerializeField]
    ShootObjects _ShootObjectData;
    public ShootObjects GetShootObjectData { get { return _ShootObjectData; } }

    bool _IsStable = false;
    public bool GetIsStable {  get { return _IsStable; } }

    private void Start()
    {
        // transform.localScale = _ShootObjectData.GetShootObjectScale;
        
    }
    
    // Ǯ���� ����� �� �ٽ� IsStable = false;
    private void OnEnable()
    {
        _IsStable = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // �浹 üũ
    // 1. ���� ���� ID����
    // 2. ���ٸ� �Ѵ� ��Ȱ��ȭ �� ���� ID ������Ʈ Ǯ�� -> ������ ��ġ�� ? 
   
    private void OnCollisionEnter(Collision collision)
    {
        // ù ���� �� Stable ���·� ����
        if(_IsStable == false)
        {
            _IsStable = true;
        }        

        //�߾� �����̶�� ó������ ����
        if (collision.gameObject.CompareTag("ZeroPointSphere"))
        {
            return;
        }

        if (collision.gameObject.GetComponent<ShootObjectBasement>()._ShootObjectData.GetShootObjectID == this._ShootObjectData.GetShootObjectID)
        {
            // �� �� �� �������� �۵��� �� �ְ� �ؽ��ڵ�� ����
            if(this.gameObject.GetHashCode() < collision.gameObject.GetHashCode())
            {
                // �浹�� �Լ� ����
                GameSceneManager._Inst.CollisionPlanet(this.gameObject, collision.gameObject);

                // ���� �߰� (��ü�� ������Ʈ�� ID�� ���� ȯ��)
                GameSceneScoreManager._Inst.AddScoreByCombine(GetShootObjectData.GetShootObjectID);
                
            }
        }
    }
}

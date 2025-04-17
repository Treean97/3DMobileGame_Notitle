using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShootObjectData", menuName = "Scriptable Object/ShootObjectData", order = 0)]
public class ShootObjects : ScriptableObject
{
    /*
    ID
    Name
    Scale
    
    ���ٽ� ���۸� ���� �� ��� ������ ���� ���̴�
    */

    [SerializeField]
    private int _ShootObjectID;
    public int GetShootObjectID { get { return _ShootObjectID; } }
    [SerializeField]
    private string _ShootObjectName;
    public string GetShootObjectName { get {  return _ShootObjectName; } }
    [SerializeField]
    private Vector3 _ShootObjectScale;
    public Vector3 GetShootObjectScale { get {  return _ShootObjectScale; } }
}

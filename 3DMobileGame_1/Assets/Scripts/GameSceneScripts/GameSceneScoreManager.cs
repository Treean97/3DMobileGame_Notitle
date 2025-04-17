using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;


public class GameSceneScoreManager : MonoBehaviour
{
    public static GameSceneScoreManager _Inst;    

    private void Awake()
    {
        if(_Inst == null)
        {
            _Inst = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    [SerializeField]
    GameSceneScoreUI _GameSceneScoreUI;

    [SerializeField]
    int _ShootScore;

    [SerializeField]
    int[] _CombineScore;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddScoreByShoot()
    {
        _GameSceneScoreUI.AddScore(_ShootScore);
    }

    public void AddScoreByCombine(int tObjectID)
    {
        _GameSceneScoreUI.AddScore(_CombineScore[tObjectID]);
    }
}

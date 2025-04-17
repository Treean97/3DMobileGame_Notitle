using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneManager : MonoBehaviour
{
    [SerializeField]
    GameObject _GameTitlePF;

    [SerializeField]
    GameObject _GameTitleGO;

    [SerializeField]
    Transform _GameTitleSpawnTF;

    bool _IsRespawning = false;

    private void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (_GameTitleGO == null && !_IsRespawning)
        {
            StartCoroutine(RespawnGameTitle());
        }
    }

    private IEnumerator RespawnGameTitle()
    {
        _IsRespawning = true; // 재생성 중임을 표시

        // 프레임을 기다려 현재 생성 중인 오브젝트가 할당될 시간을 확보
        yield return null;

        // 오브젝트 생성
        _GameTitleGO = Instantiate(_GameTitlePF, _GameTitleSpawnTF.position, _GameTitlePF.transform.rotation);

        _IsRespawning = false; // 재생성 완료 후 플래그 초기화
    }
}

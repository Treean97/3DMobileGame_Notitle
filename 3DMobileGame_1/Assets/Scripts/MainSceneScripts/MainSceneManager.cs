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
        _IsRespawning = true; // ����� ������ ǥ��

        // �������� ��ٷ� ���� ���� ���� ������Ʈ�� �Ҵ�� �ð��� Ȯ��
        yield return null;

        // ������Ʈ ����
        _GameTitleGO = Instantiate(_GameTitlePF, _GameTitleSpawnTF.position, _GameTitlePF.transform.rotation);

        _IsRespawning = false; // ����� �Ϸ� �� �÷��� �ʱ�ȭ
    }
}

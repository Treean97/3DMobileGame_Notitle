using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameTitle : MonoBehaviour
{
    [SerializeField]
    float _YPosition;

    [SerializeField]
    MainSceneManager _MainSceneMgr;

    [SerializeField]
    Rigidbody _Rig;


    // Start is called before the first frame update
    void Start()
    {
        _MainSceneMgr = GameObject.FindGameObjectWithTag("MainSceneManager").GetComponent<MainSceneManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y >= _YPosition)
        {
            _Rig.useGravity = true;
            _Rig.freezeRotation = true;
        }
        else
        {
            _Rig.useGravity = false;
            _Rig.linearVelocity = Vector3.zero;
            _Rig.freezeRotation = false;
        }
    }

}

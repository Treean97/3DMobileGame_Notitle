using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneStartBtnTrigger : MonoBehaviour
{
    [SerializeField]
    MainSceneBtnManager _MainSceneBtnManager;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        _MainSceneBtnManager.LoadLoadingScene();
    }
}

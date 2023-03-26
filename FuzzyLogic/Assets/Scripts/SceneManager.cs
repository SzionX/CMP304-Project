using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    //Serialize Fields
    [Header("Camera Reference Fields")]
    [SerializeField] private Camera mainCam;
    [SerializeField] private Camera followCam;

    //variables
    bool isMainCam;

    // Start is called before the first frame update
    void Start()
    {
        isMainCam = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.T))
        {
            isMainCam = !isMainCam;
            SwitchCam();
        }
    }

    void SwitchCam()
    {
        switch (isMainCam)
        {
            case true:
                mainCam.gameObject.SetActive(true);
                followCam.gameObject.SetActive(false);
                break;
            case false:
                mainCam.gameObject.SetActive(false);
                followCam.gameObject.SetActive(true);
                break;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CinemacineSwitcher : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCam;

    public CinemachineFreeLook freeLookcam;

    public bool usingFreeLook = false;

    // Start is called before the first frame update
    void Start()
    {
        virtualCam.Priority = 10;
        freeLookcam.Priority = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            usingFreeLook = !usingFreeLook;
            if (usingFreeLook)
            {
                freeLookcam.Priority = 20;
                virtualCam.Priority = 0;
            }
            else
            {
                virtualCam.Priority = 20;
                freeLookcam.Priority = 0;
            }
        }
    }
}

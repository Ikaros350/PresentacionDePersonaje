using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class DollyActivator : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera myCam;
    [SerializeField] CinemachineTrackedDolly mydolly;
    
    // Start is called before the first frame update
    void Start()
    {
        mydolly = myCam.GetCinemachineComponent<CinemachineTrackedDolly>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeDollyValue(float value)
    {
        mydolly.m_PathPosition = value;
    }
}

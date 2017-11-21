using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

    public static CameraManager Instance;
    Transform m_Target;
    public Vector3 m_Offset;
	void Start () {
        Instance = this;
	}
	
    public void SetTarget(Transform target)
    {
        m_Target = target;
    }
	// Update is called once per frame
	void Update () {
		
	}

    void LateUpdate()
    {
        if (m_Target == null) return;
        transform.position = m_Target.position + m_Offset;
        transform.LookAt(m_Target);
    }
}

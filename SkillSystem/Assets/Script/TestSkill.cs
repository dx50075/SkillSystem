using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SkillCore;
public class TestSkill : MonoBehaviour {

    public TextAsset text = null;
    public static Actor m_Actor = null;
    public int SkillID = 1000;
	void Start () {
        SkillSystem.Instance.LoadSkill(text.text);
        SkillEventMgr.Instance.RegisterSkillEvent();
        m_Actor = ActorManager.Instance.CreateActor();
        if(CameraManager.Instance != null)
            CameraManager.Instance.SetTarget(m_Actor.transform);

        Actor a = ActorManager.Instance.CreateActor();
        a.SetPos(m_Actor.transform.position + new Vector3(Random.Range(5, 10), 0f, Random.Range(5, 10)));
        a = ActorManager.Instance.CreateActor();
        a.SetPos(m_Actor.transform.position + new Vector3(Random.Range(5, 10), 0f, Random.Range(5, 10)));
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnGUI()
    {
        if (GUILayout.Button("Reload skill.txt"))
        {
            SkillSystem.Instance.Reset();
            SkillSystem.Instance.LoadSkill(text.text);
        }
        if(GUILayout.Button("Use Skill"))
        {
            m_Actor.UseSkill(SkillID);
        }
        if (GUILayout.Button("Create Actor"))
        {
            Actor a = ActorManager.Instance.CreateActor();
            a.SetPos(m_Actor.transform.position + new Vector3(Random.Range(5, 10), 0f, Random.Range(5, 10)));
        }
    }
}

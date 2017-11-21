using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SkillCore;
public class Actor : MonoBehaviour {
    public int UID = -1;
    private Animator m_Animator;
    private SkillData m_SkillData = new SkillData();
    List<SkillInstance> m_CurSkills = new List<SkillInstance>();

    public void Init(int uid)
    {
        UID = uid;
        InitSkillData();
    }
	public void SetModel (GameObject go) 
    {
        if(go == null)
        {
            Debug.LogError("Actor's model is null");
            return;
        }
        m_Animator = go.GetComponentInChildren<Animator>();
        go.transform.SetParent(transform);
	}

    public void SetPos(Vector3 pos)
    {
        transform.position = pos;
    }
	
	void InitSkillData()
    {
        m_SkillData.m_AttackerID = this.UID;
    }
	void Update () 
    {
        UpdateMove();
        int count = m_CurSkills.Count;
        if (count <= 0) return;

        for(int i = 0; i < count;i++)
        {
            SkillInstance skill = m_CurSkills[i];
            if(skill != null)
            {
                if(skill.UpdateTriggers(Time.time,m_SkillData))
                {
                    m_CurSkills.RemoveAt(i);
                    PlayAnimation("Idle");
                    return;
                }
            }
        }       
	}

    public void PlayAnimation(string name)
    {
        if (m_Animator == null) return;
        m_Animator.Play(name);
        //m_Animator.CrossFade(name, 0.2f);
    }

    public void UseSkill(int skillID)
    {
        m_SkillData.m_TargetID = 2;
        SkillInstance skill = SkillSystem.Instance.NewSkillInstance(skillID);
        skill.Begin(Time.time);       
        m_CurSkills.Add(skill);
    }

   

    Vector3 m_Des;

    bool m_IsMoving = false;
    public void MoveTo(Vector3 des)
    {
        PlayAnimation("Run");
        m_Des = des;
        
        transform.LookAt(m_Des);
        m_IsMoving = true;
    }

    public float m_Speed = 5f;
    public void UpdateMove()
    {
        if (!m_IsMoving)
            return;
        Vector3 distance = m_Des - transform.position;
        float delta = m_Speed * Time.deltaTime;
        if(distance.sqrMagnitude <= delta * delta )
        {
            StopMove();
            return;
        }
        Vector3 dir = distance.normalized;
        Vector3 pos = transform.position + Time.deltaTime * m_Speed * dir;
        transform.position = pos;
    }

    public void StopMove()
    {
        m_IsMoving = false;
        PlayAnimation("Idle");
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using SkillCore;

public class BulletTrigger : AbstractSkillTrigger
{
    public string m_EffectName;
    public float m_Speed;

    //参数（触发时间，子弹特效名称，子弹速度）
    public override void Init(string args)
    {
        string[] line = args.Split(',');
        float.TryParse(line[0], out m_StartTime);
        m_EffectName = line[1];
        float.TryParse(line[2], out m_Speed);
        Debug.LogFormat("子弹触发时间 {0},动作名 {1}", m_StartTime, m_EffectName);
    }

    public override ISkillTrigger Clone()
    {
        ISkillTrigger t = (ISkillTrigger)this.MemberwiseClone();
        return t;
    }

    public override bool Execute(SkillData data, float curTime)
    {
        data.m_EffectName = this.m_EffectName;
        data.m_BulletSpeed = m_Speed;
        m_IsExecuted = true;
        EventDispatcher.TriggerEvent<SkillData>(EventID.Bullet, data);
        return true;
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using SkillCore;

public class PlayEffectTrigger : AbstractSkillTrigger
{
    public string m_EffectName;
    public float m_EffectDuration;
    //参数（触发时间，特效名称，持续时间）
    public override void Init(string args)
    {
        string[] line = args.Split(',');
        float.TryParse(line[0], out m_StartTime);
        m_EffectName = line[1];
        float.TryParse(line[2], out m_EffectDuration);
        Debug.LogFormat("特效触发器，触发时间 {0}，特效名 {1}", m_StartTime, m_EffectName);
    }

    public override ISkillTrigger Clone()
    {
        ISkillTrigger t = (ISkillTrigger)this.MemberwiseClone();
        return t;
    }

    public override bool Execute(SkillData data, float curTime)
    {
        data.m_EffectName = this.m_EffectName;
        data.m_EffectDuration = this.m_EffectDuration;
        EventDispatcher.TriggerEvent<SkillData>(EventID.PlayEffect, data);
        m_IsExecuted = true;
        return true;
    }
}


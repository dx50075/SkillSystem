using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using SkillCore;

public class PlayAnimationTrigger : AbstractSkillTrigger
{
    public string m_AnimationName;
    //参数，（触发时间，动作名称）
    public override void Init(string args)
    {
        string[] line = args.Split(',');
        float.TryParse(line[0], out m_StartTime);
        m_AnimationName = line[1];
        Debug.LogFormat("动作触发器，触发时间 {0},动作名 {1}", m_StartTime, m_AnimationName);
    }

    public override ISkillTrigger Clone()
    {
        ISkillTrigger t = (ISkillTrigger)this.MemberwiseClone();
        return t;
    }

    public override bool Execute(SkillData data, float curTime)
    {
        data.m_AnimationName = this.m_AnimationName;
        EventDispatcher.TriggerEvent<SkillData>(EventID.PlayAnimation, data);
        m_IsExecuted = true;
        return true;
    }
}


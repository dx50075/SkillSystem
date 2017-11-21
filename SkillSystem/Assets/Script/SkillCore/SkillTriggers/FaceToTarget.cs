using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using SkillCore;

public class FaceToTargetTrigger : AbstractSkillTrigger
{
    //参数(触发时间)
    public override void Init(string args)
    {
        string[] line = args.Split(',');
        float.TryParse(line[0], out m_StartTime);
        Debug.LogFormat("面向目标触发时间 {0}", m_StartTime);
    }

    public override ISkillTrigger Clone()
    {
        ISkillTrigger t = (ISkillTrigger)this.MemberwiseClone();
        return t;
    }

    public override bool Execute(SkillData data, float curTime)
    {
        m_IsExecuted = true;
        EventDispatcher.TriggerEvent<SkillData>(EventID.FaceToTarget, data);
        return true;
    }
}


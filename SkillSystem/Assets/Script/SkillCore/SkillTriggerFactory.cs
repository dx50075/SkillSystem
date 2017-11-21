using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkillCore
{
    public class BaseSkillTriggerFactory
    {
        public virtual ISkillTrigger Create() { return null; }
    }
    public class SkillTriggerFactory<T> : BaseSkillTriggerFactory where T : ISkillTrigger, new()
    {
        public override ISkillTrigger Create()
        {
            return new T();
        }
    }
}

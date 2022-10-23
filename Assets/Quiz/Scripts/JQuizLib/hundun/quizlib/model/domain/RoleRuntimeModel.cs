using hundun.quizlib.prototype;
using hundun.quizlib.view.role;
using hundun.quizlib.view.skill;

using System;
using System.Collections.Generic;

namespace hundun.quizlib.model.domain
{
    public class RoleRuntimeModel {
        
        public RolePrototype prototype { get; set; }

        public List<SkillSlotRuntimeModel> skillSlotRuntimeModels { get; set; }

        public RoleRuntimeModel(RolePrototype prototype, List<SkillSlotRuntimeModel> skillSlotRuntimeModels) {
            this.prototype = prototype;
            this.skillSlotRuntimeModels = skillSlotRuntimeModels;
        }
        
        

        public RoleRuntimeView toRoleRuntimeInfoDTO() {
            RoleRuntimeView dto = new RoleRuntimeView();
            dto.name = (prototype.name);
            List<SkillSlotRuntimeView> skillSlotRuntimeViews = new List<SkillSlotRuntimeView>();
            skillSlotRuntimeModels.ForEach(model => skillSlotRuntimeViews.Add(new SkillSlotRuntimeView(model.prototype.name, model.remainUseTime)));
            dto.skillSlotRuntimeViews = (skillSlotRuntimeViews);
            return dto;
        }

        
    }
}


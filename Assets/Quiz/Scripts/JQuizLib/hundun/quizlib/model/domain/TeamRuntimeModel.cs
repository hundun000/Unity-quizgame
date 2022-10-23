using hundun.quizlib.exception;
using hundun.quizlib.model.domain.buff;
using hundun.quizlib.prototype;
using hundun.quizlib.view.buff;
using hundun.quizlib.view.team;

using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace hundun.quizlib.model.domain
{
    public class TeamRuntimeModel {
        
        private static double DEFAULT_HIT_PICK_RATE = 0.2;
        private static double HIT_PICK_RATE_INCREASE_STEP = 0.05;

        //@Getter
        public TeamPrototype prototype { get; private set; }
        //@Getter
        public RoleRuntimeModel roleRuntimeModel { get; private set; }

        private int matchScore;
        private int health;
        
        private List<BuffRuntimeModel> buffs = new List<BuffRuntimeModel>();
        //@Getter
        public double hitPickRate { get; private set; }


        public TeamRuntimeModel(TeamPrototype prototype, RoleRuntimeModel roleRuntimeModel) {
            this.prototype = prototype;
            int currentHealth = -1;
            this.matchScore = 0;
            setHealth(currentHealth);
            this.roleRuntimeModel = roleRuntimeModel;
            
            resetHitPickRate();
            buffs.Clear();
        }
        
        public void addScore(int addition) {
            matchScore += addition;
        }

        
        public void resetHitPickRate() {
            this.hitPickRate = DEFAULT_HIT_PICK_RATE;
        }
        
        public void increaseHitPickRate() {
            this.hitPickRate = Math.Min(hitPickRate + HIT_PICK_RATE_INCREASE_STEP, 1);
        }
        
        public String getName() {
            return prototype.name;
        }

        
        public bool isAlive() {
            return this.health > 0;
        }

        public String getRoleName() {
            return roleRuntimeModel != null ? roleRuntimeModel.prototype.name :null;
        }
        


        
        public int getMatchScore() {
            return matchScore;
        }
        
        public List<BuffRuntimeModel> getBuffs() {
            return buffs;
        }
        
        public void setHealth(int health) {
            this.health = health;
        }

        
        public void addBuff(BuffRuntimeModel newBuff) {
            foreach (BuffRuntimeModel buff in buffs) {
                if (buff.prototype.name.Equals(newBuff.prototype.name)) {
                    buff.addDuration(newBuff.duration);
                    return;
                }
            }
            
            buffs.Add(newBuff);
        }
        
        public SkillSlotRuntimeModel getSkillSlotRuntime(String skillName) {
            if (roleRuntimeModel != null) {
                foreach (SkillSlotRuntimeModel skillSlotRuntimeModel in roleRuntimeModel.skillSlotRuntimeModels) {
                    if (skillSlotRuntimeModel.prototype.name.Equals(skillName)) {
                        return skillSlotRuntimeModel;
                    }
                }
            }
            throw new NotFoundException("skill in team", skillName);
        }

        
        
        public TeamRuntimeView toTeamRuntimeInfoDTO() {
            TeamRuntimeView dto = new TeamRuntimeView();
            dto.name = (getName());
            dto.matchScore = (matchScore);
            if (roleRuntimeModel != null) {
                dto.roleRuntimeInfo = (roleRuntimeModel.toRoleRuntimeInfoDTO());
            }
            List<BuffRuntimeView> buffRuntimeViews = new List<BuffRuntimeView>();
            buffs.ForEach(item => buffRuntimeViews.Add(item.toRunTimeBuffDTO()));
            dto.runtimeBuffs = (buffRuntimeViews);
            dto.alive = (isAlive());
            dto.health = (health);
            return dto;
        }
        
        public bool isPickAndNotBan(HashSet<String> questionTags) {
            foreach (String questionTag in questionTags) {
                if (prototype.pickTags.Contains(questionTag) && !prototype.banTags.Contains(questionTag)) {
                    return true;
                }
            }
            return false;
        }
        
        public bool isNotBan(HashSet<String> questionTags) {
            foreach (String questionTag in questionTags) {
                if (prototype.banTags.Contains(questionTag)) {
                    return false;
                }
            }
            return true;
        }
        
    }
}




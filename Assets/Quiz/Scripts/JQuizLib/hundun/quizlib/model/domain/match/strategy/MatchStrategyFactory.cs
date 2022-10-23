using hundun.quizlib.context;
using hundun.quizlib.exception;
using hundun.quizlib.prototype.match;

using System;
using System.Collections.Generic;

namespace hundun.quizlib.model.domain.match.strategy
{
    public class MatchStrategyFactory : IQuizComponent {
    
        private QuizComponentContext context;
        
        //@Override
        public void postConstruct(QuizComponentContext context) {
            this.context = context;
        }
        
        public BaseMatchStrategy getMatchStrategy(MatchStrategyType type) {
            switch (type) {
                case MatchStrategyType.ENDLESS:
                    return new EndlessStrategy(context.questionService, context.teamService, context.roleSkillService, context.buffService);
                case MatchStrategyType.PRE:
                    return new PreStrategy(context.questionService, context.teamService, context.roleSkillService, context.buffService);
                case MatchStrategyType.MAIN:
                    return new MainStrategy(context.questionService, context.teamService, context.roleSkillService, context.buffService);
                default:
                    throw new NotFoundException("MatchStrategyType", type.ToString());
            }
        }

        
    }
}



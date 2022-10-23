using hundun.quizlib.prototype.match;
using System.Collections.Generic;

namespace hundun.quizlib.tool
{

    public class MatchStateUtils {
        
        static Dictionary<MatchState, HashSet<ClientActionType>> stateTransitionRules;
        static MatchStateUtils(){
            stateTransitionRules = new Dictionary<MatchState, HashSet<ClientActionType>>();
            addStateTransitionRule(MatchState.WAIT_START, ClientActionType.START_MATCH);
            addStateTransitionRule(MatchState.WAIT_GENERATE_QUESTION, ClientActionType.NEXT_QUESTION);
            addStateTransitionRule(MatchState.WAIT_ANSWER, ClientActionType.ANSWER);
            addStateTransitionRule(MatchState.WAIT_ANSWER, ClientActionType.USE_SKILL);
        }
        
        private static void addStateTransitionRule(MatchState state, ClientActionType actionType) {
            if (!stateTransitionRules.containsKey(state)) {
                stateTransitionRules.put(state, new HashSet<ClientActionType>(4));
            }
            stateTransitionRules.get(state).Add(actionType);
        }
        
        
        public static HashSet<ClientActionType> getValidClientActions(MatchState state) {
            return stateTransitionRules.getOrDefault(state, new HashSet<ClientActionType>(0));
        }
        
        public static bool check(MatchState state, ClientActionType actionType) {
            return stateTransitionRules.get(state).Contains(actionType);
        }
    }
}




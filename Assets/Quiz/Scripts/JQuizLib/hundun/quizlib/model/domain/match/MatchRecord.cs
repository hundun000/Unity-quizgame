using System;
using System.Collections.Generic;

namespace hundun.quizlib.model.domain.match
{
    public class MatchRecord {
        
        private String matchId;
        private Dictionary<String, int> scores = new Dictionary<String, int>();
        
        public MatchRecord(BaseMatch match) {
            this.matchId = match.getSessionId();
            match.teamRuntimeModels.ForEach(team => scores.put(team.getName(), team.getMatchScore()));
        }
        
        public String getMatchId() {
            return matchId;
        }
        
        public Dictionary<String, int> getScores() {
            return scores;
        }

    }
}



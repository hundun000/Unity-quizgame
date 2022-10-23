using hundun.quizlib.prototype.match;

using System;
using System.Collections.Generic;

namespace hundun.quizlib.model.domain.match
{

    internal class RecordNode{
        public RecordNode(string teamName, string answer, string questionId, AnswerType answerType)
        {
            this.teamName = teamName;
            this.answer = answer;
            this.questionId = questionId;
            this.answerType = answerType;
        }

        public String teamName {get; set;}
        public String answer {get; set;}
        public String questionId {get; set;}
        public AnswerType answerType {get; set;}
    }


    public class AnswerRecorder {
        
        /**
        * 绑定LinkedList实现，以使用LinkedList特有方法
        */
        List<RecordNode> nodes = new List<RecordNode>();
        
        public void addRecord(String teamName, String answer, String questionId, AnswerType answerType) {
            nodes.Insert(0, new RecordNode(teamName, answer, questionId, answerType));
        }
        
        
        
        /**
        * 返回teamName连续错误次数是否大于等于num。
        * @param teamName
        * @param num
        * @return
        */
        public bool isConsecutiveWrongAtLeast(String teamName, int num) {
            return countConsecutiveWrong(teamName, num) >= num;
        }
        
        public bool isSumAtLeast(String teamName, int num) {
            return count(teamName, AnswerType.NULL, num, false) >= num;
        }
        
        public int countConsecutiveWrong(String teamName, int max) {
            return count(teamName, AnswerType.WRONG, max, true);
        }
        
        public int countSum(String teamName, int max) {
            return count(teamName, AnswerType.NULL, max, false);
        }

        /**
        * 可用统计某队[连续/累计][正确/错误/全部]回答的个数
        * @param teamName
        * @param check 计数的类型；null:计数任意回答
        * @param max 计数等于max时停止
        * @param consecutive 是否要求连续满足计数的类型；
        * @return
        */
        public int count(String teamName, AnswerType check, int max, bool consecutive) {
            int num = 0;
            foreach (RecordNode node in nodes) {
                if(node.teamName != (teamName)) {
                    continue;
                }
                
                if (node.answerType == AnswerType.SKIPPED) {
                    continue;
                } else {
                    if(check == AnswerType.NULL || node.answerType == check) {
                        // 类型符合
                        num++;
                    } else {
                        if (consecutive) {
                        // 类型不符合，且要求连续符合，故跳出
                        break; 
                        }
                    }
                }
                
                if (num == max) {
                    break;
                }
            }
            return num;
        }

    }
}



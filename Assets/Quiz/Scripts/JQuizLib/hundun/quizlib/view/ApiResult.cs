using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace hundun.quizlib.view
{
    /**
    * @author hundun
    * Created on 2019/11/04
    * @param <T>
    */
    public class ApiResult<T> {
        
        private const String SUCCESS_MESSAGE = "SUCCESS";
        private const int SUCCESS_STATUS = 200;
        private const int FAIL_STATUS = 400;
        
        [JsonProperty]
        String message {get; set;}
        [JsonProperty]
        public int status {get; set;}
        [JsonProperty]
        public T payload {get; set;}
        [JsonProperty]
        public int retcode;
        
        public ApiResult() {
            
        }

        public ApiResult(String failMessage, int retcode) {
            this.message = failMessage;
            this.status = FAIL_STATUS;
            this.payload = default(T);
            this.retcode = -1;
        }

        
        public ApiResult(T payload) {
            this.message = SUCCESS_MESSAGE;
            this.status = SUCCESS_STATUS;
            this.payload = payload;
            this.retcode = 0;
        }


        
    }
}


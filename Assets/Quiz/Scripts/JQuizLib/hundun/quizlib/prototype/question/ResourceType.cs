using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace hundun.quizlib.prototype.question
{
    public enum ResourceType {
        IMAGE,
        VOICE,
        NONE
    }
    public class ResourceTypeCompanion {

        public static ResourceType getByLocalFileExtension(String name) {
            if (name.endsWith(".jpg") || name.endsWith(".png")) {
                return ResourceType.IMAGE;
            }
            
            if (name.endsWith(".Ogg") || name.endsWith(".ogg")) {
                return ResourceType.VOICE;
            }
            
            return ResourceType.NONE;
        }

    }
}


using System;

namespace hundun.quizlib.context
{
    public interface IFrontEnd
    {

        String[] fileGetChilePathNames(String folder);

        String fileGetContent(String str);

    }

}

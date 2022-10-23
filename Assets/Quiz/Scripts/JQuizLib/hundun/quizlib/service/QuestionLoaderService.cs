using hundun.quizlib;
using hundun.quizlib.context;
using hundun.quizlib.exception;
using hundun.quizlib.model.domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace hundun.quizlib.service
{
    public class QuestionLoaderService : IQuizComponent
    {

        //    public File RESOURCE_ICON_FOLDER;
        //    private File PACKAGES_FOLDER;
        public static String PACKAGE_FOLDER = "quiz/question_packages/";
        public static String RELEASE_PACKAGE_NAME = "questions";
        public static String PRELEASE_PACKAGE_NAME = "questions_small";
        public static String TEST_PACKAGE_NAME = "questions_test";
        public static String TEST_SMALL_PACKAGE_NAME = "questions_test_small";

        public const String TAGS_SPLIT = ";";

        IFrontEnd frontEnd;

        public void postConstruct(QuizComponentContext context)
        {
            this.frontEnd = context.frontEnd;
        }

        //    public void lateInitFolder(File PACKAGES_FOLDER, File RESOURCE_ICON_FOLDER) {
        //        this.PACKAGES_FOLDER = PACKAGES_FOLDER;
        //        this.RESOURCE_ICON_FOLDER = RESOURCE_ICON_FOLDER;
        //    }

        public static List<QuestionModel> loadQuestionsFromFile(String fileContent, String fileName)
        {
            Regex rgx = new Regex("\r?\n|\r");
            List<String> lines = JavaFeatureExtension.ArraysAsList(rgx.Split(fileContent));
            try
            {
                return parseTextToQuestions(lines);
            }
            catch (QuestionFormatException e)
            {
                throw new QuestionFormatException(e, fileName);
            }
        }

        public List<QuestionModel> loadAllQuestions(String packageName)
        {

            String[] chileNames = frontEnd.fileGetChilePathNames(PACKAGE_FOLDER + packageName);
            List<QuestionModel> questions = new List<QuestionModel>();

            foreach (String chileName in chileNames)
            {
                String fileContent = frontEnd.fileGetContent(PACKAGE_FOLDER + packageName + Path.DirectorySeparatorChar + chileName);
                questions.addAll(loadQuestionsFromFile(fileContent, chileName));
            }
            return questions;
        }




        // FEFF because this is the Unicode char represented by the UTF-8 byte order mark (EF BB BF).
        public const String UTF8_BOM = "\uFEFF";
        /**
         * 每一题后通过至少一个空白行分割；最后一题后可以无空行
         * @param lines
         * @param singleTagName
         * @return
         * @throws QuestionFormatException
         */
        private static List<QuestionModel> parseTextToQuestions(List<String> lines)
        {

            int currentLine = 0;

            String numText = lines.get(currentLine++);
            if (numText.StartsWith(UTF8_BOM))
            {
                numText = numText.Substring(1);
            }

            String tagsText = lines.get(currentLine++);
            HashSet<String> tagNames;
            if (tagsText.Trim().Length != 0)
            {
                tagNames = tagsText.Split(TAGS_SPLIT)
                        .Select(it => it.Trim())
                        .ToHashSet()
                        ;
                currentLine++;
            }
            else
            {
                tagNames = new HashSet<String>(0);
            }


            int num = Int32.Parse(numText);
            List<QuestionModel> questions = new List<QuestionModel>(num);

            while (currentLine < lines.Count)
            {
                int i0 = currentLine;
                try
                {

                    String stem = lines.get(currentLine++);
                    String optionA = lines.get(currentLine++);
                    String optionB = lines.get(currentLine++);
                    String optionC = lines.get(currentLine++);
                    String optionD = lines.get(currentLine++);
                    String answer = lines.get(currentLine++);
                    String resourceText = lines.get(currentLine++);

                    while (currentLine < lines.Count)
                    {
                        String next = lines.get(currentLine);
                        if (next.Trim().Length == 0)
                        {
                            currentLine++;
                        }
                        else
                        {
                            break;
                        }
                    }

                    bool elementLost = stem.Length == 0
                            || optionA.Length == 0
                            || optionB.Length == 0
                            || optionC.Length == 0
                            || optionD.Length == 0
                            || answer.Length == 0
                            || resourceText.Length == 0
                            ;
                    if (elementLost)
                    {
                        throw new QuestionFormatException(i0, currentLine, questions.Count + 1, "题目组成");
                    }

                    QuestionModel question = new QuestionModel(stem, optionA, optionB, optionC, optionD, answer, resourceText, tagNames);

                    questions.Add(question);
                }
                catch (Exception e)
                {
                    throw new QuestionFormatException(i0, currentLine, questions.Count + 1, e.Message);
                }

            }
            return questions;
        }





    }


}




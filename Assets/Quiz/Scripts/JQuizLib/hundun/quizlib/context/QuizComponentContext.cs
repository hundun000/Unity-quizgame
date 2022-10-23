using hundun.quizlib.exception;
using hundun.quizlib.model.domain.match.strategy;
using hundun.quizlib.service;

namespace hundun.quizlib.context 
{

    public class QuizComponentContext
    {

        public IFrontEnd frontEnd { get; private set; }

        public GameService gameService { get; private set; }
        public QuestionService questionService { get; private set; }
        public TeamService teamService { get; private set; }
        public RoleSkillService roleSkillService { get; private set; }
        public BuffService buffService { get; private set; }
        public SessionService sessionService { get; private set; }
        public MatchStrategyFactory matchStrategyFactory { get; private set; }
        public QuestionLoaderService questionLoaderService { get; private set; }
        public BuiltinDataConfiguration builtinDataConfiguration { get; private set; }

        public static class Factory
        {
            public static QuizComponentContext create(IFrontEnd frontEnd)
            {
                QuizComponentContext context = new QuizComponentContext();


                context.frontEnd = frontEnd;
                context.gameService = new GameService();
                context.questionService = new QuestionService();
                context.teamService = new TeamService();
                context.roleSkillService = new RoleSkillService();
                context.buffService = new BuffService();
                context.sessionService = new SessionService();
                context.matchStrategyFactory = new MatchStrategyFactory();
                context.questionLoaderService = new QuestionLoaderService();
                context.builtinDataConfiguration = new BuiltinDataConfiguration();


                context.gameService.postConstruct(context);
                context.questionService.postConstruct(context);
                context.teamService.postConstruct(context);
                //context.roleSkillService.postConstruct(context);
                //context.buffService.postConstruct(context);
                context.sessionService.postConstruct(context);
                context.matchStrategyFactory.postConstruct(context);
                context.questionLoaderService.postConstruct(context);
                context.builtinDataConfiguration.postConstruct(context);
            
                return context;
            }
        }
    }

}


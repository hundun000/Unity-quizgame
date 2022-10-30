public class JQuizViewModelContext : BaseViewModelContext {

    QuizGdxGame game;
    
    public JQuizViewModelContext(QuizGdxGame game) {
        this.game = game;
        
    }


    override public void lazyInitOnGameCreate() {

        
    }

    override public void disposeAll() {

    }

    


}
public class QuizViewModelContext : BaseViewModelContext {

    QuizGdxGame game;
    
    public QuizViewModelContext(QuizGdxGame game) {
        this.game = game;
    }


    override public void lazyInitOnGameCreate() {

        
    }

    override public void disposeAll() {

    }

    


}
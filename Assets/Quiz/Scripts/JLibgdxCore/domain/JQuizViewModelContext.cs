public class JQuizViewModelContext : BaseViewModelContext {

    JQuizGdxGame game;
    
    public JQuizViewModelContext(JQuizGdxGame game) {
        this.game = game;
        
    }


    override public void lazyInitOnGameCreate() {

        
    }

    override public void disposeAll() {

    }

    


}
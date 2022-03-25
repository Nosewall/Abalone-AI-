public class miniMaxState{
    int value;
    State state;

    public miniMaxState(int value, State state){
        this.value = value;
        this.state = state;
    }

    public int  getValue(){
        return this.value ;
    }
    public State getState(){
        return state;
    }
}
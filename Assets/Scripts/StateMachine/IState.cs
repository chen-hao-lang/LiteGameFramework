public interface IState
{
    void Create(StateMachine _machine,IState _to = null);
    void Tick();
    void OnEnter();
    void OnExit();
}
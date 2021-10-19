namespace Infrastructure.States
{
  public interface IState : IExitableState
  {
    void Enter();
  }

  public interface IPayLoadState<TPayload> : IExitableState
  {
    void Enter(TPayload payload);
  }
  
  public interface IExitableState
  {
    void Exit();
  }
}
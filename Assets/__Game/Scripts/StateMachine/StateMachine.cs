using System;

namespace Test_Game
{
  public class StateMachine
  {
    public event Action<State> StateChanged;

    public State CurrentState { get; private set; }
    public State PreviousState { get; private set; }

    public void Init(State initState)
    {
      CurrentState = initState;
      CurrentState.Enter();
    }

    public void ChangeState(State newState)
    {
      if (newState == CurrentState) return;

      PreviousState = CurrentState;
      CurrentState.Exit();
      CurrentState = newState;
      CurrentState.Enter();

      StateChanged?.Invoke(CurrentState);
    }
  }
}
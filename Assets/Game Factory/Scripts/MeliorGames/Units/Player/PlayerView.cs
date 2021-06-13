using System;
using Game_Factory.Scripts.MeliorGames.Units.Animation;
using UnityEngine;

namespace Game_Factory.Scripts.MeliorGames.Units.Player
{
  public class PlayerView : MonoBehaviour, IAnimationStateReader
  {
    public float ThrowPower
    {
      get => _throwPower;
      set => _throwPower = Mathf.Clamp(value, 0f, 0.99f);
    }

    [Range(0,1)]
    [SerializeField]
    public float _throwPower = 0.0f;
    
    public AnimatorState State { get; private set; }

    public event Action<AnimatorState> StateEntered;
    public event Action<AnimatorState> StateExited; 
    
    private Animator animator;
    
    private static readonly int ThrowControl = Animator.StringToHash("ThrowControl");
    private static readonly int Aiming = Animator.StringToHash("Aiming");
    
    private readonly int crouchStateHash = Animator.StringToHash("Crouch");
    private readonly int preparationStateHash = Animator.StringToHash("Preparation");
    private readonly int throwStateHash = Animator.StringToHash("Throw");
    

    private void Awake()
    {
      animator = GetComponent<Animator>();
    }

    public void ControlThrow(float value)
    {
      animator.SetFloat(ThrowControl, value);
    }

    public void SwitchAiming(bool state)
    {
      animator.SetBool(Aiming, state);
    }

    public void EnteredState(int stateHash)
    {
      State = StateFor(stateHash);
      StateEntered?.Invoke(State);
      //Debug.Log($"Enter state {StateFor(stateHash).ToString()}");
    }

    public void ExitedState(int stateHash)
    {
      StateExited?.Invoke(StateFor(stateHash));
      //Debug.Log($"Exited state {StateFor(stateHash).ToString()}");
    }

    private AnimatorState StateFor(int stateHash)
    {
      AnimatorState state;

      if (stateHash == crouchStateHash)
        state = AnimatorState.Crouch;
      else if (stateHash == preparationStateHash)
        state = AnimatorState.Preparation;
      else if (stateHash == throwStateHash)
        state = AnimatorState.Throw;
      else
        state = AnimatorState.Unknown;

      return state;
    }
  }
}
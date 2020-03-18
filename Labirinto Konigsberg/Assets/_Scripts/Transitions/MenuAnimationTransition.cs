using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class MenuAnimationTransition : MonoBehaviour
{
    [SerializeField] private UnityEvent _actions;

    [SerializeField] private Animator _animator;
    [SerializeField] private string _animationName;

    public void CallTransition()
    {
        StartCoroutine(Transition());
    }

    private IEnumerator Transition()
    {
        _animator.Play(_animationName);

        yield return new WaitForSeconds(_animator.GetCurrentAnimatorStateInfo(0).length);

        _actions.Invoke();
    }
}

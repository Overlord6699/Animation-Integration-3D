using UnityEngine;

public class FinishPromtDisplayer : MonoBehaviour
{
    [SerializeField]
    private GameObject _killPrompt;
    [SerializeField]
    private ScriptableValue<KillingTarget> _target;
    [SerializeField]
    private ScriptableValue<bool> _isKilling;

    private void Start()
    {
        ChangePromptVisibility(_target.Value, _isKilling.Value);
    }

    private void OnEnable()
    {
        _target.ValueChangeEvent.AddListener(OnTargetChanged);
        _isKilling.ValueChangeEvent.AddListener(OnCanFinishChanged);
    }

    private void OnDisable()
    {
        _target.ValueChangeEvent.RemoveListener(OnTargetChanged);
        _isKilling.ValueChangeEvent.RemoveListener(OnCanFinishChanged);
    }

    //target change reaction
    private void OnTargetChanged(KillingTarget newTarget)
    {
        ChangePromptVisibility(newTarget, _isKilling.Value);
    }

    //finish ability reaction
    private void OnCanFinishChanged(bool newCanFinish)
    {
        ChangePromptVisibility(_target.Value, newCanFinish);
    }

    //we can see prompt when all requirements were met
    private void ChangePromptVisibility(KillingTarget target, bool isFinishing)
    {
      _killPrompt.SetActive(target != null && !isFinishing);
    }
}

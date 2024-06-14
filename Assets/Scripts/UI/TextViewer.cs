using TMPro;
using UnityEngine;

public abstract class TextViewer<T> : MonoBehaviour where T : MonoBehaviour, ISpawnable
{
    [SerializeField] private Spawner<T> _spawner;

    [SerializeField] private TMP_Text _activeCreated;
    [SerializeField] private TMP_Text _amountCreated;

    protected void OnEnable()
    {
        _spawner.AmountChanged += (value) => OutputValue(_amountCreated, value);
        _spawner.ActiveChanged += (value) => OutputValue(_activeCreated, value);
    }

    protected void OnDisable()
    {
        _spawner.AmountChanged -= (value) => OutputValue(_amountCreated, value);
        _spawner.ActiveChanged -= (value) => OutputValue(_activeCreated, value);
    }

    protected void OutputValue(TMP_Text textField, int value)
    {
        textField.text = value.ToString();
    }
}


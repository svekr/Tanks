using System;
using com.Tanks.TanksBattle.Tank.Movement;
using com.Tanks.TanksBattle.UI.Input;
using UnityEngine;

public class MovementTypeSwitcher : MonoBehaviour {
    public event Action<TankMovementType> OnSwitch;

    private readonly TankMovementType[] _movementTypes = new [] {
        TankMovementType.Classic,
        TankMovementType.Caterpillar
    };

    [SerializeField] private Transform _selector;
    [SerializeField] private UIClickHandler[] _items;

    private int _currentIndex;

    public TankMovementType CurrentType {
        get => _movementTypes[_currentIndex];
        set => SetCurrentIndex(Mathf.Max(Array.IndexOf(_movementTypes,  value), 0));
    }

    private void Start() {
        foreach (var item in _items) {
            item.SetClickHandler(OnItemClick);
        }
    }

    private void OnItemClick(UIClickHandler item) {
        var itemIndex = Mathf.Max(Array.IndexOf(_items, item), 0);
        SetCurrentIndex(itemIndex);
    }

    private void SetCurrentIndex(int index) {
        if (index == _currentIndex) return;
        _currentIndex = index;
        _selector.position = _items[_currentIndex].transform.position;
        OnSwitch?.Invoke(CurrentType);
    }
}
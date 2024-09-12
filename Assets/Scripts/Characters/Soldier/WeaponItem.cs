using UnityEngine;

public class WeaponItem : ItemOnCharacterMb
{
    [SerializeField] private Animator _animator;
    private int _onWeaponEquip = Animator.StringToHash("OnWeaponEquip");

    public override void OnEquip() =>
        _animator.SetTrigger(_onWeaponEquip);
}
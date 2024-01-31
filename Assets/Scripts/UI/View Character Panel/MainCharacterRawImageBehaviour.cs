using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainCharacterRawImageBehaviour : MonoBehaviour, IPointerMoveHandler, IDropHandler
{
    [SerializeField] private Image _greenCircleImage;
    [SerializeField] private GameObject _particlesImage;

    public void Init()
    {
        ItemIcon.OnHoverEnterDragIcon += ItemIcon_OnHoverEnterDragIcon;
        ItemIcon.OnHoverExitDragIcon += ItemIcon_OnHoverExitDragIcon;
        
        ServiceLocator.Instance.CharacterChanger.ShowNewCharacter += OnShowNewCharacter;
        ServiceLocator.Instance.CharacterDresser.CharacterIsFullyEquiped += OnCharacterFullyEquiped;

        _greenCircleImage.gameObject.SetActive(false);

        HideVictoryParticles();
    }

    private void OnCharacterFullyEquiped() =>
        ShowVictoryParticles();

    private void OnShowNewCharacter(CharacterSo obj) =>
        HideVictoryParticles();

    private void ItemIcon_OnHoverExitDragIcon() =>
        _greenCircleImage.gameObject.SetActive(false);

    private void ItemIcon_OnHoverEnterDragIcon() =>
        _greenCircleImage.gameObject.SetActive(true);

    public void OnDrop(PointerEventData eventData) =>
        _greenCircleImage.gameObject.SetActive(false);

    public void OnPointerMove(PointerEventData eventData)
    {
        if (eventData.eligibleForClick && eventData.pointerEnter == this.gameObject)
            ServiceLocator.Instance.InputFromImagesService.SetCharacterInput(eventData.delta);
    }

    public void ShowVictoryParticles() =>
        _particlesImage.SetActive(true);

    public void HideVictoryParticles() =>
        _particlesImage.SetActive(false);

    public void OnDestroy()
    {
        ItemIcon.OnHoverEnterDragIcon -= ItemIcon_OnHoverEnterDragIcon;
        ItemIcon.OnHoverExitDragIcon -= ItemIcon_OnHoverExitDragIcon;
        ServiceLocator.Instance.CharacterChanger.ShowNewCharacter -= OnShowNewCharacter;
        ServiceLocator.Instance.CharacterDresser.CharacterIsFullyEquiped -= OnCharacterFullyEquiped;
    }
}
using UnityEngine;
using UnityEngine.UI;

public abstract class CharacterTopButton : MonoBehaviour
{   
    [SerializeField] private Button _iconButton;
    [SerializeField] private Image _previewImage;
    [SerializeField] private Image _greenBottomImage;

    protected CanvasController _canvasController;
    
    public virtual void Init(CanvasController canvasController) 
    {
        _canvasController = canvasController;
        _iconButton.onClick.AddListener(() => OnIconClick());
        ServiceLocator.Instance.CharacterChanger.ShowNewCharacter += OnCharacterChange;
    }

    public virtual void OnCharacterChange(CharacterSo obj) { }

    public virtual void OnIconClick() { }

    public virtual void ShowActiveState() =>
        _greenBottomImage.gameObject.SetActive(true);

    public virtual void ShowInactiveState() =>
        _greenBottomImage.gameObject.SetActive(false);

    private void OnDestroy() =>
        ServiceLocator.Instance.CharacterChanger.ShowNewCharacter -= OnCharacterChange;
}
using UnityEngine;
using UnityEngine.UI;

public abstract class CharacterTopButton : MonoBehaviour
{   
    [SerializeField] private Button _iconButton;
    [SerializeField] private Image _previewImage;
    [SerializeField] private Image _greenBottomImage;

    public string CharacterId { get; protected set; }

    protected CanvasController CanvasController;
    
    public virtual void Init(CanvasController canvasController) 
    {
        CanvasController = canvasController;
        _iconButton.onClick.AddListener(() => OnIconClick());
        ServiceLocator.Instance.CharacterChanger.ShowNewCharacter += OnCharacterChange;
    }

    public virtual void OnCharacterChange(CharacterSo characterSo) 
    {
        if (CharacterId != characterSo.Id)
            ShowInactiveState();
        else
            ShowActiveState();
    }

    public virtual void OnIconClick() { }

    public virtual void ShowActiveState() =>
        _greenBottomImage.gameObject.SetActive(true);

    public virtual void ShowInactiveState() =>
        _greenBottomImage.gameObject.SetActive(false);

    private void OnDestroy() =>
        ServiceLocator.Instance.CharacterChanger.ShowNewCharacter -= OnCharacterChange;
}
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ViewItemInfoPanel : MonoBehaviour
{
    [SerializeField] private ScrollRect _scrollRect;
    [SerializeField] private Transform _scrollContentTransform;
    [SerializeField] private ContentSizeFitter _contentSizeFitter;
    [SerializeField] private AudioButton _audioButton;
    [SerializeField] private GameObject _lockItemIcon;
    [SerializeField] private Text _itemNameText;
    [SerializeField] private Text _descriptionText;

    private CharacterDresser _characterDresser;
    private GameObject _lockIcon;

    public void Init()
    {
        _audioButton.Init();
        _characterDresser = ServiceLocator.Instance.CharacterDresser;
    }

    public void OnClickOnItem(CharacterItemSo itemSo)
    {
        CleanUpScroll();
        if (!_characterDresser.CanEquipItem(itemSo))
        {
            _lockIcon = Instantiate(_lockItemIcon, _scrollContentTransform);
            _lockIcon.transform.SetAsFirstSibling();
        }

        _itemNameText.text = itemSo.ItemName;
        _descriptionText.text = itemSo.ItemDescription;

        _audioButton.SetContent(itemSo.ItemAudioClip);
        StartCoroutine(ContentFitterHandler());
    }

    private void CleanUpScroll()
    {
        Destroy(_lockIcon);
        _scrollRect.verticalNormalizedPosition = 0;
    }

    private IEnumerator ContentFitterHandler()
    {
        _contentSizeFitter.enabled = false;
        yield return new WaitForEndOfFrame();
        _contentSizeFitter.enabled = true;
    }
}
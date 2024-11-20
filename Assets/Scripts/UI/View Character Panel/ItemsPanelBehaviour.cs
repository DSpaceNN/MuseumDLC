using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;

public class ItemsPanelBehaviour : ItemsPanelBase
{
    [SerializeField] private ScrollView _itemsScroll;
    [SerializeField] private Transform _scrollContentHolder;
    [SerializeField] private ItemIcon _itemIconPrefab;
    [SerializeField] private Button _questionButton;
    [SerializeField] private Button _infoButton;
    [SerializeField] private Text _infoButtonText;

    private Image _infoButtonImage;
    private float _fadeAlpha = 0.4f;

    private CharacterSo _characterSo;
    private List<ItemIcon> _itemIcons = new List<ItemIcon>();

    public override void Init(CanvasController canvasController)
    {
        _canvasController = canvasController;

        _infoButtonImage = _infoButton.gameObject.GetComponent<Image>();

        _questionButton.onClick.AddListener(() => OnQuestionButton());
        _infoButton.onClick.AddListener(() => OnInfoButton());
    }

    public override void ShowIcons(CharacterSo characterSo)
    {
        Tools.DestroyAllChilds(_scrollContentHolder);
        _itemIcons.Clear();

        for (int i = 0; i < characterSo.Items.Length; i++)
            InstantiateIcon(characterSo.Items[i]);

        DeactivateInfoButton();
    }

    public void InstantiateIcon(CharacterItemSo itemSo)
    {
        ItemIcon iconMb = Instantiate(_itemIconPrefab, _scrollContentHolder);
        iconMb.ShowItem(itemSo, _canvasController);
        _itemIcons.Add(iconMb);
    }

    public void ActivateInfoButton()
    {
        _infoButton.enabled = true;
        _infoButtonText.color = Color.white;
        _infoButtonImage.color = Color.white;
    }

    private void DeactivateInfoButton()
    {
        _infoButton.enabled = false;
        Color fadeColor = Color.white;
        fadeColor.a = _fadeAlpha;
        _infoButtonText.color = fadeColor;
        _infoButtonImage.color = fadeColor;
    }
}
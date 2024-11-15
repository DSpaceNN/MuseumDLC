using System;
using UnityEngine;
using UnityEngine.UI;

public class CharacterOnSceneHolder : MonoBehaviour
{
    public event Action<CharacterModelMb> OnInstantiateCharacter;

    [Header("Dependencies")]
    [SerializeField] private Camera _characterCamera;
    [SerializeField] private Image _backgroundImage;

    [Space]
    [Header("Settings")]
    [SerializeField] private Sprite _defaultBackgroundSprite;
    [SerializeField] private Color _backColor;

    private ServiceLocator _serviceLocator;
    private InputFromImagesService _inputService;
    private GameObject _characterModel;
    private CharacterSo _characterSo;

    private float _rotationSpeed = 100f;
    private float _backRotationSpeed = 3f;
    private float _backTransitionCoolDown = 2f;
    private float _timerToBackTransition;

    public void Init()
    {
        _serviceLocator = ServiceLocator.Instance;
        _inputService = _serviceLocator.InputFromImagesService;

        _serviceLocator.CharacterChanger.ShowNewCharacter += ShowCharacter;
        CharacterDresser.CharacterIsFullyEquiped += OnCharacterIsFullyEquiped;
    }

    public void ShowCharacter(CharacterSo characterSo)
    {
        Destroy(_characterModel);
        _characterSo = characterSo;
        _characterModel = Instantiate(characterSo.CharacterPrefab, Vector3.zero, Quaternion.identity, this.transform);
        CharacterModelMb characterMb = _characterModel.GetComponent<CharacterModelMb>();
        HandleCharacterBackgroundOnStart();
        OnInstantiateCharacter?.Invoke(characterMb);
    }

    private void HandleCharacterBackgroundOnStart()
    {
        switch (_serviceLocator.InterfaceType)
        {
            case Enums.InterfaceType.DarkTheme:
                _backgroundImage.sprite = _defaultBackgroundSprite;
                break;

            case Enums.InterfaceType.WhiteTheme:
                _backgroundImage.sprite = null;
                _backgroundImage.color = _backColor;
                break;
        }
    }

    private void Update()
    {
        if (_characterModel != null)
        {
            HandleInputRotation();
            ReturnStartRotation();
        }
    }

    private void HandleInputRotation()
    {
        if (_inputService.CharacterDeltaInput != Vector2.zero)
        {
            _characterModel.transform.Rotate(0, -_inputService.CharacterDeltaInput.x * _rotationSpeed * Time.deltaTime, 0);
            _timerToBackTransition = _backTransitionCoolDown;
        }
    }

    private void ReturnStartRotation()
    {
        _timerToBackTransition -= Time.deltaTime;
        if (_timerToBackTransition < 0 && _characterModel.transform.eulerAngles.y != 0)
        {
            float y = _characterModel.transform.eulerAngles.y;
            float newRot = y >= 180f ? 360f : 0f;

            y = Mathf.Lerp(y, newRot, Time.deltaTime * _backRotationSpeed);
            Vector3 rot = new Vector3(_characterModel.transform.eulerAngles.x, y, _characterModel.transform.eulerAngles.z);

            if (y < 1f)
                rot = Vector3.zero;

            _characterModel.transform.rotation = Quaternion.Euler(rot);
        }
    }

    private void OnCharacterIsFullyEquiped()
    {
        switch (_serviceLocator.InterfaceType)
        {
            case Enums.InterfaceType.DarkTheme:
                _backgroundImage.sprite = _characterSo.CharacterBackground;
                break;

            case Enums.InterfaceType.WhiteTheme:

                break;
        }
    }   

    private void OnDestroy()
    {
        _serviceLocator.CharacterChanger.ShowNewCharacter -= ShowCharacter;
        CharacterDresser.CharacterIsFullyEquiped -= OnCharacterIsFullyEquiped;
    }   
}
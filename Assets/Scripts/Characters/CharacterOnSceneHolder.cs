using System;
using UnityEngine;
using UnityEngine.UI;

public class CharacterOnSceneHolder : MonoBehaviour
{
    public event Action<CharacterModelMb> OnInstantiateCharacter;

    [Header("Dependencies")]
    [SerializeField] private Camera _characterCamera;
    [SerializeField] private Image _backgroundImage;
    [SerializeField] private Transform _charPlace;

    [Space]
    [Header("Settings")]
    [SerializeField] private Sprite _defaultBackgroundSprite;
    [SerializeField] private GameObject _whiteBackGo;

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
        _characterModel = Instantiate(characterSo.CharacterPrefab, Vector3.zero, Quaternion.identity, _charPlace);
        CharacterModelMb characterMb = _characterModel.GetComponent<CharacterModelMb>();
        HandleCharacterBackgroundOnStart();
        OnInstantiateCharacter?.Invoke(characterMb);
    }

    private void HandleCharacterBackgroundOnStart()
    {
        switch (_serviceLocator.InterfaceType)
        {
            case Enums.InterfaceType.DarkTheme:
                _backgroundImage.gameObject.SetActive(true);
                _backgroundImage.sprite = _defaultBackgroundSprite;
                _whiteBackGo.SetActive(false);
                break;

            case Enums.InterfaceType.WhiteTheme:
                _backgroundImage.gameObject.SetActive(false);
                _whiteBackGo.SetActive(true);
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
            _charPlace.Rotate(0, -_inputService.CharacterDeltaInput.x * _rotationSpeed * Time.deltaTime, 0);
            _timerToBackTransition = _backTransitionCoolDown;
        }
    }

    private void ReturnStartRotation()
    {
        _timerToBackTransition -= Time.deltaTime;
        if (_timerToBackTransition < 0 && _charPlace.eulerAngles.y != 0)
        {
            float y = _charPlace.eulerAngles.y;
            float newRot = y >= 180f ? 360f : 0f;

            y = Mathf.Lerp(y, newRot, Time.deltaTime * _backRotationSpeed);
            Vector3 rot = new Vector3(_charPlace.eulerAngles.x, y, _charPlace.eulerAngles.z);

            if (y < 1f)
                rot = Vector3.zero;

            _charPlace.rotation = Quaternion.Euler(rot);
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
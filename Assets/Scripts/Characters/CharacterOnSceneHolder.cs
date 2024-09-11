﻿using System;
using UnityEngine;
using UnityEngine.UI;

public class CharacterOnSceneHolder : MonoBehaviour
{
    public event Action<CharacterModelMb> OnInstantiateCharacter;

    [SerializeField] private Camera _characterCamera;
    [SerializeField] private Image _backgroundImage;
    [SerializeField] private Sprite _defaultBackgroundSprite;

    private InputFromImagesService _inputService;
    private GameObject _characterModel;
    private CharacterSo _characterSo;

    private float _rotationSpeed = 100f;
    private float _backRotationSpeed = 3f;
    private float _backTransitionCoolDown = 2f;
    private float _timerToBackTransition;

    public void Init()
    {
        _inputService = ServiceLocator.Instance.InputFromImagesService;

        ServiceLocator.Instance.CharacterChanger.ShowNewCharacter += ShowCharacter;
        CharacterDresser.CharacterIsFullyEquiped += OnCharacterIsFullyEquiped;
    }

    public void ShowCharacter(CharacterSo characterSo)
    {
        Destroy(_characterModel);
        _characterSo = characterSo;
        _characterModel = Instantiate(characterSo.CharacterPrefab, Vector3.zero, Quaternion.identity, this.transform);
        CharacterModelMb characterMb = _characterModel.GetComponent<CharacterModelMb>();
        _backgroundImage.sprite = _defaultBackgroundSprite;
        OnInstantiateCharacter?.Invoke(characterMb);
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

    private void OnCharacterIsFullyEquiped() =>
        _backgroundImage.sprite = _characterSo.CharacterBackground;

    private void OnDestroy()
    {
        ServiceLocator.Instance.CharacterChanger.ShowNewCharacter -= ShowCharacter;
        CharacterDresser.CharacterIsFullyEquiped -= OnCharacterIsFullyEquiped;
    }   
}
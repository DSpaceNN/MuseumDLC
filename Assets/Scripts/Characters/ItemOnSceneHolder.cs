using System.Collections.Generic;
using UnityEngine;

public class ItemOnSceneHolder : MonoBehaviour
{
    [SerializeField] private Camera _itemCamera;

    private InputFromImagesService _inputService;
    private GameObject _itemModel;

    private float _rotationSpeed = 100f;
    private float _backRotationSpeed = 3f;
    private float _backTransitionCoolDown = 2f;
    private float _timerToBackTransition;

    private Dictionary<string, GameObject> _itemOnScene = new Dictionary<string, GameObject>();

    public void Init() =>
        _inputService = ServiceLocator.Instance.InputFromImagesService;

    public void ShowItem(string itemId)
    {
        foreach (KeyValuePair<string, GameObject> item in _itemOnScene)
        {
            if (item.Key == itemId)
            {
                item.Value.SetActive(true);
                _itemModel = item.Value;
            }
            else
            {
                item.Value.SetActive(false);
            }
        }
    }

    private void Update()
    {
        if (_itemModel != null)
        {
            HandleInputRotation();
            ReturnStartRotation();
        }
    }

    private void OnShowNewCharacter(CharacterSo characterSo)
    {
        if (_itemOnScene.Count > 0)
            CleanItemsOnScene();

        _itemModel = null;

        foreach (CharacterItemSo item in characterSo.Items)
        {
            GameObject itemGo = SpawnItem(item.ItemPrefab);
            itemGo.SetActive(false);
            _itemOnScene.Add(item.Id, itemGo);
        }
    }

    float sizeInMeters = 2f;
    private GameObject SpawnItem(GameObject prefab)
    {
        GameObject itemGo = Instantiate(prefab, this.transform);
        itemGo.transform.localPosition = Vector3.zero;
        itemGo.transform.localRotation = Quaternion.identity;

        BoxCollider collider = itemGo.GetComponentInChildren<BoxCollider>();
        if (collider == null)
            Debug.LogError("На префабе нет коллайдера");

        //это нужно, чтобы объект вписался в кадр
        itemGo.transform.localScale /= collider.size.magnitude;
        itemGo.transform.localScale *= sizeInMeters;
        return itemGo;
    }

    private void CleanItemsOnScene()
    {
        foreach (KeyValuePair<string, GameObject> pair in _itemOnScene)
            Destroy(pair.Value);

        _itemOnScene.Clear();
    }

    private void Start() =>
        ServiceLocator.Instance.CharacterChanger.ShowNewCharacter += OnShowNewCharacter;

    private void OnDestroy() =>
        ServiceLocator.Instance.CharacterChanger.ShowNewCharacter -= OnShowNewCharacter;

    private void ReturnStartRotation()
    {
        _timerToBackTransition -= Time.deltaTime;
        if (_timerToBackTransition < 0 && _itemModel.transform.eulerAngles.y != 0)
        {
            Vector3 rot = _itemModel.transform.rotation.eulerAngles;
            Vector3 newRot = new Vector3(GetСoordinate(rot.x), GetСoordinate(rot.y), GetСoordinate(rot.z));
            _itemModel.transform.rotation = Quaternion.Euler(newRot);
        }
    }

    private void HandleInputRotation()
    {
        if (_inputService.ItemDeltaInput != Vector2.zero)
        {
            _itemModel.transform.RotateAround(_itemModel.transform.position, Vector2.up, -_inputService.ItemDeltaInput.x * _rotationSpeed * Time.deltaTime);
            _itemModel.transform.RotateAround(_itemModel.transform.position, _itemCamera.gameObject.transform.right, _inputService.ItemDeltaInput.y * _rotationSpeed * Time.deltaTime);
            _timerToBackTransition = _backTransitionCoolDown;
        }
    }

    private float GetСoordinate(float x)
    {
        float newRot = x >= 180f ? 360f : 0f;

        x = Mathf.Lerp(x, newRot, Time.deltaTime * _backRotationSpeed);

        if (x < 1f)
            x = 0f;

        return x;
    }
}
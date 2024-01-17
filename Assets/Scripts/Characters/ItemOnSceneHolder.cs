using UnityEngine;

public class ItemOnSceneHolder : MonoBehaviour
{
    [SerializeField] private GameObject _testGo;
    [SerializeField] private Camera _itemCamera;

    private InputFromImagesService _inputService;
    private GameObject _itemModel;

    private float _rotationSpeed = 100f;

    public void Init()
    {
        _inputService = ServiceLocator.Instance.InputFromImagesService;
        _itemModel = _testGo;
    }

    private void Update()
    {
        if (_itemModel != null)
            HandleInputRotation();
    }

    private void HandleInputRotation()
    {
        if (_inputService.ItemDeltaInput != Vector2.zero)
        {
            _itemModel.transform.RotateAround(_itemModel.transform.position, Vector2.up, -_inputService.ItemDeltaInput.x * _rotationSpeed * Time.deltaTime);
            _itemModel.transform.RotateAround(_itemModel.transform.position, _itemCamera.gameObject.transform.right, _inputService.ItemDeltaInput.y * _rotationSpeed * Time.deltaTime);
        }
    }

        //loadedMesh.transform.localScale /= collider.size.magnitude;
        //loadedMesh.transform.localScale *= sizeInMeters *= _deltaSize;
}
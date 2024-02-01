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

    public void Init()
    {
        _inputService = ServiceLocator.Instance.InputFromImagesService;
    }
    
    float sizeInMeters = 2f;
    public void ShowItem(GameObject itemModel)
    {
        if (_itemModel != null)
            Destroy(_itemModel);

        _itemModel = Instantiate(itemModel, this.transform);
        _itemModel.transform.localPosition = Vector3.zero;
        _itemModel.transform.localRotation = Quaternion.identity;

        BoxCollider collider = _itemModel.GetComponentInChildren<BoxCollider>();
        if (collider == null)
            Debug.LogError("Õ‡ ÔÂÙ‡·Â ÌÂÚ ÍÓÎÎ‡È‰Â‡");

        _itemModel.transform.localScale /= collider.size.magnitude;
        _itemModel.transform.localScale *= sizeInMeters;
    }

    private void Update()
    {
        if (_itemModel != null)
        {
            HandleInputRotation();
            ReturnStartRotation();
        }
            
    }

    private void ReturnStartRotation()
    {
        _timerToBackTransition -= Time.deltaTime;
        if (_timerToBackTransition < 0 && _itemModel.transform.eulerAngles.y != 0)
        {
            Vector3 rot = _itemModel.transform.rotation.eulerAngles;
            Vector3 newRot = new Vector3(Get—oordinate(rot.x), Get—oordinate(rot.y), Get—oordinate(rot.z));
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

    private float Get—oordinate(float x)
    {
        float newRot = x >= 180f ? 360f : 0f;

        x = Mathf.Lerp(x, newRot, Time.deltaTime * _backRotationSpeed);

        if (x < 1f)
            x = 0f;

        return x;
    }
}
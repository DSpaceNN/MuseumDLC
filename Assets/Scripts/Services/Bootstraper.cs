using UnityEngine;

public class Bootstraper : MonoBehaviour
{
    [SerializeField] private CanvasController _canvasController;
    [SerializeField] private ServiceLocator _serviceLocator;
    [SerializeField] private CharacterOnSceneHolder _characterHolder;
    [SerializeField] private ItemOnSceneHolder _itemOnSceneHolder;

    private void Start()
    {
        _canvasController.Init();

        _serviceLocator.Init(_canvasController, _characterHolder, _itemOnSceneHolder);
        _serviceLocator.InitServices();

        _characterHolder.Init();
        _itemOnSceneHolder.Init();

        _canvasController.ShowStartPanel();
    }
}
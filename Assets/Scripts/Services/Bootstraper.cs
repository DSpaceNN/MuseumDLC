using UnityEngine;

public class Bootstraper : MonoBehaviour
{
    [SerializeField] private CanvasController _canvasController;
    [SerializeField] private ServiceLocator _serviceLocator;

    private void Start()
    {
        _canvasController.Init();
        _serviceLocator.Init(_canvasController);
        _serviceLocator.InitServices();
    }
}

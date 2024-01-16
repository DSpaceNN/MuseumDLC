using UnityEngine;

public class ServiceLocator : MonoBehaviour
{
    public static ServiceLocator Instance { get; private set; }
    public InputService InputService => _inputService;

    private CanvasController _canvasController;
    
    private InputService _inputService;

    public void Init(CanvasController canvasController)
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
            _canvasController = canvasController;
            return;
        }
        Destroy(this.gameObject);
    }

    public void InitServices()
    {
        _inputService = new InputService();
    }

    private void Update()
    {
        _inputService.Update();
    }
}

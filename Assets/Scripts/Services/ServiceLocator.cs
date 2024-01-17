using UnityEngine;

public class ServiceLocator : MonoBehaviour
{
    public static ServiceLocator Instance { get; private set; }
    public CharactersStorage CharactersStorage => _charactersStorage;
    public InputFromImagesService InputFromImagesService => _inputFromImagesService;
    public CanvasController CanvasController => _canvasController;
    public CharacterOnSceneHolder CharacterOnSceneHolder => _characterHolder;
    public CharacterChanger CharacterChanger => _characterChanger;


    private CanvasController _canvasController;
    //private InputService _inputService;
    private CharactersStorage _charactersStorage;
    private InputFromImagesService _inputFromImagesService;
    private CharacterOnSceneHolder _characterHolder;
    private CharacterChanger _characterChanger;

    public void Init(CanvasController canvasController, CharacterOnSceneHolder characterHolder)
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
            _canvasController = canvasController;
            _characterHolder = characterHolder;
            return;
        }
        Destroy(this.gameObject);
    }

    public void InitServices()
    {
        //_inputService = new InputService();
        _charactersStorage = GetComponent<CharactersStorage>();
        _inputFromImagesService = new InputFromImagesService();
        _characterChanger = new CharacterChanger();
    }

    private void Update()
    {
        _inputFromImagesService.Update();
    }
}

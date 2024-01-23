using UnityEngine;

public class ServiceLocator : MonoBehaviour
{
    public static ServiceLocator Instance { get; private set; }
    public CanvasController CanvasController { get; private set; }
    public CharacterOnSceneHolder CharacterOnSceneHolder { get; private set; }
    public ItemOnSceneHolder ItemOnSceneHolder { get; private set; }

    public CharactersStorage CharactersStorage => _charactersStorage;
    public InputFromImagesService InputFromImagesService => _inputFromImagesService;
    public CharacterChanger CharacterChanger => _characterChanger;
    public CharacterDresser CharacterDresser => _characterDresser;
    public AudioPlayerService AudioPlayerService => _audioPlayer;

    //private InputService _inputService;
    private CharactersStorage _charactersStorage;
    private InputFromImagesService _inputFromImagesService;
    private CharacterChanger _characterChanger;
    private CharacterDresser _characterDresser;
    private AudioPlayerService _audioPlayer;

    public void Init(CanvasController canvasController, CharacterOnSceneHolder characterHolder, ItemOnSceneHolder itemHolder)
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);

            CanvasController = canvasController;
            CharacterOnSceneHolder = characterHolder;
            ItemOnSceneHolder = itemHolder;
            return;
        }
        Destroy(this.gameObject);
    }

    public void InitServices()
    {
        //_inputService = new InputService();
        _charactersStorage = GetComponent<CharactersStorage>();
        _inputFromImagesService = new InputFromImagesService();
        _inputFromImagesService.Init();
        _characterChanger = new CharacterChanger();
        _characterDresser = new CharacterDresser(CharacterOnSceneHolder, CharacterChanger);
        _audioPlayer = GetComponent<AudioPlayerService>();
    }

    private void Update()
    {
        _inputFromImagesService.Update();
    }
}

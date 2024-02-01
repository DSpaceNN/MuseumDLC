using UnityEngine;

public class ServiceLocator : MonoBehaviour
{
    public static ServiceLocator Instance { get; private set; }
    public CanvasController CanvasController { get; private set; }
    public CharacterOnSceneHolder CharacterOnSceneHolder { get; private set; }
    public ItemOnSceneHolder ItemOnSceneHolder { get; private set; }
    public CharactersStorage CharactersStorage { get; private set; }
    public InputFromImagesService InputFromImagesService { get; private set; }
    public CharacterChanger CharacterChanger { get; private set; }
    public CharacterDresser CharacterDresser { get; private set; }
    public AudioPlayerService AudioPlayerService { get; private set; }
    public DefaultPanelSwitcher DefaultPanelSwitcher { get; private set; }

    //private InputService _inputService;

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
        CharactersStorage = GetComponent<CharactersStorage>();
        InputFromImagesService = new InputFromImagesService();
        InputFromImagesService.Init();
        CharacterChanger = new CharacterChanger();
        CharacterDresser = new CharacterDresser(CharacterOnSceneHolder, CharacterChanger);
        DefaultPanelSwitcher = new DefaultPanelSwitcher(CanvasController, CharacterChanger);
        AudioPlayerService = GetComponent<AudioPlayerService>();
        AudioPlayerService.Init(CharacterChanger, DefaultPanelSwitcher);
    }

    public void InitDefaultPanelSwitcher(float durationTime, string defaultCharacterId)
    {
        DefaultPanelSwitcher.Init(durationTime, defaultCharacterId);
    }   

    private void Update()
    {
        InputFromImagesService.Update();
        DefaultPanelSwitcher?.Update();
    }
}
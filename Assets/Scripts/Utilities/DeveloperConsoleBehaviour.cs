using TMPro;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class DeveloperConsoleBehaviour : MonoBehaviour
{
    [SerializeField] private string prefix = string.Empty;
    [SerializeField] private ConsoleCommand[] commands = new ConsoleCommand[0];

    [Header("UI")]
    [SerializeField] private GameObject uiCanvas = null;
    [SerializeField] private TMP_InputField inputField = null;

    private static DeveloperConsoleBehaviour instance;
    private DeveloperConsole developerConsole;
    private float pausedTimeScale;

    private DeveloperConsole DeveloperConsole
    {
        get {
            if (developerConsole != null) { return developerConsole; }
            return developerConsole = new DeveloperConsole(prefix, commands);
        }
    }

    // Ensure a single instance can be used between different levels
    private void Awake()
    {
        if (instance != null & instance != this) {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Switch between UI and Input Field being displayed. Also the game world time being paused or set back to it's previous state (in menu = paused, in game = unpaused)
    public void Toggle(CallbackContext context)
    {
        if (!context.action.triggered) { return; }

        if (uiCanvas.activeSelf) {
            Time.timeScale = pausedTimeScale;
            uiCanvas.SetActive(false);
        }
        else {
            pausedTimeScale = Time.timeScale;
            Time.timeScale = 0;
            uiCanvas.SetActive(true);
            inputField.ActivateInputField();
        }
    }

    // Pass the input text into the developer console getter and clear the input field's text
    public void ProcessCommand(string input)
    {
        DeveloperConsole.ProcessCommandInput(input);
        inputField.text = string.Empty;
    }
}

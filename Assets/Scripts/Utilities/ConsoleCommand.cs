using UnityEngine;

public abstract class ConsoleCommand : ScriptableObject, IConsoleCommand
{
    [SerializeField] private string commandWord = string.Empty;

    public string CommandWord => commandWord;

    public abstract bool Process(string[] args);
}

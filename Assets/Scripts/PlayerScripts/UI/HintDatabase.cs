using System.Collections.Generic;
using UnityEngine;

public static class HintDatabase
{
    private static readonly Dictionary<string, string> _hints = new()
    {
        ["SceneDoor"] = "Нажмите [{0}] чтобы войти",
        ["NPS"] = "Нажмите [{0}] чтобы говорить",
    };

    public static string GetHintMessage(string interactionButton, string hintId)
    {
        if (_hints.TryGetValue(hintId, out string messageTemplate))
        {
            return string.Format(messageTemplate, interactionButton);
        }

        Debug.LogWarning($"Hint with ID '{hintId}' not found!");
        return "null";
    }
}
using UnityEngine;
using TMPro;

public class ConsoleDebug : MonoBehaviour
{
    [SerializeField] private GameObject inputFieldObject;
    private TMP_InputField inputField;

    void Start()
    {
        if (inputFieldObject != null)
        {
            inputField = inputFieldObject.GetComponent<TMP_InputField>();
            inputFieldObject.SetActive(false);
        }
        else
        {
            Debug.LogError("InputFieldObject is not assigned in the inspector.");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Slash))
        {
            if (inputField != null)
            {
                inputFieldObject.SetActive(true);
                inputField.text = "/";
                inputField.ActivateInputField();
                inputField.caretPosition = inputField.text.Length;
            }
        }

        if (inputField != null && inputFieldObject.activeSelf && Input.GetKeyDown(KeyCode.Return))
        {
            string command = inputField.text.TrimStart('/');
            if (!string.IsNullOrEmpty(command))
            {
                ExecuteCommand(command);
            }
            inputField.text = "";
            inputFieldObject.SetActive(false);
        }
    }

    private void ExecuteCommand(string command)
    {
        var method = GetType().GetMethod(command, System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public);
        if (method != null)
        {
            method.Invoke(this, null); // Call the method with no parameters
        }
        else
        {
            Debug.LogWarning($"Command '{command}' not found.");
        }
    }

    // Example command method
    private void TestCommand()
    {
        Debug.Log("TestCommand executed!");
    }

    private void KillAllEnemies()
    {
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var enemy in enemies)
        {
            Destroy(enemy);
        }
        Debug.Log("All enemies have been killed.");
    }
}

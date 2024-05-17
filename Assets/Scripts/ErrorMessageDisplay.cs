using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ErrorMessageDisplay : MonoBehaviour
{
    public static ErrorMessageDisplay instance;
    public GameObject ErrorMessage;
    public TextMeshProUGUI Text;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void DisplayErrorMessage(string message)
    {
        Debug.Log(message + "Error");
        ErrorMessage.SetActive(true);
        Text.text = message;
        StartCoroutine(HideErrorMessage());
    }

    IEnumerator HideErrorMessage()
    {
        yield return new WaitForSeconds(1);
        ErrorMessage.SetActive(false);
    }
}

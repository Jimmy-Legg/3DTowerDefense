using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using static DataManager;

public class DifficultySelection : MonoBehaviour
{
    public GameObject buttonPrefab;
    public Transform listParent;
    public List<string> contentList;

    private DataManager dataManager;
    private MyData data;
    private Button selectedButton;

    void Start()
    {
        dataManager = (DataManager)FindFirstObjectByType(typeof(DataManager));
        data = dataManager.LoadData();

        contentList = data.DifficultyList;

        foreach (string itemContent in contentList)
        {
            GameObject newButton = Instantiate(buttonPrefab, listParent);
            GameObject newTextObject = new GameObject("ButtonText");
            newTextObject.transform.SetParent(newButton.transform, false);
            TextMeshProUGUI buttonText = newTextObject.AddComponent<TextMeshProUGUI>();
            buttonText.text = itemContent;
            buttonText.font = Resources.Load<TMP_FontAsset>("Fonts & Materials/Arial SDF");
            buttonText.color = Color.black;
            buttonText.alignment = TextAlignmentOptions.Center;
            buttonText.fontSize = 16;

            Button buttonComponent = newButton.GetComponent<Button>();
            if (buttonComponent != null)
            {
                buttonComponent.onClick.AddListener(() => OnButtonClick(buttonComponent, itemContent));

                // Check if this button corresponds to the selected difficulty
                if (itemContent == data.DifficultySelected)
                {
                    // Update the selected button reference and change its color
                    selectedButton = buttonComponent;
                    ColorBlock selectedColors = selectedButton.colors;
                    selectedColors.normalColor = Color.green; // Change to your desired color
                    selectedButton.colors = selectedColors;
                }

                // Set button interactability based on whether the difficulty is completed
                buttonComponent.interactable = data.DifficultyCompleted.Contains(itemContent);
            }
            else
            {
                Debug.LogWarning("No Button component found in the button prefab.");
            }
        }
    }

    void OnButtonClick(Button clickedButton, string selectedDifficulty)
    {
        // Deselect the previously selected button
        if (selectedButton != null)
        {
            ColorBlock colors = selectedButton.colors;
            colors.normalColor = Color.white;
            selectedButton.colors = colors;
        }

        // Assign the selected difficulty
        data.DifficultySelected = selectedDifficulty;
        dataManager.SaveData(data);

        // Update the selected button reference and change its color
        selectedButton = clickedButton;
        ColorBlock clickedColors = clickedButton.colors;
        clickedColors.normalColor = Color.green; // Change to your desired color
        clickedButton.colors = clickedColors;
    }
}

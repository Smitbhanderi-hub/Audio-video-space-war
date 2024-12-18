using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MenuManager : MonoBehaviour
{
    public Transform levelContainer;
    public RectTransform menuContainer;
    public float transitionTime = 1f;
    private int screenWidth;

    private void Start()
    {
        IntiLevelButtons();
        screenWidth = Screen.width;
    }

    private void IntiLevelButtons()
    {
        Debug.Log($"Level container has {levelContainer.childCount} children.");
        int i = 0;
        foreach (Transform t in levelContainer)
        {
            Debug.Log($"Processing child: {t.name}");
            Button button = t.GetComponent<Button>();
            if (button != null) // Check if Button exists
            {
                int currentIdx = i;
                button.onClick.AddListener(() => OnLevelSelect(currentIdx));
                i++;
            }
            else
            {
                Debug.LogWarning($"Child {t.name} does not have a Button component.");
            }
        }
    }

    private void ChangeMenu(MenuType menuType)
    {
        Vector3 newPos;
        if(menuType == MenuType.Map1Menu)
        {
            newPos = new Vector3(-1920, 0f, 0f);
        }
        else
        {
            newPos = Vector3.zero;
        }
        
        StopAllCoroutines();
        StartCoroutine(changeMenuAniamtion(newPos));
    }

    private IEnumerator changeMenuAniamtion(Vector3 newPos)
    {
        float elapsed = 0f;
        Vector3 oldPos = menuContainer.anchoredPosition3D;

        while(elapsed <= transitionTime)
        {
            elapsed += Time.deltaTime;
            Vector3 currentPos = Vector3.Lerp(oldPos, newPos, elapsed / transitionTime);
            menuContainer.anchoredPosition3D = currentPos;
            yield return null; 
        }
    }
    private void OnLevelSelect(int idx)
    {
        Debug.Log("We Press the level Button" + idx);
        SceneManager.LoadScene("Level1");
    }
    public void OnPlayButtonClicked()
    {
        Debug.Log("Play Button Clicked");
        ChangeMenu(MenuType.Map1Menu);
    }
    public void OnMainMenuButtonClicked()
    {
        Debug.Log("Clicked Main Button");
        ChangeMenu(MenuType.MainMenu);
    }
    public void OnNextMapButtonClicked()
    {
        Debug.Log("Next Map Clicked");
    }

    private enum MenuType
    {
        MainMenu,
        Map1Menu
    }
}

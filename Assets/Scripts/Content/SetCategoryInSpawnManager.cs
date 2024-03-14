using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetCategoryInSpawnManager : MonoBehaviour
{
    void Start()
    {
        if(!GetComponent<Button>())
            gameObject.AddComponent<Button>();
        GetComponent<Button>().onClick.AddListener(OnClick);
    }
    [SerializeField]
    private string category;
    [SerializeField]
    private string localCategory;
    [SerializeField]
    private SpawnPlatesManager spawnPlatesManager;
    private void OnClick()
    {
        spawnPlatesManager.ChangeCategory(category, localCategory);
        spawnPlatesManager.gameObject.SetActive(true);
    }
}

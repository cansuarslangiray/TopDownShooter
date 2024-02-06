using UnityEngine;
using UnityEngine.UI;


public class ImageController : MonoBehaviour
{
    void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        gameObject.SetActive(false);
    }
}
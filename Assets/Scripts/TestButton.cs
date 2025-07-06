using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class TestButton : MonoBehaviour
{
    private Button button;
    private AudioSource audioSource;

    void Start()
    {
        button = GetComponent<Button>();
        audioSource = GetComponent<AudioSource>();
        button.onClick.AddListener(LoadScene);
    }
    private void OnButtonClick()
    {
        audioSource.PlayOneShot(audioSource.clip);
        LoadScene();
    }

    private void LoadScene()
    {
        SceneManager.LoadScene(1);
    }
}

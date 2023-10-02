using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Scene_Change : MonoBehaviour
{
    public int scene_index = 0;
    private Button button;
    
    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(loadScene);
    }
    private void loadScene()
    {
        SceneManager.LoadScene(scene_index);
    }
}

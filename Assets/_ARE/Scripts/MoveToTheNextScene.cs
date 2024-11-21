using UnityEngine.SceneManagement;
using UnityEngine;

public class MoveToTheNextScene : MonoBehaviour
{
    public float changeTime;
    public string nextScene;

    // Update is called once per frame
    void Update()
    {
        changeTime -= Time.deltaTime;

        if (changeTime <= 0)
            SceneManager.LoadScene(nextScene);
    }
}

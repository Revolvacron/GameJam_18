using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;
using UnityEngine.SceneManagement;

public class ReloadScene : MonoBehaviour
{
public List<GameObject> activePlayers = new List<GameObject>();
public 
// Start is called before the first frame update
void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        InputDevice activeController = InputManager.ActiveDevice;
        Debug.Log(activePlayers.Count);
        Scene thisScene = SceneManager.GetActiveScene();
        activePlayers.RemoveAll(x => x == null);
        if (activePlayers.Count == 0 && activeController.CommandIsPressed == true)
        {
            SceneManager.LoadScene(thisScene.name);
        }
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;


public class Example : MonoBehaviour
{
    public CastleSerialized globalCastle = new CastleSerialized();

    int counter = 0;
    void OnGUI()
    {
        //This displays a Button on the screen at position (20,30), width 150 and height 50. The button’s text reads the last parameter. Press this for the SceneManager to load the Scene.
        // if (GUI.Button(new Rect(20, 30, 300, 80), "Other Scene Single"))
        // {
        //     Instantiate(GameObject.Find("House"), new Vector3(1, 1, 0), Quaternion.identity);

        //     //The SceneManager loads your new Scene as a single Scene (not overlapping). This is Single mode.
        //     SceneManager.LoadScene("CastleBuilding", LoadSceneMode.Single);
        //     print("to be implemented");
        // }

        //Whereas pressing this Button loads the Additive Scene.
        if (GUI.Button(new Rect(20, 100, 300, 80), "Other Scene Additive"))
        {
            Instantiate(GameObject.Find("House"), new Vector3(-1, -1, 0), Quaternion.identity);

            
            // 
            if (counter > 0)
            {
                // GameObject.Find("castle").SetActive(true);
                print("\n\n\nHEALTH = " + globalCastle.health + "\n\n\n");
                for (int i = 0; i < globalCastle.sprites.Count; i++)
                {
                    print(globalCastle.positions[i].x);
                }
            }
            counter++;
// 
            

            buildCastle();
            

            //SceneManager loads your new Scene as an extra Scene (overlapping the other). This is Additive mode.
            // SceneManager.LoadScene("CastleBuilding", LoadSceneMode.Additive);
        }
    }


    void buildCastle() 
    {
        foreach (GameObject gameObject in SceneManager.GetSceneByName("Main").GetRootGameObjects())
        {
            // print(gameObject.name);
            if (gameObject.name != "House"){
                gameObject.SetActive(false);
            }
            
            
        }
        foreach (GameObject gameObject in SceneManager.GetSceneByName("CastleBuilding").GetRootGameObjects())
        {
            gameObject.SetActive(true);
        }



        
        GameObject.Find("castle").GetComponent<CastleBuilder>().Init(new Vector2());
    }
}
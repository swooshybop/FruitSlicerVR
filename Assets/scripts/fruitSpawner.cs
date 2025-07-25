using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class fruitSpawner : MonoBehaviour
{
    public static fruitSpawner Instance;

    void Awake()
    {
        Instance = this;
    }

    public GameObject[] FruitPrefabs;
    public int TotalScore;

    public TMPro.TMP_Text ScoreVal;
    public TMPro.TMP_Text TimeVal;

    private float timeLeft;
    private float TotalTime = 5f;
    private float StartTime;

    [SerializeField] private GameObject restartOptionPrefab;
    [SerializeField] private GameObject quitOptionPrefab;

    [SerializeField] Transform MenuAnchor;
    [SerializeField] Transform ScoreboardAnchor;

    bool roundRunning;
    Coroutine spawnLoop;

    // Start is called before the first frame update
    void Start()
    {
        ShowMenu();
        HideScoreboard();
        roundRunning = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!roundRunning)
        {
            return;
        }
        timeLeft = Mathf.Max(0f, TotalTime - (Time.time - StartTime)); //countdown from total time, and returns the larger of 2 values (so never goes below 0)
        TimeVal.text = FormatTime(timeLeft);

        if (timeLeft <= 0f)
        {
            EndRound();
        }
    }

    public void BeginRound()
    {
        ClearMenu();
        ShowScoreboard();

        TotalScore = 0;
        ScoreVal.text = "0";

        StartTime = Time.time;
        roundRunning = true;
        spawnLoop = StartCoroutine(SpawnFruits());
    }

    void EndRound()
    {
        roundRunning = false;

        if(spawnLoop != null)
        {
            StopCoroutine(spawnLoop);
        }

        ShowMenu();
        HideScoreboard();
    }

    public void RestartRound()
    {
        EndRound();
        BeginRound();
    }

    void ShowMenu()
    {
        Vector3 center = MenuAnchor.position;
        Quaternion rot = MenuAnchor.rotation;
        float gap = 0.5f;

        Instantiate(restartOptionPrefab, center - MenuAnchor.right * gap, rot);
        Instantiate(quitOptionPrefab, center + MenuAnchor.right * gap, rot);
    }

    public void ClearMenu()
    {
        foreach (var m in GameObject.FindGameObjectsWithTag("MenuOption"))
        {
            Destroy(m);
        }     
    }

    void ShowScoreboard()
    {
        ScoreVal.transform.parent.gameObject.SetActive(true);
    }

    void HideScoreboard()
    {
        ScoreVal.transform.parent.gameObject.SetActive(false);
    }

    public void RestartGame()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);//reload current scene

        TotalScore = 0;
        ScoreVal.text = "0";
        StartTime = Time.time;

        StartSpawnFruits();

        //Debug.Log("RestartGame CALLED");
        //Debug.Log($"Time now: {Time.time}");

        //SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    //convert the seconds to format (mm:ss)
    private string FormatTime(float time)
    {
        int wholeMinutes = Mathf.FloorToInt(time / 60);
        int secondsLeft = Mathf.FloorToInt(time % 60);
        return $"{wholeMinutes:00}:{secondsLeft:00}";
    }


    public void ClearMenuOptions()
    {
        foreach (var m in GameObject.FindGameObjectsWithTag("MenuOption"))
            Destroy(m);
    }

    private void StartSpawnFruits()
    {
        StartCoroutine(SpawnFruits());
    }
    
    IEnumerator SpawnFruits()
    {
        while (roundRunning)
        {
            GameObject Fruit = Instantiate(FruitPrefabs[Random.Range(0, FruitPrefabs.Length)]);
            Rigidbody rigidbody = Fruit.GetComponent<Rigidbody>();

            rigidbody.velocity = new Vector3(0, 5f, 0);
            rigidbody.angularVelocity = new Vector3( Random.Range(-5f, 5f) , 0f , Random.Range(-5f, 5f) );
            rigidbody.useGravity = true;

            Vector3 position = transform.position + transform.right * Random.Range(-1f, 1f);
            Fruit.transform.position = position;

            yield return new WaitForSeconds(3f);
            //The yield keyword tells the compiler that the method in which it appears is an iterator block.
            //An iterator block, or method, returns an IEnumerable as the result. And the yield keyword is used to return the values for the IEnumerable.

            

        }
    }

    public void scoreGame(int Score)
    {
        TotalScore += Score;

        ScoreVal.text = TotalScore.ToString();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;//この行を追記
using UnityEngine.UIElements;

public class Gmanager : MonoBehaviour
{

    public static Gmanager instance = null;

    public Dictionary<string, KeyCode> key_config = new Dictionary<string, KeyCode>();

    private GameObject gameOverCanvas;

    private GameObject gameClearCanvas;

    private GameObject player;
    private Vector2 playerStartPos;

    private Dictionary<string, GameObject> enemies;
    private Dictionary<string, Vector2> enemyStartPos;

    private GameObject[] grounds;

    private GameObject maincamera;
    private Vector2 cameraStartPos;

    public static string test = "nowalive";
    
    //HPの最大値
    public static int maxHP;

    private int scene;
    public int getScene() { return scene; }

    //スコア
    private int score;
    public int getTotalScore() { return score; }

    //シーン毎のスコア
    private int sceneScore;
    public int getSceneScore() { return sceneScore; }
    public void addSceneScore(int s) { sceneScore += s; }

    //クリア時間
    private float clearTime;
    public float getClearTime() { return clearTime; }

    //時間変位
    private float dt;

    //init完了か
    public bool inited;

    

    private void keySet()
    {
        key_config.Add("forward", KeyCode.RightArrow);
        key_config.Add("back", KeyCode.LeftArrow);
        key_config.Add("up", KeyCode.UpArrow);
        key_config.Add("dash", KeyCode.D);
        key_config.Add("attack", KeyCode.F);
    }

    private void Awake()
    {
        if (instance == null)
        {
            keySet();

            score = 0;
            scene = SceneManager.GetActiveScene().buildIndex;
            sceneScore = 0;

            inited = false;

            instance = this;
            DontDestroyOnLoad(this.gameObject);
            if (scene != 4 && scene != 0) { instance.initGame(); }
        }
        else
        {
            Debug.Log("scene:" + instance.getScene());
            if (instance.getScene() != 4 && instance.getScene() != 0) { instance.initGame(); }
            Debug.Log("inited:" + instance.inited.ToString());
            Destroy(this.gameObject);
        }
    }

    public void initGame()
    {
        sceneScore = 0;
        GameObject[] plr = GameObject.FindGameObjectsWithTag("Player");

        foreach(GameObject p in plr)
        {
            if (p.transform.root.gameObject.name == p.name)
            {
                player = p;
                playerStartPos = player.transform.position;
            }
        }

       

        enemies = new Dictionary<string, GameObject>();
        enemyStartPos = new Dictionary<string, Vector2>();

        GameObject[] es = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject go in es)
        {
            if(go.transform.root.gameObject.name == go.name) {
                enemies.Add(go.name, go);
                enemyStartPos.Add(go.name, go.transform.position);
                go.SetActive(false);
            }
        }

        maincamera = GameObject.FindGameObjectWithTag("MainCamera");
        cameraStartPos = maincamera.transform.position;

        gameClearCanvas = GameObject.FindGameObjectWithTag("GameClear");
        gameOverCanvas = GameObject.FindGameObjectWithTag("GameOver");
        gameClearCanvas.SetActive(false);
        gameOverCanvas.SetActive(false);

        dt = 0;
        clearTime = 0f;

        inited = true;
        //player.GetComponent<PlayerStatus>().SetHP(1000);
        PlayerStatus status = player.GetComponent<PlayerStatus>();
        Debug.Log(player.name);
        //status.HitPoint = 1000;
        maxHP = status.GetHP();
}

    public GameObject getPlayer() { return player; }

    public int getHP() { return player.GetComponent<PlayerStatus>().GetHP(); }
    //速度取得
    public Vector2 getPlayerVelocity() { return player.GetComponent<Mover>().get_vel(); }
    //位置取得
    public Vector2 getPlayerPos() { return player.transform.position; }

    public GameObject getEnemy(string name) { return enemies[name]; }

    public void removeEnemy(string name) {
        sceneScore += enemies[name].GetComponent<EnemyStatus>().getScore();
        enemies.Remove(name);
        enemyStartPos.Remove(name);
    }

    public void EnemyCheck()
    {
        Vector2 rightTop = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        Vector2 leftBottom = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));

        foreach (KeyValuePair<string, Vector2> pair in enemyStartPos)
        {
            
            bool inwidth = pair.Value.x < rightTop.x + 5 && pair.Value.x > leftBottom.x - 5;
            bool inheight = pair.Value.y < rightTop.y + 5 && pair.Value.y > leftBottom.y - 5;

            bool incameraWidth = enemies[pair.Key].transform.position.x < rightTop.x + 2 
                && enemies[pair.Key].transform.position.x > leftBottom.x - 2;
            bool incameraHeight = enemies[pair.Key].transform.position.y < rightTop.y + 2 
                && enemies[pair.Key].transform.position.y > leftBottom.y - 2;

            if (incameraWidth && incameraHeight)
            {
                Debug.Log(pair.Key);
                if (!enemies[pair.Key].activeSelf)
                {
                    enemies[pair.Key].GetComponent<Mover>().collide();
                    enemies[pair.Key].SetActive(true);
                }
            }
            else
            {
                if (enemies[pair.Key].activeSelf && !(inwidth && inheight))
                {
                    enemies[pair.Key].SetActive(false);
                    enemies[pair.Key].GetComponent<Mover>().setPos(pair.Value);
                }
            }
        }
    }

    public GameObject getCamera() { return maincamera; }
    //カメラの位置
    public Vector2 getCameraPos() { return maincamera.transform.position; }

    public void GameOver()
    {
        gameOverCanvas.SetActive(true);
        player.SetActive(false);
        Debug.Log("gover");
    }

    public void GameClear()
    {
        gameClearCanvas.SetActive(true);
        player.SetActive(false);
        sceneScore += (int)((300 - clearTime) * 7.5f);
    }

    public void Retry()
    {
        inited = false;
        SceneManager.LoadScene(scene);
    }

    public void nextStage()
    {
        scene++;
        Debug.Log("changed");
        score += sceneScore;
        Retry();
    }

    public void ReturnTitle()
    {
        SceneManager.LoadScene(0);
        Destroy(this.gameObject);
    }

    public void goTutorial()
    {
        scene = 5;
        Retry();
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        if(inited)
        {
            dt = Time.deltaTime;
            if (player.activeSelf) { clearTime += dt; }
            EnemyCheck();
        }

    }
}

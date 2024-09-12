using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.SearchService;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int level;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private float minimumSpawnTime;
    [SerializeField] private float maximumSpawnTime;
    [SerializeField] private float timeUntilSpawn;
    [SerializeField] private float time;
    [SerializeField] private bool stormNotified;
    [SerializeField] private bool isAllEnemyDead;
    [SerializeField] private EnemySpawnManager enemySpawnManager;
    [SerializeField] private MeteorSpawner meteorSpawnManager;
    [SerializeField] private int mobCount;
    [SerializeField] private bool isAllEnemyDead;


    private void LevelManagerInit()
    {
        enemySpawnManager = this.GetComponent<EnemySpawnManager>();
        meteorSpawnManager = this.GetComponent<MeteorSpawner>();
        isAllEnemyDead = false;
    }

    private void SetTimer()
    {
        time = 30f;
        minimumSpawnTime = time;
        maximumSpawnTime = minimumSpawnTime + 30f;
        timeUntilSpawn = Random.Range(minimumSpawnTime, maximumSpawnTime);
        stormNotified = false;
    }

    private void TimerTick()
    {
        time -= Time.deltaTime;
        timeUntilSpawn -= Time.deltaTime;   
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void SpawnEnemies(int level)
    {
        if(level % 15 == 0)
            enemySpawnManager.SpawnEnemies(mobCount, true, true);
        else if(level % 10 == 0)
            enemySpawnManager.SpawnEnemies(mobCount, false, true);
        else if (level % 5 == 0)
            enemySpawnManager.SpawnEnemies(mobCount, true, false);
        else
            enemySpawnManager.SpawnEnemies(mobCount, false, false);

        mobCount += 2;
    }

    private void SpawnMeteors(int level)
    {
        meteorSpawnManager.Meteor(level);
    }

    private void DestroyMeteors()
    {
        Debug.Log("Meteor Destroyed");
    }

    private void TeleportPlayer()
    {
        Debug.Log("Player Teleported");
    }

    private bool IsFiveSecBeforeStorm()
    {
        if(timeUntilSpawn <= 5f)
            return true;

        return false;
    }

    private bool HasStromAproached()
    {
        if(timeUntilSpawn <= 0f)
            return true;

        return false;
    }

    private bool isEnemyExist()
    {
        return (GameObject.FindGameObjectWithTag("Enemy") != null);
    }

    private void Start()
    {
        SetTimer();
        LevelManagerInit();
        SpawnMeteors(level);
    }
    private void Update()
    {
        if(timeUntilSpawn >= 0)
            TimerTick();

        if(IsFiveSecBeforeStorm() && !stormNotified)
        {
            Debug.Log("Storm is coming");
            stormNotified = true;
        }

        if(HasStromAproached())
        {
            SpawnEnemies(level);
        }

        //TOOD sýçýyo düzelt. Çok geliyo
        if (!isEnemyExist())
        {
            TeleportPlayer();
            DestroyMeteors();
            level++;
            SetTimer();
            SpawnMeteors(level);
        }
    }
}

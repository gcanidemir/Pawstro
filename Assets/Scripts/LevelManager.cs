using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine.Rendering.PostProcessing;
using NavMeshPlus.Components;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int level;
    [SerializeField] private TextMeshProUGUI timerText;
    private float minimumSpawnTime;
    private float maximumSpawnTime;
    private float timeUntilSpawn;
    private float time;
    private bool stormNotified;
    [SerializeField] private EnemySpawnManager enemySpawnManager;
    [SerializeField] private MeteorSpawner meteorSpawnManager;
    [SerializeField] private int mobCount;
    private bool spawnEnemies;
    private bool screenFlash;
    [SerializeField] private PostProcessVolume volume;
    private Vignette vignette;
    private float intensity;
    [SerializeField] private TextMeshProUGUI enemyAlertText;
    [SerializeField] private NavMeshSurface navMeshSurface;

    private void LevelManagerInit()
    {
        enemySpawnManager = this.GetComponent<EnemySpawnManager>();
        meteorSpawnManager = this.GetComponent<MeteorSpawner>();
        enemyAlertText.color = new Color(255, 255, 255, 0);

        volume.profile.TryGetSettings<Vignette>(out vignette);
        vignette.enabled.Override(false);
    }

    private void SetTimer()
    {
        time = 10f;
        minimumSpawnTime = time;
        maximumSpawnTime = minimumSpawnTime + 10f;
        timeUntilSpawn = Random.Range(minimumSpawnTime, maximumSpawnTime);
        stormNotified = false;
    }

    private void TimerTick()
    {
        if (time - Time.deltaTime <= 0.1)
            time = 0;
        else
            time -= Time.deltaTime;

        timeUntilSpawn -= Time.deltaTime;
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void SpawnEnemies(int level)
    {
        if (level % 15 == 0)
            enemySpawnManager.SpawnEnemies(mobCount, true, true);
        else if (level % 10 == 0)
            enemySpawnManager.SpawnEnemies(mobCount, false, true);
        else if (level % 5 == 0)
            enemySpawnManager.SpawnEnemies(mobCount, true, false);
        else
            enemySpawnManager.SpawnEnemies(mobCount, false, false);

        mobCount += 2;
    }

    private IEnumerator SpawnMeteors(int level)
    {
        meteorSpawnManager.Meteor(level);
        yield return null;
        navMeshSurface.BuildNavMesh();
    }

    private void DestroyMeteors()
    {
        //TODO change with tag later.
        GameObject[] temp = GameObject.FindGameObjectsWithTag("Meteor");

        foreach (var item in temp)
        {
            Destroy(item);
            item.SetActive(false);
        }
    }

    private void TeleportPlayer()
    {
        GameObject.FindGameObjectWithTag("Player").transform.position = new Vector3(0, 0, 0);
    }

    private bool IsFiveSecBeforeStorm()
    {
        if (timeUntilSpawn <= 5f)
            return true;

        return false;
    }

    private bool HasStromAproached()
    {
        if (timeUntilSpawn <= 0f)
            return true;

        return false;
    }

    private IEnumerator FlashScreen()
    {
        screenFlash = false;
        intensity = 0f;

        vignette.enabled.Override(true);
        vignette.intensity.Override(intensity);

        for (int i = 0; i < 5; i++)
        {
            while (intensity < 0.3f)
            {
                enemyAlertText.color = new Color(255, 255, 255, enemyAlertText.color.a + 0.17f);

                intensity += 0.01f;

                if (intensity > 0.3f)
                    intensity = 0.3f;

                vignette.intensity.Override(intensity);

                yield return new WaitForSeconds(0.02f);
            }

            yield return new WaitForSeconds(0.1f);

            while (intensity > 0)
            {
                enemyAlertText.color = new Color(255, 255, 255, enemyAlertText.color.a - 0.17f);

                intensity -= 0.01f;

                if (intensity < 0)
                    intensity = 0;

                vignette.intensity.Override(intensity);


                yield return new WaitForSeconds(0.02f);
            }
        }

        vignette.enabled.Override(false);

        yield break;
    }

    private bool isEnemyExist()
    {
        return (GameObject.FindGameObjectWithTag("Enemy") != null);
    }

    private void Start()
    {
        SetTimer();
        LevelManagerInit();
        StartCoroutine(SpawnMeteors(level));
    }
    private void Update()
    {
        if (timeUntilSpawn >= 0)
            TimerTick();

        if (IsFiveSecBeforeStorm() && !stormNotified)
        {
            stormNotified = true;
            spawnEnemies = true;
            screenFlash = true;
        }

        if (screenFlash && intensity <= 0.01)
        {
            StartCoroutine(FlashScreen());
        }

        if (HasStromAproached() && !isEnemyExist() && spawnEnemies)
        {
            SpawnEnemies(level);
            spawnEnemies = false;
            screenFlash = false;
        }

        if (!isEnemyExist() && timeUntilSpawn <= 0)
        {
            TeleportPlayer();
            DestroyMeteors();
            level++;
            SetTimer();
            StartCoroutine(SpawnMeteors(level));
        }
    }
}

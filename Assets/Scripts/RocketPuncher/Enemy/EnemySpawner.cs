// Author(s): Paul Calande
// Class for spawning enemies in Rocket Puncher.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    [Tooltip("File containing enemy data.")]
    TextAsset enemiesFile;
    [SerializeField]
    [Tooltip("File containing enemy spawner data.")]
    TextAsset enemySpawnersFile;
    [SerializeField]
    [Tooltip("File containing item data.")]
    TextAsset itemsFile;
    [SerializeField]
    [Tooltip("File to read score tuning from.")]
    TextAsset scoreFile;
    [SerializeField]
    [Tooltip("Reference to the Score instance.")]
    RPScore score;
    [SerializeField]
    [Tooltip("Reference to the PlayerPowerup instance.")]
    RPPlayerPowerup playerPowerup;
    [SerializeField]
    [Tooltip("The enemy prefab.")]
    GameObject prefabEnemy;
    [SerializeField]
    [Tooltip("The enemy spawn locations.")]
    GameObject[] spawns;
    [SerializeField]
    TimeScale timeScale;

    // The current maximum challenge value for the spawn groups.
    float challengeMax;
    // How much the challenge value increases every time an enemy spawn group finishes.
    float challengeIncreasePerSpawnGroup;
    // How many spawn groups are required to begin the dead time.
    int spawnGroupsPerDeadTime;
    // How many seconds must pass between each spawn.
    float secondsBetweenSpawns;
    // How many seconds must pass between each spawn group (not during dead time).
    float secondsBetweenSpawnGroups;
    // How many seconds must pass during dead time.
    float secondsPerDeadTime;
    // How quickly all of the enemies move to the left.
    // Also affects how quickly enemy projectiles move to the left.
    // This creates the illusion that the player is moving to the right.
    float enemyBaseLeftMovementSpeed;

    Timer timerSpawn;
    Timer timerSpawnGroup;
    Timer timerDeadTime;

    // A collection of all the possible enemies that can spawn.
    List<Enemy.Data> possibleEnemies = new List<Enemy.Data>();
    // How many spawn groups have occurred since the last dead time.
    int spawnGroupsSinceLastDeadTime = 0;
    // The current amount of challenge accumulated in this spawn group.
    float challengeCurrent = 0.0f;
    // The current spawn position to use to spawn new enemies.
    Vector3 spawnPos;
    // Whether the current spawn group has finished or not.
    bool spawnGroupActive = true;

    private void Awake()
    {
        Tune();
    }

    // Read data from the files.
    private void Tune()
    {
        JSONNodeReader reader = new JSONNodeReader(enemySpawnersFile);
        challengeMax = reader.Get("initial challenge", 1.0f);
        challengeIncreasePerSpawnGroup = reader.Get("challenge increase per spawn group", 1.0f);
        spawnGroupsPerDeadTime = reader.Get("spawn groups per dead time", 3);
        secondsBetweenSpawns = reader.Get("seconds between spawns", 1.0f);
        secondsBetweenSpawnGroups = reader.Get("seconds between spawn groups", 2.0f);
        secondsPerDeadTime = reader.Get("seconds per dead time", 3.0f);
        enemyBaseLeftMovementSpeed = reader.Get("enemy base left movement speed", 0.05f);

        timerSpawn = new Timer(secondsBetweenSpawns, TimerSpawnFinished);
        timerSpawnGroup = new Timer(secondsBetweenSpawnGroups, TimerSpawnGroupFinished);
        timerDeadTime = new Timer(secondsPerDeadTime, TimerDeadTimeFinished);
        timerSpawn.Run();
        timerSpawnGroup.Run();
        timerDeadTime.Run();
        ChooseRandomSpawnPosition();

        // How much health a single health kit should give.
        int healthPerHealthKit;
        int pointsPerEnemyKilled;
        int pointsPerProjectilePunched;
        int pointsPerFullHealthHealthKit;

        reader.SetFile(scoreFile);
        pointsPerEnemyKilled = reader.Get("points per enemy killed", 100);
        pointsPerProjectilePunched = reader.Get("points per projectile punched", 10);
        pointsPerFullHealthHealthKit = reader.Get("points per full health health kit", 100);

        // Drop rates for items.
        Probability<ItemType> probItem = new Probability<ItemType>(ItemType.None);
        float dropRateHealthKit;
        float dropRateBattleAxe;
        float dropRateMoreArms;

        reader.SetFile(itemsFile);
        dropRateHealthKit = reader.Get("health kit drop rate", 0.07f);
        dropRateBattleAxe = reader.Get("battle axe drop rate", 0.05f);
        dropRateMoreArms = reader.Get("more arms drop rate", 0.05f);
        healthPerHealthKit = reader.Get("health per health kit", 50);

        probItem.SetChance(ItemType.HealthKit, dropRateHealthKit);
        probItem.SetChance(ItemType.BattleAxe, dropRateBattleAxe);
        probItem.SetChance(ItemType.MoreArms, dropRateMoreArms);

        reader.SetFile(enemiesFile);
        JSONArrayReader enemyArray = reader.Get<JSONArrayReader>("enemies");
        //for (int i = 0; i < enemyArray.GetCount(); ++i)
        JSONNodeReader enemyNode;
        while (enemyArray.GetNextNode(out enemyNode))
        {
            //JSONNodeReader enemyNode = enemyArray.GetNode(i);
            string enemyName = enemyNode.Get("name", "UNNAMED");

            // Read volley data.
            JSONNodeReader volleyNode = enemyNode.Get<JSONNodeReader>("volley");

            string colString = volleyNode.Get("color", "#ffffff");
            Color projColor;
            if (!ColorUtility.TryParseHtmlString(colString, out projColor))
            {
                Debug.Log(enemyName + ": Could not parse HTML color for volley!");
            }
            EnemyProjectile.Data projectile = new EnemyProjectile.Data(
                new EnemyProjectile.Data.Refs(score),
                volleyNode.Get("projectile punchable", true),
                volleyNode.Get("projectile damage", 20),
                pointsPerProjectilePunched,
                projColor,
                volleyNode.Get("direction", 180.0f),
                volleyNode.Get("speed", 4.0f));

            VolleyData volley = new VolleyData(projectile,
                volleyNode.Get("projectile count", 1),
                volleyNode.Get("spread angle", 0.0f),
                volleyNode.Get("aims at player", false));

            OscillatePosition2D.Data oscData = new OscillatePosition2D.Data(
                0.0f, 0.0f,
                enemyNode.Get("y oscillation magnitude", 0.0f),
                enemyNode.Get("y oscillation speed", 0.0f));

            EnemyAttack.Data attack = new EnemyAttack.Data(
                new EnemyAttack.Data.Refs(playerPowerup.gameObject),
                volley,
                enemyNode.Get("seconds between volleys", 1.0f),
                enemyNode.Get("volley direction delta per shot", 0.0f),
                enemyBaseLeftMovementSpeed);

            EnemySprite.Data enemySprite = new EnemySprite.Data(
                enemyNode.Get("sprite name", "basic"));

            Vector2 leftMovement = new Vector2(
                -enemyNode.Get("left movement speed increase", 0.0f)
                - enemyBaseLeftMovementSpeed, 0.0f);

            ItemHealthKit.Data healthKitData = new ItemHealthKit.Data(
                new ItemHealthKit.Data.Refs(score),
                healthPerHealthKit,
                pointsPerFullHealthHealthKit);
            EnemyHealth.Data enemyHealthData = new EnemyHealth.Data(
                new EnemyHealth.Data.Refs(score, playerPowerup),
                healthKitData,
                pointsPerEnemyKilled,
                probItem);

            Enemy.Data enemy = new Enemy.Data(enemyNode.Get("challenge", 1.0f),
                oscData,
                attack,
                enemySprite,
                leftMovement,
                enemyHealthData);

            // Add the enemy to the possible enemies pool.
            possibleEnemies.Add(enemy);
        }
    }

    private void TimerSpawnFinished(float secondsOverflow)
    {
        ChooseRandomSpawnPosition();
        SpawnEnemy();
    }

    private void TimerSpawnGroupFinished(float secondsOverflow)
    {
        challengeCurrent = 0.0f;
        challengeMax += challengeIncreasePerSpawnGroup;
        spawnGroupActive = true;
        spawnGroupsSinceLastDeadTime += 1;
    }

    private void TimerDeadTimeFinished(float secondsOverflow)
    {
        spawnGroupsSinceLastDeadTime = 0;
    }

    private void FixedUpdate()
    {
        if (spawnGroupsSinceLastDeadTime < spawnGroupsPerDeadTime)
        {
            if (spawnGroupActive)
            {
                timerSpawn.Tick(timeScale.DeltaTime());
            }
            else
            {
                timerSpawnGroup.Tick(timeScale.DeltaTime());
            }
        }
        else
        {
            timerDeadTime.Tick(timeScale.DeltaTime());
        }
    }

    // Choose a viable enemy based on the current level of challenge and spawn it.
    private void SpawnEnemy()
    {
        // Filter out all enemies that have too high of a challenge.
        List<Enemy.Data> viableEnemies = GetViableEnemies();

        // Choose one of these enemies randomly.
        Enemy.Data enemy = viableEnemies[Random.Range(0, viableEnemies.Count)];
        challengeCurrent += enemy.challenge;

        // Instantiate the enemy.
        GameObject newEnemy = Instantiate(prefabEnemy, spawnPos, Quaternion.identity);
        Enemy en = newEnemy.GetComponent<Enemy>();
        en.SetData(enemy.DeepCopy());
        newEnemy.GetComponent<TimeScale>().SetData(timeScale);

        // Check if there are any viable enemies left.
        // If not, it's time to move on to the next spawn group.
        viableEnemies = GetViableEnemies();
        spawnGroupActive = (viableEnemies.Count != 0);
    }

    private List<Enemy.Data> GetViableEnemies()
    {
        float challengeRemaining = challengeMax - challengeCurrent;
        return possibleEnemies.FindAll(x => x.challenge <= challengeRemaining);
    }

    private Vector3 GetRandomSpawnPosition()
    {
        return spawns[Random.Range(0, spawns.Length)].transform.position;
    }

    private void ChooseRandomSpawnPosition()
    {
        spawnPos = GetRandomSpawnPosition();
    }
}
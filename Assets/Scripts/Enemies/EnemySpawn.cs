using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Threading;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    private const float MAX_RADIUS_ANGLE = 359.9f;

    public int center_X;                    // The X position where the moon is
    public int center_Y;                    // The Y position where the moon is
    public float radius = 30.0f;              // The spawn radius around the moon. The enemies will spawn in that perimetter.
    private float spawn_time_range = 10.0f; // The time lapse
    public int n_enemies = 3;               // The number of enemies that will spawn in a lapse time -> (spawn_time_range)

    public GameObject enemy_game_obj;       // The enemy gameObject
    private float current_time = 0.0f;      // The current time inside the time lapse

    private List<float> spawn_times;        // List of spawn times. Each value indicates when the enemy should spawn inside the time lapse.


    void Start()
    {
        calculateRandomSpawnTimes();
    }

    // Update is called once per frame
    void Update()
    {
        current_time += Time.deltaTime;

        int enemies_to_spawn = 0;                    // The number of enemies to spawn this frame
        List<int> index_to_remove = new List<int>(); // Time Indexes to remove after spawn
        int current_index = 0;                       // Variable to store the iteration index

        // Get how many enemies we have to spawn this frame
        foreach (float time_to_spawn in spawn_times) {
            if (current_time >= time_to_spawn) {
                index_to_remove.Add(current_index);
                enemies_to_spawn++;
            }
            current_index++;
        }

        // Spawn enemies
        for (int i= 0; i < enemies_to_spawn; i++)
        {        
            float random_angle = (float)UnityEngine.Random.Range(0.0f, MAX_RADIUS_ANGLE);
            Vector3 spawn_pos = new Vector3((float)(radius * Math.Cos(random_angle)), (float)(radius * Math.Sin(random_angle)), 0.0f);
            GameObject new_obj = GameObject.Instantiate(enemy_game_obj);
            new_obj.transform.position = spawn_pos;
        }

        
        // Remove those indexes that have spawn already
        foreach (int index in index_to_remove)
        {
            spawn_times.RemoveAt(index);
        }

        // Refresh the spawn time array in case the time lapse has expired
        if (current_time >= spawn_time_range)
        {
            current_time = 0.0f;
            calculateRandomSpawnTimes();
        }

    }

    void calculateRandomSpawnTimes()
        // Create a list of spawn times between the time lapse
    {
        spawn_times = new List<float>(n_enemies);
        for (int i=0; i<n_enemies; i++)
        {
            spawn_times.Add((float)UnityEngine.Random.Range(0.0f, spawn_time_range));
        }
        spawn_times.Sort();
    }
}

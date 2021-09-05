using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeScript : MonoBehaviour
{
   [Header("References")]
    [SerializeField] private Pipe pipePrefab = null;
    [SerializeField] private float gapSize  = 9;
    [SerializeField] private float secondsBetweenSpawns = 2f;

    private float spawnTimer;

    private readonly List<Pipe> pipes = new List<Pipe>();

    private void Update() 
    {
        RemoveOldPipes();
        SpawnNewPipes();
    }

    public void RemoveOldPipes()
    {
        //looping over the pipes list count 
        for(int i = pipes.Count - 1; i >= 0; i--)
        {
            //and if the pipes are on or beond -50f length then they will be destroyed 
            if(pipes[i].transform.position.x < -20f)
            {
                Destroy(pipes[i].gameObject);
                //Removes pipe
                pipes.RemoveAt(i);
            }
            
            
        }
    }

    private void SpawnNewPipes()
    {
        //Pipe Spawner
        spawnTimer -= Time.deltaTime;

        //If timer is ready then it'll spawn pipe
        if(spawnTimer > 0f) {return;}

        // The top pipe prefa is flipped 180 and in the same postion
        Pipe topPipe = Instantiate(pipePrefab, transform.position, Quaternion.Euler(0f, 0f, 180f));
        //topPipe.transform.Translate(new Vector3(2.1f, 0f, 0));
        //Bottom pipe stays the same postion
        Pipe bottomPipe = Instantiate(pipePrefab, transform.position, Quaternion.Euler(0f, 0f, 0f));

        //a random postion of where the top pipes will meet each other (THEY ARE TOUCHING EACH OTHER)
        float centerHeight = UnityEngine.Random.Range(-2.5f, 3f);

        //The his will Move the bottom pipe and top pipe 2 spaces down so it is able to create the gap in the middle for the bird to get through
        topPipe.transform.Translate(Vector3.up * (centerHeight + (gapSize / 2)), Space.World);
        bottomPipe.transform.Translate(Vector3.up * (centerHeight - (gapSize / 2)), Space.World);

        // Then add them to the list 
        pipes.Add(topPipe);
        pipes.Add(bottomPipe);

        // and reset spawn timer
        spawnTimer = secondsBetweenSpawns;
    }

    public void ResetPipes()
    {
        foreach(var pipe in pipes)
        {
            Destroy(pipe.gameObject);
        }

        pipes.Clear();
        
        spawnTimer = 0f;
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static SR_TreeController;

public class EnemySpawnController : MonoBehaviour
{
    public List <EnemyInfo> EnemysInfo = new List <EnemyInfo> ();
    public List<SR_TreeController> sR_Trees = new List <SR_TreeController> ();

    public List <GameObject> Enemys = new List <GameObject> ();

    public List<float> SpawnTimer = new List<float> ();

    //public float SpawnTimer = 8;
    public int Number = 0;
    public float minusTimer = 0;

    public float SpawnCount = 0;

    public float minusFlagTimer = 10;
    float minusFlagCount = 0;

    public enum Mode 
    { 
    

        Game,
        Stay
    
    
    }
    public Mode mode = Mode.Game;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (mode == Mode.Game)
        {
            SpawnCount += Time.deltaTime;
            if (SpawnTimer[Number] < SpawnCount)
            {
                RandomEnemySpawn();
            }

            minusFlagCount += Time.deltaTime;
            if (minusFlagCount > minusFlagTimer ) 
            {
                minusFlagCount = 0;

                
                if (SpawnTimer.Count <= Number)
                {
                    //PhaseãŒÀ
                }
                else 
                { 
                    Number++;
                }
                
            }
        }
    }

    void RandomEnemySpawn() 
    {
        SpawnCount = 0;

        int AllSpawnRate =0;
        foreach (EnemyInfo info in EnemysInfo) 
        {
            AllSpawnRate += info.EnemySpawnRate;
        }
        int RandomRate = Random.Range(0, AllSpawnRate);
        int RateNow = 0;
        foreach(EnemyInfo info in EnemysInfo)
        {
            RateNow += info.EnemySpawnRate;
            if (RateNow > RandomRate ) 
            {

                if (info.Fly) 
                {
                   GameObject CL_Enemy = Instantiate(info.EnemyPrefab);
                    Enemys.Add( CL_Enemy );
                }
                else
                { 
                    int RandomSpaenTree = Random.Range(0, sR_Trees.Count);

                    int RandomTrees = Random.Range(0, 3);
                    if (RandomTrees == 0)
                    {

                        foreach (TreeInfo treeinfo in sR_Trees[RandomSpaenTree].Trees)
                        {
                            if (treeinfo.SquirrelHole)
                            {

                                GameObject CL_Enemy = Instantiate(info.EnemyPrefab, treeinfo.MyTree.transform.position, Quaternion.identity);
                                Enemys.Add(CL_Enemy);
                            }
                        }

                    }
                    else 
                    {
                        GameObject CL_Enemy = Instantiate(info.EnemyPrefab, sR_Trees[RandomSpaenTree].transform.position, Quaternion.identity);
                        Enemys.Add(CL_Enemy);
                    }

                    
                }
                break;
            }


        }

    }
}

[System.Serializable]
public class EnemyInfo 
{
    public GameObject EnemyPrefab;

    public bool Fly = false;

    public string EnemyName;
    public int EemyID = 0;
    public int EnemySpawnRate = 0;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public float ERROR_TRESHOLD = 0.1f;

    public GameObject[] girlPrefabs;
    public GameObject[] spawnLocations;

    public float distance = 3f;
    public float girlSpeed = 5f;
    public float delaySpawn = 10f;
    public float delayDestroy = 5f;

    public float maxX = 7f;
    public float maxY = 3f;

    public int idleState = 0;
    public int walkRightState = 1;
    public int walkLeftState = 2;

    public List<GirlAndTarget> pairs = new List<GirlAndTarget>();

    // private List<GameObject> girls = new List<GameObject>();
    // private List<Vector3> targets = new List<Vector3>();

	// Use this for initialization
	void Start () {
        GameObject tempGirl = Instantiate(girlPrefabs[Random.Range(0, girlPrefabs.Length)], spawnLocations[Random.Range(0, spawnLocations.Length)].transform.position, Quaternion.identity);
        Vector3 tempTarget = new Vector3(Random.Range(-maxX, maxX), Random.Range(-maxY, maxY), 0f);
        pairs.Add(new GirlAndTarget(tempGirl, tempTarget));

        // girls.Add(Instantiate(girl, spawnLocations[Random.Range(0, 3)].transform.position, Quaternion.identity));
        // targets.Add(new Vector3(Random.Range(-maxX, maxX), Random.Range(-maxY, maxY), 0f));

        tempGirl = Instantiate(girlPrefabs[Random.Range(0, girlPrefabs.Length)], spawnLocations[Random.Range(0, spawnLocations.Length)].transform.position, Quaternion.identity);
        tempTarget = getNewTarget();
        pairs.Add(new GirlAndTarget(tempGirl, tempTarget));

        // girls.Add(Instantiate(girl, spawnLocations[Random.Range(0, 3)].transform.position, Quaternion.identity));
        // targets.Add(getNewTarget());

        InvokeRepeating("SpawnGirl", delaySpawn, delaySpawn);
	}

	// Update is called once per frame
	void Update () {
        foreach (GirlAndTarget pair in pairs) {
            GameObject girl = pair.girl;
            Vector3 target = pair.target;

            if (Vector3.Distance(girl.transform.position, target) > ERROR_TRESHOLD) {
                girl.transform.position =  Vector3.MoveTowards(girl.transform.position, target, girlSpeed * Time.deltaTime);
            } else {
                girl.GetComponent<TargetBehaviour>().alive = true;
            }
        }
	}

    Vector3 getNewTarget () {
        while (true) {
            Vector3 temp = new Vector3(Random.Range(-maxX, maxX), Random.Range(-maxY, maxY), 0f);
            bool found = true;
            foreach (GirlAndTarget pair in pairs) {
                if (Vector3.Distance(temp, pair.target) < distance) {
                    found = false;
                    break;
                }
            }
            if (found) {
                return temp;
            }
        }
    }

    void SpawnGirl () {
        GameObject tempGirl = Instantiate(girlPrefabs[Random.Range(0, girlPrefabs.Length)], spawnLocations[Random.Range(0, spawnLocations.Length)].transform.position, Quaternion.identity);
        Vector3 tempTarget = getNewTarget();
        pairs.Add(new GirlAndTarget(tempGirl, tempTarget));

    }

    public void despawn (GameObject obj) {
        pairs[GetPairIndexOf(obj)].target = spawnLocations[Random.Range(0, spawnLocations.Length)].transform.position;

        StartCoroutine(destroyGirl(obj, delayDestroy));
    }

    IEnumerator destroyGirl (GameObject obj, float delay) {
        yield return new WaitForSeconds(delay);

        pairs.RemoveAt(GetPairIndexOf(obj));
        Destroy(obj);
    }

    int GetPairIndexOf (GameObject obj) {
        for (int i = 0; i < pairs.Count; i++) {
            if (pairs[i].girl == obj) {
                return i;
            }
        }
        return -1;
    }
}

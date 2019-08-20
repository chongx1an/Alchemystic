using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSummonAreaController : MonoBehaviour
{
    private ArrayList summonerList;
    public GameObject enemyPrefab;
    public int maxNumOfSummoner;
    private bool isSpawning;
    // Start is called before the first frame update
    void Start()
    {
        summonerList = new ArrayList();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        foreach (GameObject obj in summonerList.ToArray())
        {
            if (obj == null)
            {
                summonerList.Remove(obj);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            AudioManagerController.instance.StartCoroutine("StartEnterBossBackgroundMusic");
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {


            if (summonerList.Count < maxNumOfSummoner)
            {

                if (!isSpawning)
                {

                    StartCoroutine("Summon");

                }

            }
        }
    }

    private IEnumerator Summon()
    {
        Vector2 spawnPos = new Vector2(transform.position.x + Random.Range(-5, 5), transform.position.y);
        GameObject enemy = Instantiate(enemyPrefab, spawnPos, transform.rotation);
        summonerList.Add(enemy);

        isSpawning = true;
        yield return new WaitForSeconds(10f);
        isSpawning = false;

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTriggeredAreaController : MonoBehaviour
{
    private ArrayList summonerList;
    public GameObject enemyPrefab;
    private PortalController portalController;
    public int maxNumOfSummoner;
    private bool isSpawning;
    public GameObject bossCoverBackground;
    private SpriteRenderer rendererCoverBackground;
    // Start is called before the first frame update
    void Start()
    {
        summonerList = new ArrayList();
        rendererCoverBackground = bossCoverBackground.GetComponent<SpriteRenderer>();
        portalController = FindObjectOfType<PortalController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        foreach(GameObject obj in summonerList.ToArray())
        {
            if(obj == null)
            {
                summonerList.Remove(obj);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            StartCoroutine("DarkerBackground");
            AudioManagerController.instance.StartCoroutine("StartEnterBossBackgroundMusic");
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (!FindObjectOfType<PortalController>().isSealed)
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

    private IEnumerator DarkerBackground()
    {
        while(rendererCoverBackground.color.a < 0.5f && (!portalController.isSealed || FindObjectsOfType<BossController>().Length != 0))
        {
            Color newColor = new Color(
                rendererCoverBackground.color.r, 
                rendererCoverBackground.color.g, 
                rendererCoverBackground.color.b, 
                rendererCoverBackground.color.a + 0.01f);

            rendererCoverBackground.color = newColor;

            yield return new WaitForSeconds(0.5f);
        }
    }

    private IEnumerator LighterBackground()
    {
        while (rendererCoverBackground.color.a > 0 && portalController.isSealed && FindObjectsOfType<BossController>().Length == 0)
        {
            Color newColor = new Color(
                rendererCoverBackground.color.r,
                rendererCoverBackground.color.g,
                rendererCoverBackground.color.b,
                rendererCoverBackground.color.a - 0.01f);

            rendererCoverBackground.color = newColor;

            yield return new WaitForSeconds(0.5f);
        }
    }
}

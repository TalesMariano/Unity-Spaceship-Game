using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("References")]

    public ShipWiggle ship;
    public TransitionLevel transitionLevel;
    public MobileFunctions mobileFunctions;


    [Header("EndGame")]
    public GameObject canvasEnd;


    [Header("Others")]

    public int currentLevel = 0;



    public Level[] levels;

    public Vector3 posOne;
    public Vector3 posTwo;

    private void Update()
    {
        if (currentLevel  >= levels.Length || levels[currentLevel].CheckAllAsteroidsGone())
        {
            currentLevel++;

            MoveLevel();
        }
    }

    [ContextMenu("MovelLevel")]
    public void MoveLevel()
    {
        //int level = 0;

        ship.StopShip();

        if(currentLevel-1 >= levels.Length)
        {
            StartCoroutine(IeEndGame());
        }
        else
        {
            StartCoroutine(IeMovelLevel(currentLevel));
        }

        
    }

    IEnumerator IeMovelLevel(int level = 0)
    {
        yield return new WaitForSeconds(0.8f); // wait ship move center

        transitionLevel.VFX();

        yield return new WaitForSeconds(0.8f);


        float duration = 1;

        for (float i = 0; i < duration; i+= Time.deltaTime)
        {
            levels[level].levelsObj.transform.position = Vector3.Lerp(posOne, posTwo, i / duration);

            yield return null;
        }

        levels[level].levelsObj.transform.position = posTwo;

        levels[level].StartAsteroids();

        ship.StartMove();
    }


    IEnumerator IeEndGame()
    {
        
        ship.EndLaunch();

        yield return new WaitForSeconds(4);

        canvasEnd.SetActive(true);

        yield return new WaitForSeconds(2);
        mobileFunctions.resetable = true;
    }



    [System.Serializable]
    public class Level
    {
        public GameObject levelsObj;

        public GameObject[] asteroids;

        public void StartAsteroids()
        {
            foreach (var item in asteroids)
            {
                item.GetComponent<IMove>().StartMove();
            }
        }

        public bool CheckAllAsteroidsGone()
        {
            for (int i = 0; i < asteroids.Length; i++)
            {
                if (asteroids[i] != null)
                    return false;
            }

            return true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.gray;
        Gizmos.DrawWireSphere(posOne, 1);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(posTwo, 1);
    }
}

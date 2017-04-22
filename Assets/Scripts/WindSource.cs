using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindSource : MonoBehaviour {

    public float totalLifeTime;
    public WindParticle WindParticlePrefab;

	// Use this for initialization
	IEnumerator Start ()
    {
        yield return null;


        float totalTime = 0;
        while (true)
        {
            
            float randomTime = Random.Range(0.05f, 0.2f);
            totalTime += randomTime;

            print(totalTime + "   " + totalLifeTime);
            if (totalTime < totalLifeTime)
            {
                WindParticle windParticle = Instantiate(WindParticlePrefab);
                windParticle.transform.position = transform.position;

                yield return new WaitForSeconds(randomTime);
            }
            else
            {
                Destroy(gameObject);
                break;
            }
        }
        

    }
}

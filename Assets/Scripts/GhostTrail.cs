using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostTrail : MonoBehaviour
{
    public float ghostDelay;
    public GameObject ghost;
    public bool createGhost;
    //private variables
    private float ghostDelaySeconds;

    //components
    private SpriteRenderer rend;

    // Start is called before the first frame update
    void Start()
    {
        ghostDelaySeconds = ghostDelay;
    }

    // Update is called once per frame
    void Update()
    {
        if(createGhost) {
            if (ghostDelaySeconds > 0) {
                ghostDelaySeconds -= Time.deltaTime;
            }
            else {
                //Generate a ghost
                GameObject currentGhost = Instantiate(ghost, transform.position, transform.rotation);
                currentGhost.transform.localScale = this.transform.localScale;
                currentGhost.GetComponent<SpriteRenderer>().sprite = GetComponent<SpriteRenderer>().sprite;
                ghostDelaySeconds = ghostDelay;
                Destroy(currentGhost, 0.5f);
            }
        }
    }
}

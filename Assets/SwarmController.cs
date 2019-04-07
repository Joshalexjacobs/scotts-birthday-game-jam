using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmController : MonoBehaviour
{

    public GameObject AgentPrefab;
    private List<Rigidbody2D> AgentRigidList;
    private List<GameObject> AgentList;

    private Vector2 target = new Vector2();

    private int PopulationSize = 10;
    private float dampening = 0.000005f;
    private float minspeed = 0.8f;
    private float maxspeed = 0.95f;

    // Start is called before the first frame update
    void Start()
    {
        AgentRigidList = new List<Rigidbody2D>();
        AgentList = new List<GameObject>();

        target = new Vector2();

        for (int i = 0; i < PopulationSize; i++) {
            GameObject agent = Instantiate(AgentPrefab, new Vector3(i * 1.0f, 0, 0), Quaternion.identity);
            AgentList.Add(agent);
            AgentRigidList.Add(agent.GetComponent<Rigidbody2D>());
        }

        for (int j = 0; j < PopulationSize; j++)
        {
            Rigidbody2D body = AgentRigidList[j];
            body.position = new Vector2(Random.value * 10 - 5, Random.value * 10 - 5);
        }
    }

    private void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {

       

        for (int i = 0; i < PopulationSize; i++) {
            Rigidbody2D body = AgentRigidList[i];
            GameObject gobject = AgentList[i];

            Vector3 mousePosition = Input.mousePosition;
            Vector3 objectPosition = Camera.main.WorldToScreenPoint(body.transform.position);
            Vector2 objectPos = new Vector2(objectPosition.x, objectPosition.y);

            float randDia = 300.0f;
            float randx = Random.value * randDia - (randDia/2);
            float randy = Random.value * randDia - (randDia/2);
            Vector2 mousePos = new Vector2(mousePosition.x + randx, mousePosition.y + randy);
            

            Vector2 normd = (mousePos - objectPos);
            normd.Normalize();
            body.velocity += normd * (Time.deltaTime * 60);


            Vector2 velod = body.velocity;
            velod.Normalize();

            float direction = Vector2.Dot(normd, velod);
            if ((direction <= 0) && (body.velocity.magnitude > minspeed)) {
                body.velocity *= (1.0f - dampening) * (Time.deltaTime * 60);
            }


            
            if (body.velocity.magnitude > maxspeed)
            {
                body.velocity.Normalize();
                body.velocity *= maxspeed;
                //sometimes when testing on the first frame the delta time was huge and caused it to explode for a moment
                /*
                if (body.velocity.magnitude > 0.05f)
                {
                    body.velocity.Normalize();
                }
                */
            }
            
            

            Vector2 v = body.velocity;
            float angle = -Mathf.Atan2(v.x, v.y) * Mathf.Rad2Deg;
            body.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }


    /***
     * 
     * This function isn't used
     ***/
    Vector2 flockGeneralDirection(int agentperspective, float distance)
    {
        Vector2 gendirection = new Vector2();
        Vector2 agentDirection = AgentRigidList[agentperspective].velocity;


        for (int i = 0; i < PopulationSize; i++)
        {
            if(agentperspective != i) { 
                Rigidbody2D body = AgentRigidList[i];
                gendirection = gendirection + body.velocity;
            }
        }

        return gendirection / PopulationSize;
    }

}

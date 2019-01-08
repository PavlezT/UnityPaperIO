using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;             //Floating point variable to store the player's movement speed.
    private float _speed;

    private Rigidbody rb;
    private LineRenderer line;
    //GameObject[] polygons = new GameObject[5];
    GameObject[] polygons = new GameObject[2];
    // Start is called before the first frame update
    private int lineCount = 0;
    private bool inZone = true;

    private int prevHor = 0;
    private int preVer = 1;
    private float preAngle = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        this.initLine();

        this.initCircle();

        this._speed = this.speed;
    }

    // Update is called once per frame
    void Update()
    {
        this.makeMovement();

        this.meakeRotate();
    }

    private void FixedUpdate()
    {
    
        this.drawLine();
       
    }

    private void initLine()
    {
        line = GameObject.Find("UserLine").GetComponent<LineRenderer>();
        line.SetWidth(0.5f, 0.5f);
        this.lineCount = 0;
    }

    private void initCircle()
    {
        FieldScript script = new FieldScript();
        line.SetVertexCount(script.segments + 1);
        script.CreatePoints(line);

        for (int x = 0; x < 2; x++)
        {
            polygons[x] = new GameObject();
            polygons[x].name = "PlayerZonePolygon";
            Mesh mesh = new Mesh();

            //Components
            MeshFilter MF = polygons[x].AddComponent<MeshFilter>();
            MeshRenderer MR = polygons[x].AddComponent<MeshRenderer>();
            polygons[x].AddComponent<SphereCollider>();
            //Rigidbody rigid = polygons[x].AddComponent<Rigidbody>();
            //rigid.useGravity = false;
            //rigid.angularDrag = 0;
            //rigid.drag = 0;

            //rigid.position = polygons[x].transform.position;
            //myObject[x].AddComponent()

            mesh = CreateMesh(x);

            //Assign materials
            MR.material = gameObject.GetComponent<MeshRenderer>().material;

            //Assign mesh to game object
            MF.mesh = mesh;
        }

        this.initLine();
    }

    private void drawLine()
    {
        //Debug.Log(line);
        this.lineCount++;
        line.SetVertexCount(lineCount);
        line.SetPosition(lineCount-1, this.transform.position);

    }

    private void meakeRotate()
    {
        Vector3 point = new Vector3(0, 1, 0);

        float angle = 0;

        if (this.preVer == 1 && this.prevHor == 0)
        {
            angle = 0;
        } 
        else if (this.preVer == -1 && this.prevHor == 0)
        {
            angle = -180f;
        }
        else if (this.preVer == 0 && this.prevHor == 1)
        {
            angle = -90f;
        }
        else if (this.preVer == 0 && this.prevHor == -1)
        {
            angle = 90f;
        }
        else if (this.preVer > 0 && this.prevHor > 0)
        {
            angle = -45f;
        }
         else if (this.preVer > 0 && this.prevHor < 0)
        {
            angle = 45f;
        }
         else if (this.preVer < 0 && this.prevHor > 0)
        {
            angle = -135f;
        }
        else if (this.preVer < 0 && this.prevHor < 0)
        {
            angle = 135f;
        }

        this.transform.Rotate(point, this.preAngle - angle);

        this.preAngle = angle;
    }

    private void makeMovement()
    {
        //Store the current horizontal input in the float moveHorizontal.
        float moveHorizontal = Input.GetAxis("Horizontal");

        //Store the current vertical input in the float moveVertical.
        float moveVertical = Input.GetAxis("Vertical");

        int directionVertical = moveVertical > 0 ? 1 : -1;
        int directionHorizontal = moveHorizontal > 0 ? 1 : -1;

        if (moveVertical == 0 && moveHorizontal == 0)
        {
            directionVertical = this.preVer;
            directionHorizontal = this.prevHor;
        } 
        else if (moveVertical == 0)
        {
            directionVertical = 0;
        } 
        else if (moveHorizontal == 0)
        {
            directionHorizontal = 0;
        }

        this.preVer = directionVertical;
        this.prevHor = directionHorizontal;

        //Use the two store floats to create a new Vector2 variable movement.
        Vector3 movement = new Vector3(directionHorizontal, 0, directionVertical);

        //Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
        rb.AddForce(movement * speed);
    }

    void updatePolygon()
    {
        //GameObject[] polygons = new GameObject[5];
        for (int x = 0; x < 2; x++)
        {
            //Destroy old game object
            //Destroy(polygons[x]);

            //New mesh and game object
            //polygons[x] = new GameObject();
            //polygons[x].name = "MousePolygon";
            Mesh mesh = new Mesh();

        //Components
            MeshFilter MF = polygons[x].GetComponent<MeshFilter>();
            //MeshRenderer MR = polygons[x].GetComponent<MeshRenderer>();
            //myObject[x].AddComponent()
           
            mesh = CreateMesh(0);

            //Assign materials
            //MR.material = gameObject.GetComponent<MeshRenderer>().material;

            //Assign mesh to game object
            MF.mesh = mesh;
        }
    }

    Mesh CreateMesh(int num)
    {
        int x;

        //Create a new mesh
        Mesh mesh = new Mesh();

        //Vertices
        var vertex = new Vector3[line.positionCount];

        for (x = 0; x < line.positionCount; x++)
        {
            vertex[x] = line.GetPosition(x);
            vertex[x].y += 0.5f;
        }

        //UVs
        var uvs = new Vector2[vertex.Length];

        for (x = 0; x < vertex.Length; x++)
        {
            if ((x % 2) == 0)
            {
                uvs[x] = new Vector2(0, 0);
            }
            else
            {
                uvs[x] = new Vector2(1, 1);
            }
        }

        //Triangles
        var tris = new int[3 * (vertex.Length - 2)];    //3 verts per triangle * num triangles
        int C1;
        int C2;
        int C3;

        if (num == 0)
        {
            C1 = 0;
            C2 = 1;
            C3 = 2;

            for (x = 0; x < tris.Length; x += 3)
            {
                tris[x] = C1;
                tris[x + 1] = C2;
                tris[x + 2] = C3;

                C2++;
                C3++;
            }
        }
        else
        {
            C1 = 0;
            C2 = vertex.Length - 1;
            C3 = vertex.Length - 2;

            for (x = 0; x < tris.Length; x += 3)
            {
                tris[x] = C1;
                tris[x + 1] = C2;
                tris[x + 2] = C3;

                C2--;
                C3--;
            }
        }

        //Assign data to mesh
        mesh.vertices = vertex;
        mesh.uv = uvs;
        mesh.triangles = tris;

        //Recalculations
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        //mesh.Optimize();

        //Name the mesh
        mesh.name = "MyMesh";

        //Return the mesh
        return mesh;
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.name == "Cylinder")
        {
            this.speed = this._speed;
        }
        else if (other.name == "PlayerZonePolygon" && this.inZone == false)
        {
            this.inZone = true;
            Debug.Log("Draw polygon");
            this.updatePolygon();
        }
        //else
        {
            Debug.Log("enter:" + other.name);
        }
    }

    //void OnTriggerStay(Collider other)
    //{
    //    Debug.Log("stay:" + other.name);
    //    //print("Still colliding with trigger object " + other.name);
    //}

    void OnTriggerExit(Collider other)
    {

        if (other.name == "Cylinder")
        {
            this.speed = 0;
        }
        else if (other.name == "PlayerZonePolygon")
        {
            this.inZone = false;
            Debug.Log("inZone:" + other.name);
        }

        //else
        {
            Debug.Log("exit:" + other.name);
        }
    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        //Debug.Log("collision enter");
        print("Detected collision between " + gameObject.name + " and " + collisionInfo.collider.name);
        //print("There are " + collisionInfo.contacts.Length + " point(s) of contacts");
        //print("Their relative velocity is " + collisionInfo.relativeVelocity);
    }

}

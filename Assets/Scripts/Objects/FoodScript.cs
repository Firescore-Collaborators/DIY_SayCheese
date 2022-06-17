using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SplineMesh;
using DG.Tweening;

//Object script of Food which about to cut
public class FoodScript : MonoBehaviour
{
    //To choose if mesh is top or bottom half
    public enum Half
    {
        Top,
        Bottom
    };
    public Half meshType;

    //Private variables
    private MeshBender meshBender;
    Vector3[] vertices;
    Vector3 originPoint;
    Mesh mesh;
    Transform knifeCutPoint;
    Transform knifeBufferPoint;
    int cutVertice = 0;

    [System.NonSerialized]
    public bool cutComplete = false;
    float sideEdge;

    //class for storing vertex data.
    private class Vertex
    {
        public int id;
        public Vector3 worldPos;
        public bool isliced;
    }

    List<Vertex> SlicingVertex = new List<Vertex>();

    //Initialising mesh data
    public void MeshInitialisation()
    {
       
        //Debug.Log("Mesh Initialisation called =" + gameObject.name);
        SlicingVertex.Clear();
        cutVertice = 0;
        cutComplete = false;
        //transform.position = HorizonStepManager.Inst.foodStartPos.transform.position;
        originPoint = transform.position;
        knifeCutPoint = HorizonStepManager.Inst.cheeseSlicer.transform.GetChild(0);
        knifeBufferPoint = HorizonStepManager.Inst.cheeseSlicer.transform.GetChild(1);
       
        //Getting mesh data
        if (meshType == Half.Top)
        {

            meshBender = GetComponent<MeshBender>();
            if (meshBender.Result != null)
            {
                mesh = meshBender.Result;
            }
        }
        else 
        {
            mesh = transform.GetChild(0).transform.GetComponent<MeshFilter>().mesh;
        }
        vertices = mesh.vertices;

        //detecting extream points
        Vector3 temp1 = transform.TransformPoint(vertices[0]);
        if(meshType == Half.Top)
        {
            sideEdge = temp1.x;
        }
        else
        {
            sideEdge = temp1.x;
        }

        //selecting vertices to be modified 
        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 temp = transform.TransformPoint(vertices[i]);
            //Based on mesh type
            if (temp.y < originPoint.y && meshType == Half.Top)
            {
                if(temp.x<sideEdge)
                {
                    sideEdge = temp.x;
                }
                var k = new Vertex();
                k.id = i;
                k.worldPos = vertices[i];
                k.isliced = false;
                SlicingVertex.Add(k);
            }
            else if (temp.y > originPoint.y && meshType == Half.Bottom)
            {
                if (temp.x < sideEdge)
                {
                    sideEdge = temp.x;
                }
                var k = new Vertex();
                k.id = i;
                k.worldPos = vertices[i];
                k.isliced = false;
                SlicingVertex.Add(k);
            }
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        if (HorizonStepManager.Inst.cutState == HorizonStepManager.HorizontalCuttingStates.cutting)
        {
            UpdateMesh();
        }
    }

  
    //Mesh slicing action
    Vector3 temp;
    void UpdateMesh()
    {
        Vector3 localKnifeCutPos;
        Vector3 localKnifeBufferPos;
        localKnifeCutPos = transform.InverseTransformPoint(knifeCutPoint.transform.position);
        localKnifeBufferPos = transform.InverseTransformPoint(knifeBufferPoint.transform.position);
        //checking for game over state
        if (cutVertice >= SlicingVertex.Count || knifeCutPoint.transform.position.z<sideEdge) //taking side edge to ensure gameover state just incase meshslice was undetected due to frame drop 
        {
            if (meshType == Half.Top && cutComplete==false)
            {
                Timer.Delay(1, TopMeshMoveUpAnimation);
            }
            cutComplete = true;
            HorizonStepManager.Inst.cutState = HorizonStepManager.HorizontalCuttingStates.cutComplete;
        
            return;
        }
        else
        {
            //iterating through all slicable vertices
            for (int i = 0; i < SlicingVertex.Count; i++)
            {
                //Top half
                if (meshType == Half.Top && SlicingVertex[i].isliced == false)
                {
                    //if (SlicingVertex[i].worldPos.z < localKnifeBufferPos.z && SlicingVertex[i].worldPos.z > localKnifeCutPos.z)
                    if (SlicingVertex[i].worldPos.x > localKnifeCutPos.z)
                    {
                        
                        cutVertice += 1;
                        meshBender.UpdateSourceVertexData(SlicingVertex[i].id, localKnifeCutPos.y);//Updating source mesh in meshbender
                        SlicingVertex[i].isliced = true;
                    }
                }
                //Bottom half
                else if (meshType == Half.Bottom && SlicingVertex[i].isliced == false)
                {

                    // if (SlicingVertex[i].worldPos.x > localKnifeBufferPos.z && SlicingVertex[i].worldPos.x < localKnifeCutPos.z)
                    if (SlicingVertex[i].worldPos.x > localKnifeCutPos.z)
                    {
                        cutVertice += 1;
                        //changing vertex position to knife y position
                        temp = new Vector3(vertices[SlicingVertex[i].id].x, localKnifeCutPos.y, vertices[SlicingVertex[i].id].z);
                        vertices[SlicingVertex[i].id] = temp;
                        mesh.vertices = vertices;
                        SlicingVertex[i].isliced = true;
                    }
                }    
            }
        }
    }

    void TopMeshMoveUpAnimation()
    {
        transform.DOMoveY(12, 5f).SetEase(Ease.OutExpo);
    }
}

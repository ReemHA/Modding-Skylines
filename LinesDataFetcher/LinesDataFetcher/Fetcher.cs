using System.IO;
using System.Text;
using ICities;
using Leguar.TotalJSON;
using UnityEngine;

namespace LinesDataFetcher
{
    public class Fetcher : ThreadingExtensionBase
    {
        JSON json;
        public override void OnAfterSimulationFrame()
        {
            base.OnAfterSimulationFrame();
        }

        public override void OnUpdate(float realTimeDelta, float simulationTimeDelta)
        {
            base.OnUpdate(realTimeDelta, simulationTimeDelta);
            if (Input.GetKeyDown(KeyCode.P))
            {
                StringBuilder txtLines = new StringBuilder();
                TransportLine[] lines = TransportManager.instance.m_lines.m_buffer;

                //json = JSON.Serialize(lines);
                //json.CreateString()
                //txtLines.Append(json);
                StreamWriter outputFile = new StreamWriter("Files/transport_lines.csv");
                outputFile.WriteLine(txtLines);
                outputFile.Close();
            }
            else if (Input.GetKeyDown(KeyCode.L))
            {
                int count = 0;
                NetNode[] nodes = NetManager.instance.m_nodes.m_buffer;
                foreach (var node in nodes)
                {
                    var go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    go.transform.position = node.m_position;
                    go.transform.localScale *= 30;
                    count++;
                }
                Debug.Log("The number of nodes (with count) " + count + "the number of nodes (without) " + nodes.Length + "---" + NetManager.instance.m_nodeCount);
            }
            else if (Input.GetKeyDown(KeyCode.J))
            {
                NetNode[] nodes = NetManager.instance.m_nodes.m_buffer;
                foreach (var node in nodes)
                {
                    if (node.m_flags.ToString().Contains(NetNode.Flags.Junction.ToString()))
                    {
                        Debug.Log(node.m_flags.ToString());
                        var go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                        go.transform.position = node.m_position;
                        go.transform.localScale *= 30;
                        Material newMat = new Material(Shader.Find("Custom/Particles/Additive (Soft)"));
                        newMat.color = new Color(34, 139, 34);
                        go.GetComponent<MeshRenderer>().material = newMat;
                        //var go = new GameObject();
                        //var spRend = go.AddComponent<SpriteRenderer>() as SpriteRenderer;
                        //spRend.color = new Color(0.9f, 0.9f, 0.9f, 1.0f);
                        //spRend.transform.position = node.m_position;
                        //var sprite = Sprite.Create(Texture2D.whiteTexture, new Rect(0.0f, 0.0f, Texture2D.whiteTexture.width, Texture2D.whiteTexture.height), new Vector2(0.5f, 0.5f), 50, 2, SpriteMeshType.Tight);
                        //spRend.sprite = sprite;
                    }
                }
            }
        }
    }
}

using System.IO;
using System.Text;
using ICities;
using Leguar.TotalJSON;
using UnityEngine;

namespace LinesDataFetcher
{
    public class Fetcher : ThreadingExtensionBase
    {
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
                    Material newMat = new Material(Shader.Find("Hidden/DayNight/Skybox"));
                    newMat.color = new Color(255, 153, 51);
                    go.GetComponent<MeshRenderer>().material = newMat;
                    go.transform.localScale *= 10;
                    count++;
                }
                Debug.Log("The number of nodes (with count) " + count + "the number of nodes (without) " + nodes.Length + "---" + NetManager.instance.m_nodeCount);
            }
            else if (Input.GetKeyDown(KeyCode.J))
            {
                NetNode[] nodes = NetManager.instance.m_nodes.m_buffer;
                foreach (var node in nodes)
                {
                    if (node.m_flags.ToString().Contains(NetNode.Flags.Junction.ToString()) &&
                        !(node.m_flags.ToString().Contains(NetNode.Flags.OneWayIn.ToString()) &&
                        node.m_flags.ToString().Contains(NetNode.Flags.OneWayOut.ToString())))
                    {
                        Debug.Log(node.m_flags.ToString());
                        var go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                        go.transform.position = node.m_position;
                        go.transform.localScale *= 20;
                        Material newMat = new Material(Shader.Find("Custom/Overlay/TransportConnection"));
                        //newMat.SetColor("color", new Color(0, 244, 0));
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
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                StringBuilder txtLines = new StringBuilder();
                NetNode[] nodes = NetManager.instance.m_nodes.m_buffer;
                NetSegment[] segments = NetManager.instance.m_segments.m_buffer;
                JArray jArray = new JArray();
                foreach (var node in nodes)
                {
                    if (node.m_flags.ToString().Contains(NetNode.Flags.Junction.ToString()) &&
                        !(node.m_flags.ToString().Contains(NetNode.Flags.OneWayIn.ToString()) &&
                        node.m_flags.ToString().Contains(NetNode.Flags.OneWayOut.ToString())))
                    {
                        JSON json = new JSON();
                        json.Add("position", JSON.Serialize(node.m_position));
                        JArray segArray = new JArray();
                        JSON temp = new JSON();
                        AddSegmentsToArray(segments, node, segArray);
                        jArray.Add(json);
                    }
                }
                var jsonString = jArray.CreateString();
                txtLines.Append(jsonString);
                StreamWriter outputFile = new StreamWriter("Files/nodes_info.txt");
                outputFile.WriteLine(txtLines);
                outputFile.Close();
            }
        }

        private void AddSegmentsToArray(NetSegment[] segments, NetNode node, JArray segArray)
        {
            JSON temp = new JSON();
            temp = AddSegmentToJson(node.m_segment1, segments);
            if (temp != null)
            {
                segArray.Add(temp);
            }
            temp = AddSegmentToJson(node.m_segment2, segments);
            if (temp != null)
            {
                segArray.Add(temp);
            }
            temp = AddSegmentToJson(node.m_segment3, segments);
            if (temp != null)
            {
                segArray.Add(temp);
            }
            temp = AddSegmentToJson(node.m_segment4, segments);
            if (temp != null)
            {
                segArray.Add(temp);
            }
            temp = AddSegmentToJson(node.m_segment5, segments);
            if (temp != null)
            {
                segArray.Add(temp);
            }
            temp = AddSegmentToJson(node.m_segment6, segments);
            if (temp != null)
            {
                segArray.Add(temp);
            }
            temp = AddSegmentToJson(node.m_segment7, segments);
            if (temp != null)
            {
                segArray.Add(temp);
            }
        }

        private JSON AddSegmentToJson(ushort index, NetSegment[] segments)
        {
            JSON json = new JSON();
            if (index != 0)
            {
                json.Add("position", JSON.Serialize(segments[index].m_middlePosition));
                json.Add("traffic buffer", segments[index].m_trafficBuffer);
                json.Add("traffic density", segments[index].m_trafficDensity);
                return json;
            }
            return null;
        }
    }
}
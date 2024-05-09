using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Map
{
    public static class MapGenerator
    {
        private static MapConfig config;

        //private static readonly List<NodeType> RandomNodes = new List<NodeType>
        //{NodeType.Mystery, NodeType.Store, NodeType.Treasure, NodeType.MinorEnemy, NodeType.RestSite};

        private static List<float> layerDistances;//用于存储层之间的距离
        private static List<List<Point>> paths;
        // ALL nodes by layer:
        private static readonly List<List<Node>> nodes = new List<List<Node>>();

        public static Map GetMap(MapConfig conf)
        {
            if (conf == null)
            {
                Debug.LogWarning("Config was null in MapGenerator.Generate()");
                return null;
            }

            config = conf;
            nodes.Clear();

            GenerateLayerDistances();//设置层之间的距离

            for (var i = 0; i < conf.layers.Count; i++)//设置每一层的节点,每一层的节点是一个List<Node>
                PlaceLayer(i);

            GeneratePaths();

            RandomizeNodePositions();

            SetUpConnections();

            RemoveCrossConnections();

            CheckIfMapRational();
            // select all the nodes with connections:
            var nodesList = nodes.SelectMany(n => n).Where(n => n.incoming.Count > 0 || n.outgoing.Count > 0).ToList();

            // pick a random name of the boss level for this map:
            var bossNodeName = config.nodeBlueprints.Where(b => b.nodeType == NodeType.Boss).ToList().Random().name;
            return new Map(conf.name, bossNodeName, nodesList, new List<Point>());
        }

        private static void GenerateLayerDistances()
        {
            layerDistances = new List<float>();
            foreach (var layer in config.layers)
                layerDistances.Add(layer.distanceFromPreviousLayer.GetValue());
        }

        private static float GetDistanceToLayer(int layerIndex)
        {
            if (layerIndex < 0 || layerIndex > layerDistances.Count) return 0f;

            return layerDistances.Take(layerIndex + 1).Sum();
        }

        private static void PlaceLayer(int layerIndex)
        {
            var layer = config.layers[layerIndex];
            var nodesOnThisLayer = new List<Node>();

            // offset of this layer to make all the nodes centered:
            var offset = layer.nodesApartDistance * config.GridWidth / 2f;

            for (var i = 0; i < config.GridWidth; i++)
            {
                var nodeType = Random.Range(0f, 1f) < layer.randomizeNodes ? GetRandomNode(layer.extraNodes) : layer.nodeType;
                var blueprintName = config.nodeBlueprints.Where(b => b.nodeType == nodeType).ToList().Random().name;
                var node = new Node(nodeType, blueprintName, new Point(i, layerIndex))
                {
                    position = new Vector2(-offset + i * layer.nodesApartDistance, GetDistanceToLayer(layerIndex))
                };
                nodesOnThisLayer.Add(node);
            }

            nodes.Add(nodesOnThisLayer);
        }

        private static void RandomizeNodePositions()//随机化节点的位置
        {
            for (var index = 0; index < nodes.Count; index++)
            {
                var list = nodes[index];
                var layer = config.layers[index];
                var distToNextLayer = index + 1 >= layerDistances.Count
                    ? 0f
                    : layerDistances[index + 1];
                var distToPreviousLayer = layerDistances[index];

                foreach (var node in list)
                {
                    var xRnd = Random.Range(-1f, 1f);
                    var yRnd = Random.Range(-1f, 1f);

                    var x = xRnd * layer.nodesApartDistance / 2f;
                    var y = yRnd < 0 ? distToPreviousLayer * yRnd / 2f : distToNextLayer * yRnd / 2f;

                    node.position += new Vector2(x, y) * layer.randomizePosition;
                }
            }
        }

        private static void SetUpConnections()//设置连接
        {
            foreach (var path in paths)
            {
                for (var i = 0; i < path.Count - 1; ++i)
                {
                    var node = GetNode(path[i]);
                    var nextNode = GetNode(path[i + 1]);
                    node.AddOutgoing(nextNode.point);
                    nextNode.AddIncoming(node.point);
                }
            }
        }

        private static void RemoveCrossConnections()//移除交叉连接
        {
            for (var i = 0; i < config.GridWidth - 1; i++)
                for (var j = 0; j < config.layers.Count - 1; j++)
                {
                    var node = GetNode(new Point(i, j));
                    if (node == null || node.HasNoConnections()) continue;
                    var right = GetNode(new Point(i + 1, j));
                    if (right == null || right.HasNoConnections()) continue;
                    var top = GetNode(new Point(i, j + 1));
                    if (top == null || top.HasNoConnections()) continue;
                    var topRight = GetNode(new Point(i + 1, j + 1));
                    if (topRight == null || topRight.HasNoConnections()) continue;

                    // Debug.Log("Inspecting node for connections: " + node.point);
                    if (!node.outgoing.Any(element => element.Equals(topRight.point))) continue;
                    if (!right.outgoing.Any(element => element.Equals(top.point))) continue;

                    // Debug.Log("Found a cross node: " + node.point);

                    // we managed to find a cross node:
                    // 1) add direct connections:
                    node.AddOutgoing(top.point);
                    top.AddIncoming(node.point);

                    right.AddOutgoing(topRight.point);
                    topRight.AddIncoming(right.point);

                    var rnd = Random.Range(0f, 1f);
                    if (rnd < 0.2f)
                    {
                        // remove both cross connections:
                        // a) 
                        node.RemoveOutgoing(topRight.point);
                        topRight.RemoveIncoming(node.point);
                        // b) 
                        right.RemoveOutgoing(top.point);
                        top.RemoveIncoming(right.point);
                    }
                    else if (rnd < 0.6f)
                    {
                        // a) 
                        node.RemoveOutgoing(topRight.point);
                        topRight.RemoveIncoming(node.point);
                    }
                    else
                    {
                        // b) 
                        right.RemoveOutgoing(top.point);
                        top.RemoveIncoming(right.point);
                    }
                }
        }

        private static Node GetNode(Point p)
        {
            if (p.y >= nodes.Count) return null;
            if (p.x >= nodes[p.y].Count) return null;

            return nodes[p.y][p.x];
        }
        //获取最后一个节点
        private static Point GetFinalNode()
        {
            var y = config.layers.Count - 1;
            if (config.GridWidth % 2 == 1)
                return new Point(config.GridWidth / 2, y);

            return Random.Range(0, 2) == 0
                ? new Point(config.GridWidth / 2, y)
                : new Point(config.GridWidth / 2 - 1, y);
        }

        private static void GeneratePaths()
        {
            var finalNode = GetFinalNode();//获取最后一个节点,point 是一个下标，二维数组的下标
            paths = new List<List<Point>>();//存储路径的列表
            var numOfStartingNodes = config.numOfStartingNodes.GetValue();//获取起始节点的数量
            var numOfPreBossNodes = config.numOfPreBossNodes.GetValue();//获取boss节点之前的节点数量

            var candidateXs = new List<int>();
            for (var i = 0; i < config.GridWidth; i++)//config.GridWidth代表初始和boss前节点的最大值
                candidateXs.Add(i);

            candidateXs.Shuffle();//随机打乱candidateXs
            var startingXs = candidateXs.Take(numOfStartingNodes);
            var startingPoints = (from x in startingXs select new Point(x, 0)).ToList();

            candidateXs.Shuffle();
            var preBossXs = candidateXs.Take(numOfPreBossNodes);
            var preBossPoints = (from x in preBossXs select new Point(x, finalNode.y - 1)).ToList();

            int numOfPaths = Mathf.Max(numOfStartingNodes, numOfPreBossNodes) + Mathf.Max(0, config.extraPaths);//路径的数目
            for (int i = 0; i < numOfPaths; ++i)
            {
                Point startNode = startingPoints[i % numOfStartingNodes];
                Point endNode = preBossPoints[i % numOfPreBossNodes];
                var path = Path(startNode, endNode);//生成路径
                path.Add(finalNode);
                paths.Add(path);
            }
        }

        // Generates a random path bottom up.
        private static List<Point> Path(Point fromPoint, Point toPoint)
        {
            int toRow = toPoint.y;//1.	首先，我们定义了一些变量来存储起点和终点的行列坐标，以及上一个节点的列坐标：
            int toCol = toPoint.x;
            int lastNodeCol = fromPoint.x;

            var path = new List<Point> { fromPoint }; //2.接下来，我们创建一个空的路径列表，并将起点添加到路径中：

            var candidateCols = new List<int>(); //3.我们还创建了一个候选列列表，用于存储可能的下一个节点的列坐标：
            for (int row = 1; row < toRow; ++row)//4.然后，我们使用一个循环来生成路径的每一行，从第二行开始（因为第一行已经是起点）：
            {
                candidateCols.Clear();//5.在每一行中，我们首先清空候选列列表：

                //6.然后，我们计算当前节点和终点之间的垂直距离，并将其存储在verticalDistance变量中。
                //接下来，我们计算当前节点和终点之间的水平距离，并将其存储在horizontalDistance变量中：
                int verticalDistance = toRow - row;
                int horizontalDistance;

                int forwardCol = lastNodeCol;
                horizontalDistance = Mathf.Abs(toCol - forwardCol);
                //7.	然后，我们计算向前、向左和向右的列坐标，并检查它们是否满足以下条件：
                //水平距离小于等于垂直距离
                //向左的列坐标大于等于0
                //向右的列坐标小于网格宽度

                if (horizontalDistance <= verticalDistance)
                    candidateCols.Add(lastNodeCol);

                int leftCol = lastNodeCol - 1;
                horizontalDistance = Mathf.Abs(toCol - leftCol);
                if (leftCol >= 0 && horizontalDistance <= verticalDistance)
                    candidateCols.Add(leftCol);

                int rightCol = lastNodeCol + 1;
                horizontalDistance = Mathf.Abs(toCol - rightCol);
                if (rightCol < config.GridWidth && horizontalDistance <= verticalDistance)
                    candidateCols.Add(rightCol);
                //8.如果有候选列，我们随机选择一个候选列，并将其添加到路径中。然后，我们更新lastNodeCol变量，以便在下一行中使用：
                int RandomCandidateIndex = Random.Range(0, candidateCols.Count);
                int candidateCol = candidateCols[RandomCandidateIndex];
                var nextPoint = new Point(candidateCol, row);
                //9.最后，我们将下一个节点添加到路径中，并更新lastNodeCol变量：
                path.Add(nextPoint);
                lastNodeCol = candidateCol;
            }

            path.Add(toPoint);

            return path;
        }

        private static void CheckIfMapRational()
        {
            for(var i = 0; i < nodes.Count; i++)
                for(var j = 0; j < nodes[i].Count; j++)
                {
                    var layer = config.layers[i];
                    var node = nodes[i][j];
                    bool ifHasIncomingStore = false;
                    //如果节点是商店，则前面不能是商店
                    if (node.nodeType == NodeType.Store)
                    {
                        //遍历节点的前面的节点
                        foreach(var incoming in node.incoming)
                        {
                            var incomingNode = GetNode(incoming);
                            if (incomingNode.nodeType == NodeType.Store)
                            {
                                ifHasIncomingStore = true;
                                break;
                            }
                        }
                        if (ifHasIncomingStore)
                        {
                            //Debug.LogWarning("Store node has incoming store node!" + node.point.x + " " +node.point.y);
                            node.nodeType =layer.nodeType;
                            node.blueprintName = config.nodeBlueprints.Where(b => b.nodeType == layer.nodeType).ToList().Random().name;
                        }
                    }
                }
        }
        //待定
        private static void CheckIfBrotherNodeLegal()
        {
            for (var i = 0; i < nodes.Count; i++)
                for (var j = 0; j < nodes[i].Count; j++)
                {
                    var layer = config.layers[i];
                    int nodeSum = layer.extraNodes.Length + 1;
                    //总的该层的可选节点数
                    var node = nodes[i][j];
                    bool ifHasIncomingStore = false;
                    //如果节点是商店，则前面不能是商店
                    if (node.nodeType == NodeType.Store)
                    {
                        //遍历节点的前面的节点
                        foreach (var incoming in node.incoming)
                        {
                            var incomingNode = GetNode(incoming);
                            if (incomingNode.nodeType == NodeType.Store)
                            {
                                ifHasIncomingStore = true;
                                break;
                            }
                        }
                        if (ifHasIncomingStore)
                        {
                            //Debug.LogWarning("Store node has incoming store node!" + node.point.x + " " +node.point.y);
                            node.nodeType = layer.nodeType;
                            node.blueprintName = config.nodeBlueprints.Where(b => b.nodeType == layer.nodeType).ToList().Random().name;
                        }
                    }
                }
        }
        private static NodeType GetRandomNode(NodeType[] nodeTypes)
        {
            return nodeTypes[Random.Range(0, nodeTypes.Length)];
        }
    }
}
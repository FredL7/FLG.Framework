using System.Collections.Generic;


namespace FLG.Cs.Graph.Pathfinding {

    /*
     * Populate the graph one origin-destination pair at a time.
     * Might eventually fill the whole graph.
     *
     */
    internal class AStarAlgorithm<T> where T : INodeItem {
        public class ChatGPTNode {
            public ChatGPTNode[] neighbours = [];
            public ChatGPTNode? parent = null;
            public float g = 0, h = 0;
            public float F { get => g + h; }
        }

        public static List<ChatGPTNode>? BidirectionalAStar(ChatGPTNode start, ChatGPTNode goal)
        {
            throw new NotImplementedException();

            //PriorityQueue<ChatGPTNode, float> openListStart = new();
            //HashSet<ChatGPTNode> closedListStart = new();

            //PriorityQueue<ChatGPTNode, float> openListGoal = new();
            //HashSet<ChatGPTNode> closedListGoal = new();

            //start.g = 0;
            //start.h = heuristic(start, goal);
            //start.parent = null;
            //openListStart.Enqueue(start, start.F);

            //goal.g = 0;
            //goal.h = heuristic(goal, start);
            //goal.parent = null;
            //openListGoal.Enqueue(goal, goal.F);

            //while (openListStart.Count > 0 && openListGoal.Count > 0)
            //{
            //    {
            //        var currentNodeStart = openListStart.Dequeue();
            //        if (closedListGoal.Contains(currentNodeStart))
            //        {
            //            return ReconstructPath(start, currentNodeStart, goal);
            //        }
            //        closedListStart.Add(currentNodeStart);

            //        foreach (var neighbour in currentNodeStart.neighbours)
            //        {
            //            if (closedListStart.Contains(neighbour))
            //            {
            //                continue;
            //            }

            //            float tentativeG = currentNodeStart.g + distance(currentNodeStart, neighbour);

            //            if (tentativeG < neighbour.g /* || !openListStart.Contains(neighbour) */)
            //            {
            //                neighbour.parent = currentNodeStart;
            //                neighbour.g = tentativeG;
            //                neighbour.h = heuristic(neighbour, goal);

            //                // if (!openListStart.Contains(neighbour))
            //                openListStart.Enqueue(neighbour, neighbour.F);
            //            }
            //        }
            //    }

            //    {
            //        var currentNodeGoal = openListGoal.Dequeue();
            //        if (closedListStart.Contains(currentNodeGoal))
            //        {
            //            return ReconstructPath(start, currentNodeGoal, goal);
            //        }
            //        closedListGoal.Add(currentNodeGoal);

            //        foreach (var neighbour in currentNodeGoal.neighbours)
            //        {
            //            if (closedListGoal.Contains(neighbour))
            //            {
            //                continue;
            //            }

            //            float tentativeG = currentNodeGoal.g + distance(currentNodeGoal, neighbour);

            //            if (tentativeG < neighbour.g /* || !openListGoal.Contains(neighbour) */)
            //            {
            //                neighbour.parent = currentNodeGoal;
            //                neighbour.g = tentativeG;
            //                neighbour.h = heuristic(neighbour, goal);

            //                // if (!openListStart.Contains(neighbour))
            //                openListGoal.Enqueue(neighbour, neighbour.F);
            //            }
            //        }
            //    }
            //}

            //return null;
        }
    }
}

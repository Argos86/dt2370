using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Tower_Defense.Model
{
    class AStar
    {
        class Node : IComparable<Node>
        {
            public Node()
            {
                m_parent = null;
                m_nCostFromstart = 0;
                m_nCostToGoal = 0;
            }
            public Vector2 m_node;
            public float m_nCostFromstart;
            public float m_nCostToGoal;
            public Node m_parent;

            public int CompareTo(Node a_other)
            {
                return (int)((m_nCostFromstart + m_nCostToGoal) - (a_other.m_nCostFromstart + a_other.m_nCostToGoal));
            }
        };

        const float m_heuristicsModifier = 1.5f;
        bool m_doesNearSearch;
        float m_nearDistance;
        int m_nVisitedNodes;
        float m_fTravelDistant;
        List<Node> m_listOpen;
        List<Node> m_listClosed;
        public List<Vector2> m_path;
        Map m_map;
        Vector2 m_start, m_end;
        public SearchResult m_state;

        public AStar(Map a_map)
        {
            m_map = a_map;
            m_path = new List<Vector2>();
            m_state = SearchResult.SearchNotStarted;
        }

        public enum SearchResult
        {
            SearchNotStarted,
            SearchFailedNoPath,
            SearchNotDone,
            SearchSucceded
        };

        public void InitSearch(Vector2 a_start, Vector2 a_end, bool a_nearSearch, float a_distance)
        {
            //initiate 
            m_listOpen = new List<Node>();
            m_listClosed = new List<Node>();
            m_path = new List<Vector2>();
            m_doesNearSearch = a_nearSearch;
            m_nearDistance = a_distance;
            m_nVisitedNodes = 0;
            m_state = SearchResult.SearchNotDone;
            InitSearch(a_start, a_end);
        }


        bool IsImprovment(Node a_nNode, double a_dVal)
        {

            bool foundImprovment = false;

            //List<Node>::iterator iter = m_listOpen.begin();
            //for (; iter != m_listOpen.end(); iter++)
            foreach (Node iter in m_listOpen)
            {
                if (iter.m_node.X == a_nNode.m_node.X && iter.m_node.Y == a_nNode.m_node.Y)
                {
                    if (a_dVal < iter.m_nCostFromstart)
                    {
                        //Vi har hittat en bättre väg till noden...
                        foundImprovment = true;
                    }
                }
            }

            //iter = m_listClosed.begin();
            //for (; iter != m_listClosed.end(); iter++)
            foreach (Node iter in m_listClosed)
            {
                if (iter.m_node.X == a_nNode.m_node.X && iter.m_node.Y == a_nNode.m_node.Y)
                {
                    if (a_dVal < iter.m_nCostFromstart)
                    {
                        foundImprovment = true;
                    }
                }
            }
            return foundImprovment;
        }

        float TraverseCost(Vector2 a_a, Vector2 a_b)
        {
            return (a_a - a_b).Length();
        }

        void CreatePath(Node a_node)
        {
            if (a_node.m_parent != null)
            {
                CreatePath(a_node.m_parent);
            }
            if (a_node.m_node.X == m_start.X && a_node.m_node.Y == m_start.Y)
            {

            }
            else
            {

                m_path.Add(a_node.m_node);
            }
        }

        /*bool less(AStarSearcher.Node *a_p1, AStarSearcher.Node *a_p2) {
	        return *a_p1 < *a_p2;
        }*/


        bool IsAGoalNode(Node pNode)
        {
            if (m_doesNearSearch == false)
            {
                return pNode.m_node.X == m_end.X && pNode.m_node.Y == m_end.Y;
            }
            else
            {
                return (pNode.m_node - m_end).Length() <= m_nearDistance;
            }
        }



        void InitSearch(Vector2 a_start, Vector2 a_end)
        {
            a_start.X = (int)a_start.X;
            a_start.Y = (int)a_start.Y;

            Node startNode = new Node();
            startNode.m_nCostFromstart = 0.0f;
            startNode.m_nCostToGoal = TraverseCost(a_start, a_end);
            startNode.m_node = a_start;
            startNode.m_parent = null;

            m_listClosed.Clear();
            m_listOpen.Clear();

            m_listOpen.Add(startNode);
            m_nVisitedNodes = 0;

            m_start = a_start;
            m_end = a_end;
        }

        bool ExistInOpen(Vector2 a_node)
        {
            foreach (Node iter in m_listOpen)
            {
                if (iter.m_node.X == a_node.X && iter.m_node.Y == a_node.Y)
                {
                    return true;
                }
            }
            return false;
        }

        bool ExistInClosed(Vector2 a_node)
        {
            foreach (Node iter in m_listClosed)
            {
                if (iter.m_node.X == a_node.X && iter.m_node.Y == a_node.Y)
                {
                    return true;
                }
            }
            return false;
        }
    }
}

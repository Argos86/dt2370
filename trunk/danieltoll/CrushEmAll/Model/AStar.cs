using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ZombieHoards.Model
{
    class AStar
    {
        class Node : IComparable<Node>
        {
				public Node() {
					m_parent = null;
					m_nCostFromstart = 0;
					m_nCostToGoal = 0;
				}
				public Vector2	m_node;
				public float m_nCostFromstart;
				public float m_nCostToGoal;
				public Node	m_parent;
				
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

        public SearchResult Update(int a_maxNodes)
        {
            if (m_state != SearchResult.SearchNotDone)
            {
                return m_state;
            }
            if (m_map.IsClear(m_end) == false)
            {
                m_state = SearchResult.SearchFailedNoPath;
                return SearchResult.SearchFailedNoPath;
            }
            m_nVisitedNodes = 0;

            while(m_listOpen.Count > 0)
            {
                if (m_nVisitedNodes > a_maxNodes) {
                    m_state = SearchResult.SearchNotDone;
                    return SearchResult.SearchNotDone;
                }
                SearchResult result = OneSearchStep();
                if (result != SearchResult.SearchNotDone)
                {
                    m_state = result;
                    return result;
                }
            }
            m_state = SearchResult.SearchFailedNoPath;
            return SearchResult.SearchFailedNoPath;
        }

        bool IsImprovment(Node a_nNode, double a_dVal)
        {

	        bool foundImprovment = false;

	        //List<Node>::iterator iter = m_listOpen.begin();
	        //for (; iter != m_listOpen.end(); iter++)
            foreach(Node iter in m_listOpen)
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
            foreach(Node iter in m_listClosed)
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

        float TraverseCost(Vector2 a_a, Vector2 a_b) {
	        return (a_a - a_b).Length();
        }

        void CreatePath(Node a_node) {
	        if (a_node.m_parent != null) {
		        CreatePath(a_node.m_parent);
	        }
	        if (a_node.m_node.X == m_start.X && a_node.m_node.Y == m_start.Y) {
        		
	        } else {
        		
		        m_path.Add(a_node.m_node);
	        }
        }

        /*bool less(AStarSearcher.Node *a_p1, AStarSearcher.Node *a_p2) {
	        return *a_p1 < *a_p2;
        }*/

        SearchResult OneSearchStep()
        {

	        //Inga flera noder att söka igenom
	        if (m_listOpen.Count == 0) {
		        return SearchResult.SearchFailedNoPath;
	        }
	        //Hämta första noden i open
	        Node pNode = m_listOpen.First();
	        m_listOpen.RemoveAt(0);
        	
	        //Har vi nått fram
	        if (IsAGoalNode(pNode)) {
		        m_path.Clear();
		        CreatePath(pNode);
		        m_fTravelDistant = pNode.m_nCostFromstart;

		        return SearchResult.SearchSucceded;
	        } else {
		        //Stega igenom nodens barn
		        for (int y = -1; y <= 1; y++) {
			        for (int x = -1; x <= 1; x++) {
        			

				        if (x == 0 && y == 0) 
					        continue;

				        // handle diagonal, should be like this in dungeon but not on trail...
				        // on trail diagonal is ok 
				        if (x == y || x == -y) {
					        if (IsClear(pNode.m_node, new Vector2(pNode.m_node.X + x, pNode.m_node.Y)) == false)
						        continue;
					        if (IsClear(pNode.m_node, new Vector2(pNode.m_node.X, pNode.m_node.Y + y)) == false)
						        continue;
				        }


                        if (IsClear(pNode.m_node, new Vector2(pNode.m_node.X + x, pNode.m_node.Y + y)) == true)
                        {
					        Node NewNode = new Node();
					        NewNode.m_parent = null;
					        NewNode.m_node.X = pNode.m_node.X + x;
					        NewNode.m_node.Y = pNode.m_node.Y + y;

					        //Kostnaden för att gå från start till barnnoden
					        float dNewCost = pNode.m_nCostFromstart + TraverseCost(pNode.m_node, NewNode.m_node);

					        //Är den nya kostnaden bättre än någon vi hittat förut till barnnoden?
					        bool bImprovment = IsImprovment(NewNode, dNewCost);
        					
					        //Om den finns i köerna och inte är en förbättring slänger vi den...
					        if (!bImprovment && (ExistInOpen(NewNode.m_node) || ExistInClosed(NewNode.m_node))) {
						        continue;
					        } else {
						        m_nVisitedNodes++;

						        //Vilken väg tog vi för att komma till noden
						        NewNode.m_parent = pNode;
						        //NewNode.m_pNode->setParentNode(node.m_pNode);
						        NewNode.m_nCostFromstart = dNewCost;

						        //Estimera kostnaden till målet
						        NewNode.m_nCostToGoal = TraverseCost(NewNode.m_node, m_end) * m_heuristicsModifier;
        						
						        //om den fanns i closed plocka upp den igen...
						        //ta bort den ur closed
						        if (ExistInClosed(NewNode.m_node)) {
							        bool change = true;
							        while (change) {
								        change = false;
								        //List<Node>::iterator iter = m_listClosed.begin();
								        //for (; iter != m_listClosed.end(); iter++)
                                        foreach(Node iter in m_listClosed)
								        {
									        if (iter.m_node.X == NewNode.m_node.X && iter.m_node.Y == NewNode.m_node.Y)
									        {
                                                m_listClosed.Remove(iter);
										        change = true;
										        break;
									        }
								        }
							        }
						        }
						        //om den fanns i open plocka upp den igen...
						        //ta bort den ur closed
						        if (ExistInOpen( NewNode.m_node)) {
							        bool change = true;
							        while (change)
							        {
								        change = false;
                                        foreach( Node iter in m_listOpen)
								        {
									        if (iter.m_node.X == NewNode.m_node.X && iter.m_node.Y == NewNode.m_node.Y)
									        {
                                                m_listOpen.Remove(iter);
										        change = true;
										        break;
									        }
								        }
							        }
							        //och lägg den längst bak i open
                                    m_listOpen.Add(NewNode);
						        } else {
                                    m_listOpen.Add(NewNode);
						        }
					        }
				        }
			        }
		        }
		        //sortera listan
		        m_listOpen.Sort();
	        }

	        m_listClosed.Add(pNode);
	        return SearchResult.SearchNotDone;
        }

        bool IsAGoalNode(Node pNode)
        {
	        if (m_doesNearSearch == false) {
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

        bool ExistInOpen(Vector2 a_node) {
            foreach (Node iter in m_listOpen) {
		        if (iter.m_node.X == a_node.X && iter.m_node.Y == a_node.Y) {
			        return true;
		        }
	        }
	        return false;
        }

        bool ExistInClosed(Vector2 a_node) {
	        foreach (Node iter in m_listClosed) {
		        if (iter.m_node.X == a_node.X && iter.m_node.Y == a_node.Y) {
			        return true;
		        }
	        }
	        return false;
        }


        bool IsClear(Vector2 a_from, Vector2 a_to) {

	        return m_map.IsClear(a_to);
        }
    }
}

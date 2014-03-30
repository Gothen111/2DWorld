using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Server.Model.Object;

namespace Server.Model.Collison
{
    class QuadTree2
    {
          private int MAX_OBJECTS = 10;
          private int MAX_LEVELS = 5;
 
          private int level;
          private List<Object.AnimatedObject> objects;
          private Rectangle bounds;
          private QuadTree2[] nodes;
 
         /*
          * Constructor
          */
          public QuadTree2(int pLevel, Rectangle pBounds)
          {
           level = pLevel;
           objects = new List<Object.AnimatedObject>();
           bounds = pBounds;
           nodes = new QuadTree2[4];
          }

          /*
           * Clears the quadtree
           */
          public void clear()
          {
              objects.Clear();

              for (int i = 0; i < nodes.Length; i++)
              {
                  if (nodes[i] != null)
                  {
                      nodes[i].clear();
                      nodes[i] = null;
                  }
              }
          }

          /*
           * Splits the node into 4 subnodes
           */
          private void split()
          {
              int subWidth = (int)(bounds.Width / 2);
              int subHeight = (int)(bounds.Height / 2);
              int x = (int)bounds.X;
              int y = (int)bounds.Y;

              nodes[0] = new QuadTree2(level + 1, new Rectangle(x + subWidth, y, subWidth, subHeight));
              nodes[1] = new QuadTree2(level + 1, new Rectangle(x, y, subWidth, subHeight));
              nodes[2] = new QuadTree2(level + 1, new Rectangle(x, y + subHeight, subWidth, subHeight));
              nodes[3] = new QuadTree2(level + 1, new Rectangle(x + subWidth, y + subHeight, subWidth, subHeight));
          }

          /*
           * Determine which node the object belongs to. -1 means
           * object cannot completely fit within a child node and is part
           * of the parent node
           */
          private int getIndex(Object.AnimatedObject _AnimatedObject)
          {
              int index = -1;
              double verticalMidpoint = bounds.X + (bounds.Width / 2);
              double horizontalMidpoint = bounds.Y + (bounds.Height / 2);

              // Object can completely fit within the top quadrants
              bool topQuadrant = (_AnimatedObject.Position.Y < horizontalMidpoint && _AnimatedObject.Position.Y + _AnimatedObject.Size.Y < horizontalMidpoint);
              // Object can completely fit within the bottom quadrants
              bool bottomQuadrant = (_AnimatedObject.Position.Y > horizontalMidpoint);

              // Object can completely fit within the left quadrants
              if (_AnimatedObject.Position.X < verticalMidpoint && _AnimatedObject.Position.X + _AnimatedObject.Size.X < verticalMidpoint)
              {
                  if (topQuadrant)
                  {
                      index = 1;
                  }
                  else if (bottomQuadrant)
                  {
                      index = 2;
                  }
              }
              // Object can completely fit within the right quadrants
              else if (_AnimatedObject.Position.X > verticalMidpoint)
              {
                  if (topQuadrant)
                  {
                      index = 0;
                  }
                  else if (bottomQuadrant)
                  {
                      index = 3;
                  }
              }

              return index;
          }

          /*
           * Insert the object into the quadtree. If the node
           * exceeds the capacity, it will split and add all
           * objects to their corresponding nodes.
           */
          public void insert(Object.AnimatedObject _AnimatedObject)
          {
              if (nodes[0] != null)
              {
                  int index = getIndex(_AnimatedObject);

                  if (index != -1)
                  {
                      nodes[index].insert(_AnimatedObject);

                      return;
                  }
              }

              objects.Add(_AnimatedObject);

              if (objects.Count > MAX_OBJECTS && level < MAX_LEVELS)
              {
                  if (nodes[0] == null)
                  {
                      split();
                  }

                  int i = 0;
                  while (i < objects.Count)
                  {
                      int index = getIndex(objects.ElementAt(i));
                      if (index != -1)
                      {
                          Object.AnimatedObject var_AnimatedObject = objects.ElementAt(i);
                          nodes[index].insert(var_AnimatedObject);
                          objects.Remove(var_AnimatedObject);
                      }
                      else
                      {
                          i++;
                      }
                  }
              }
          }
          /*
           * Return all objects that could collide with the given object
           */
          public List<Object.AnimatedObject> retrieve(List<Object.AnimatedObject> returnObjects, Object.AnimatedObject _AnimatedObject)
          {
              int index = getIndex(_AnimatedObject);
              if (index != -1 && nodes[0] != null)
              {
                  nodes[index].retrieve(returnObjects, _AnimatedObject);
              }

              returnObjects.AddRange(objects);

              return returnObjects;
          }
    }
}

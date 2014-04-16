using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Server.Model.Object;


using Microsoft.Xna.Framework.Graphics;


// VERSION 2.0
namespace Server.Model.Collison.COPY
{
    class QuadTree
    {
          private int MAX_OBJECTS = 10;
          private int MAX_LEVELS = 5;
 
          private int level;
          private List<Object.AnimatedObject> objects;
          private Rectangle bounds;
          private QuadTree[] nodes;

        private const int TopRight = 0;
        private const int TopLeft = 1;
        private const int BottomLeft = 2;
        private const int BottomRight = 3;

        private QuadTree parent;

        private Color color;
        
        /*
          * Constructor
          */
        public QuadTree(int pLevel, Rectangle pBounds, QuadTree _Parent)
          {
           level = pLevel;
           objects = new List<Object.AnimatedObject>();
           bounds = pBounds;
           parent = _Parent;
           nodes = new QuadTree[4];

           //Random Rnd = new Random();
           color = new Color(Server.Util.Random.GenerateGoodRandomNumber(0, 255), Server.Util.Random.GenerateGoodRandomNumber(0, 255), Server.Util.Random.GenerateGoodRandomNumber(0, 255));

          }

          public QuadTree getQuadTreeAnimatedObjectIsIn(Object.AnimatedObject _AnimatedObject)
          {
              if (objects.Contains(_AnimatedObject))
              {
                  return this;
              }
              else
              {
                  double verticalMidpoint = bounds.X + (bounds.Width / 2);
                  double horizontalMidpoint = bounds.Y + (bounds.Height / 2);

                  // Object can completely fit within the top quadrants
                  bool topQuadrant = (_AnimatedObject.Position.Y < horizontalMidpoint);
                  // Object can completely fit within the bottom quadrants
                  bool bottomQuadrant = (_AnimatedObject.Position.Y > horizontalMidpoint);

                  // Object can completely fit within the left quadrants
                  if (_AnimatedObject.Position.X < verticalMidpoint)
                  {
                      if (topQuadrant)
                      {
                          if(nodes[TopLeft]!=null)
                          {
                              return nodes[TopLeft].getQuadTreeAnimatedObjectIsIn(_AnimatedObject);
                          }
                      }
                      else if (bottomQuadrant)
                      {
                          if (nodes[BottomLeft] != null)
                          {
                              return nodes[BottomLeft].getQuadTreeAnimatedObjectIsIn(_AnimatedObject);
                          }
                      }
                  }
                  // Object can completely fit within the right quadrants
                  else if (_AnimatedObject.Position.X > verticalMidpoint)
                  {
                      if (topQuadrant)
                      {
                          if (nodes[TopRight] != null)
                          {
                              return nodes[TopLeft].getQuadTreeAnimatedObjectIsIn(_AnimatedObject);
                          }
                      }
                      else if (bottomQuadrant)
                      {
                          if (nodes[BottomRight] != null)
                          {
                              return nodes[BottomRight].getQuadTreeAnimatedObjectIsIn(_AnimatedObject);
                          }
                      }
                  }
              }
              return null;
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
             * Clears the nodes
             */
          public void clearNodes()
          {
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

              nodes[TopRight] = new QuadTree(level + 1, new Rectangle(x + subWidth, y, subWidth, subHeight), this);
              nodes[TopLeft] = new QuadTree(level + 1, new Rectangle(x, y, subWidth, subHeight), this);
              nodes[BottomLeft] = new QuadTree(level + 1, new Rectangle(x, y + subHeight, subWidth, subHeight), this);
              nodes[BottomRight] = new QuadTree(level + 1, new Rectangle(x + subWidth, y + subHeight, subWidth, subHeight), this);
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
              bool topQuadrant = (_AnimatedObject.Position.Y < horizontalMidpoint);
              // Object can completely fit within the bottom quadrants
              bool bottomQuadrant = (_AnimatedObject.Position.Y > horizontalMidpoint);

              // Object can completely fit within the left quadrants
              if (_AnimatedObject.Position.X < verticalMidpoint)
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
          public void insert(Object.AnimatedObject _AnimatedObject) // insert gedöns //???
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

          private bool isObjectStillInBound(AnimatedObject _AnimatedObject)
          {
              if (_AnimatedObject.Position.X >= this.bounds.X && _AnimatedObject.Position.Y >= this.bounds.Y)
              {
                  if (_AnimatedObject.Position.X <= this.bounds.X + this.bounds.Width && _AnimatedObject.Position.Y <= this.bounds.Y + this.bounds.Height)
                  {
                      return true;
                  }
              }
              return false;
          }

          private bool giveAnimatedObjectToParent(AnimatedObject _AnimatedObject)
          {
              if (this.parent == null)
              {
                  // OBJECT VERLÄSST CHUNK!! //???
              }
              else
              {
                  if (this.parent.isObjectStillInBound(_AnimatedObject))
                  {
                      this.parent.insert(_AnimatedObject);
                      //this.remove(_AnimatedObject);
                      return true; // ???
                  }
                  else
                  {
                      return this.parent.giveAnimatedObjectToParent(_AnimatedObject);
                  }
              }
              return false; // ???
          }

          public void update()
          {
              List<AnimatedObject> var_AnimatedObjectsLeftThisTree = new List<AnimatedObject>();
              foreach (AnimatedObject var_AnimatedObject in objects)
              {
                  var_AnimatedObject.update(); //???

                  /*int movespeed = Server.Util.Random.GenerateGoodRandomNumber(0, 2) - 1;
                  if (var_AnimatedObject.Position.X + movespeed * 0.1f > 0)
                  {
                      if (var_AnimatedObject.Position.Y + movespeed * 0.1f > 0)
                      {
                          if (var_AnimatedObject.Position.X + movespeed * 0.1f < 20 * Server.Model.Map.Block.Block.BlockSize)
                          {
                              if (var_AnimatedObject.Position.Y + movespeed * 0.1f < 20 * Server.Model.Map.Block.Block.BlockSize)
                              {
                                  var_AnimatedObject.Position = new Vector3(var_AnimatedObject.Position.X + movespeed * 0.1f, var_AnimatedObject.Position.Y + movespeed * 0.1f, 0);
                              }
                          }
                      }
                  }*/
                  
                  
                  if (!isObjectStillInBound(var_AnimatedObject))
                  {
                      var_AnimatedObjectsLeftThisTree.Add(var_AnimatedObject);
                      //Logger.Logger.LogDeb("Size : " + var_AnimatedObjectsLeftThisTree.Count);
                  }
              }

              
              foreach (AnimatedObject var_AnimatedObject in var_AnimatedObjectsLeftThisTree)
              {
                  if (this.giveAnimatedObjectToParent(var_AnimatedObject))
                  {
                      this.objects.Remove(var_AnimatedObject);
                  }
                  //this.remove(var_AnimatedObject);
              }

              checkChildNodes();

              foreach (QuadTree var_Node in nodes)
              {
                  if (var_Node != null)
                  {
                      var_Node.update();
                  }
              }
          }

          public void checkChildNodes()
          {
              if (this.nodes[0] != null)
                  if (this.nodes[0].objects.Count + this.nodes[1].objects.Count + this.nodes[2].objects.Count + this.nodes[3].objects.Count <= MAX_OBJECTS)
                  {
                      if (this.nodes[0].nodes[0] == null && this.nodes[1].nodes[0] == null && this.nodes[2].nodes[0] == null && this.nodes[3].nodes[0] == null)
                      {
                          List<AnimatedObject> var_AnimatedObjectsLeftThisTree = new List<AnimatedObject>();

                          foreach (QuadTree var_Node in this.nodes)
                          {
                              foreach (AnimatedObject var_AnimatedObject in var_Node.objects)
                              {
                                  var_AnimatedObjectsLeftThisTree.Add(var_AnimatedObject);
                              }
                          }
                          //this.parent.clearNodes();
                          this.clearNodes();
                          foreach (AnimatedObject var_AnimatedObject in var_AnimatedObjectsLeftThisTree)
                          {
                              this.insert(var_AnimatedObject);
                          }
                          
                      }
                  }
          }

          public void remove(AnimatedObject _AnimatedObject)
          {
              //this.objects.Remove(_AnimatedObject);
              // noch viel anderes zeug wie z.b. nodes auflösen //???

              if (this.parent != null)
              {
                  if(this.parent.nodes[0] != null)
                  if (this.parent.nodes[0].objects.Count + this.parent.nodes[1].objects.Count + this.parent.nodes[2].objects.Count + this.parent.nodes[3].objects.Count <= MAX_OBJECTS)
                  {
                      if (this.nodes[0] == null)
                      {
                          List<AnimatedObject> var_AnimatedObjectsLeftThisTree = new List<AnimatedObject>();
              
                          foreach (QuadTree var_Node in this.parent.nodes)
                          {
                              foreach (AnimatedObject var_AnimatedObject in var_Node.objects)
                              {
                                  var_AnimatedObjectsLeftThisTree.Add(var_AnimatedObject);
                              }
                          }
                          //this.parent.clearNodes();

                          foreach (AnimatedObject var_AnimatedObject in var_AnimatedObjectsLeftThisTree)
                          {
                              this.parent.insert(var_AnimatedObject);
                          }
                          this.parent.clearNodes();

                      }
                  }
              }

          }


          public void DrawTest(GraphicsDevice _GraphicsDevice, SpriteBatch _SpriteBatch)
          {
             Texture2D texture = new Texture2D(_GraphicsDevice, 1, 1);
              texture.SetData<Color>(new Color[] { Color.White });

              _SpriteBatch.Draw(texture, this.bounds, color);

              foreach (AnimatedObject var_AnimatedObject in objects)
              {
                  var_AnimatedObject.draw(_GraphicsDevice, _SpriteBatch);
                  //_SpriteBatch.Draw(Ressourcen.RessourcenManager.ressourcenManager.Texture[var_AnimatedObject.GraphicPath], new Vector2(var_AnimatedObject.Position.X, var_AnimatedObject.Position.Y), new Rectangle(0,0,32,32), Color.White);
              }         

              foreach (QuadTree var_Node in nodes)
              {
                  if (var_Node != null)
                  {
                      var_Node.DrawTest(_GraphicsDevice, _SpriteBatch);
                  }
              }
          }

          public int getCountofAllObjects()
          {
              int var_Count = this.objects.Count;
              foreach (QuadTree var_Node in nodes)
              {
                  if(var_Node!=null)
                  var_Count += var_Node.getCountofAllObjects();
              }
              return var_Count;
          }
    }
}

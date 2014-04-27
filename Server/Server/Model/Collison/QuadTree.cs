using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Server.Model.Object;


using Microsoft.Xna.Framework.Graphics;



// VERSION 3.0
namespace Server.Model.Collison
{
    //class QuadTree
    //{
    //      private int MAX_OBJECTS = 10;
 
    //      private int level;
    //      private List<Object.LivingObject> objects;
    //      private Rectangle bounds;

    //      public Rectangle Bounds
    //      {
    //          get { return bounds; }
    //          set { bounds = value; }
    //      }
    //      private QuadTree[] nodes;

    //    private const int TopRight = 0;
    //    private const int TopLeft = 1;
    //    private const int BottomLeft = 2;
    //    private const int BottomRight = 3;

    //    private QuadTree parent;

    //    private Color color;
        
    //    /*
    //      * Constructor
    //      */
    //    public QuadTree(int pLevel, Rectangle pBounds, QuadTree _Parent)
    //      {
    //          level = pLevel;
    //          objects = new List<Object.LivingObject>();
    //          bounds = pBounds;
    //          parent = _Parent;
    //          nodes = new QuadTree[4];

    //          color = new Color(Server.Util.Random.GenerateGoodRandomNumber(0, 255), Server.Util.Random.GenerateGoodRandomNumber(0, 255), Server.Util.Random.GenerateGoodRandomNumber(0, 255));
    //      }

    //      public QuadTree getQuadTreeLivingObjectIsIn(Object.LivingObject _LivingObject)
    //      {
    //          if (objects.Contains(_LivingObject))
    //          {
    //              return this;
    //          }
    //          else
    //          {
    //              double verticalMidpoint = bounds.X + (bounds.Width / 2);
    //              double horizontalMidpoint = bounds.Y + (bounds.Height / 2);

    //              // Object can completely fit within the top quadrants
    //              bool topQuadrant = (_LivingObject.Position.Y < horizontalMidpoint);
    //              // Object can completely fit within the bottom quadrants
    //              bool bottomQuadrant = (_LivingObject.Position.Y > horizontalMidpoint);

    //              // Object can completely fit within the left quadrants
    //              if (_LivingObject.Position.X < verticalMidpoint)
    //              {
    //                  if (topQuadrant)
    //                  {
    //                      if(nodes[TopLeft]!=null)
    //                      {
    //                          return nodes[TopLeft].getQuadTreeLivingObjectIsIn(_LivingObject);
    //                      }
    //                  }
    //                  else if (bottomQuadrant)
    //                  {
    //                      if (nodes[BottomLeft] != null)
    //                      {
    //                          return nodes[BottomLeft].getQuadTreeLivingObjectIsIn(_LivingObject);
    //                      }
    //                  }
    //              }
    //              // Object can completely fit within the right quadrants
    //              else if (_LivingObject.Position.X > verticalMidpoint)
    //              {
    //                  if (topQuadrant)
    //                  {
    //                      if (nodes[TopRight] != null)
    //                      {
    //                          return nodes[TopLeft].getQuadTreeLivingObjectIsIn(_LivingObject);
    //                      }
    //                  }
    //                  else if (bottomQuadrant)
    //                  {
    //                      if (nodes[BottomRight] != null)
    //                      {
    //                          return nodes[BottomRight].getQuadTreeLivingObjectIsIn(_LivingObject);
    //                      }
    //                  }
    //              }
    //          }
    //          return null;
    //      }

    //      /*
    //       * Clears the quadtree
    //       */
    //      public void clear()
    //      {
    //          objects.Clear();

    //          for (int i = 0; i < nodes.Length; i++)
    //          {
    //              if (nodes[i] != null)
    //              {
    //                  nodes[i].clear();
    //                  nodes[i] = null;
    //              }
    //          }
    //      }

    //      /*
    //         * Clears the nodes
    //         */
    //      public void clearNodes()
    //      {
    //          for (int i = 0; i < nodes.Length; i++)
    //          {
    //              if (nodes[i] != null)
    //              {
    //                  nodes[i].clear();
    //                  nodes[i] = null;
    //              }
    //          }
    //      }


    //      /*
    //       * Splits the node into 4 subnodes
    //       */
    //      private void split()
    //      {
    //          int subWidth = (int)(bounds.Width / 2);
    //          int subHeight = (int)(bounds.Height / 2);
    //          int x = (int)bounds.X;
    //          int y = (int)bounds.Y;

    //          nodes[TopRight] = new QuadTree(level + 1, new Rectangle(x + subWidth, y, subWidth, subHeight), this);
    //          nodes[TopLeft] = new QuadTree(level + 1, new Rectangle(x, y, subWidth, subHeight), this);
    //          nodes[BottomLeft] = new QuadTree(level + 1, new Rectangle(x, y + subHeight, subWidth, subHeight), this);
    //          nodes[BottomRight] = new QuadTree(level + 1, new Rectangle(x + subWidth, y + subHeight, subWidth, subHeight), this);
    //      }

    //      /*
    //         * Determine which node the object belongs to. -1 means
    //         * object cannot completely fit within a child node and is part
    //         * of the parent node
    //         */
    //      private int getIndex(Object.LivingObject _LivingObject)
    //      {
    //          int index = -1;
    //          double verticalMidpoint = bounds.X + (bounds.Width / 2);
    //          double horizontalMidpoint = bounds.Y + (bounds.Height / 2);

    //          // Object can completely fit within the top quadrants
    //          bool topQuadrant = (_LivingObject.Position.Y < horizontalMidpoint);
    //          // Object can completely fit within the bottom quadrants
    //          bool bottomQuadrant = (_LivingObject.Position.Y > horizontalMidpoint);

    //          // Object can completely fit within the left quadrants
    //          if (_LivingObject.Position.X < verticalMidpoint)
    //          {
    //              if (topQuadrant)
    //              {
    //                  index = 1;
    //              }
    //              else if (bottomQuadrant)
    //              {
    //                  index = 2;
    //              }
    //          }
    //          // Object can completely fit within the right quadrants
    //          else if (_LivingObject.Position.X > verticalMidpoint)
    //          {
    //              if (topQuadrant)
    //              {
    //                  index = 0;
    //              }
    //              else if (bottomQuadrant)
    //              {
    //                  index = 3;
    //              }
    //          }

    //          return index;
    //      }

    //      /*
    //       * Insert the object into the quadtree. If the node
    //       * exceeds the capacity, it will split and add all
    //       * objects to their corresponding nodes.
    //       */
    //      public void insert(Object.LivingObject _LivingObject) // insert gedöns //???
    //      {
    //          if (nodes[0] != null)
    //          {
    //              int index = getIndex(_LivingObject);

    //              if (index != -1)
    //              {
    //                  nodes[index].insert(_LivingObject);

    //                  return;
    //              }
    //          }

    //          objects.Add(_LivingObject);

    //          if (objects.Count > MAX_OBJECTS)
    //          {
    //              if (nodes[0] == null)
    //              {
    //                  split();
    //              }

    //              int i = 0;
    //              while (i < objects.Count)
    //              {
    //                  int index = getIndex(objects.ElementAt(i));
    //                  if (index != -1)
    //                  {
    //                      Object.LivingObject var_LivingObject = objects.ElementAt(i);
    //                      nodes[index].insert(var_LivingObject);
    //                      objects.Remove(var_LivingObject);
    //                  }
    //                  else
    //                  {
    //                      i++;
    //                  }
    //              }
    //          }
    //      }

    //      private bool isObjectStillInBound(LivingObject _LivingObject)
    //      {
    //          if (_LivingObject.Position.X >= this.bounds.X && _LivingObject.Position.Y >= this.bounds.Y)
    //          {
    //              if (_LivingObject.Position.X <= this.bounds.X + this.bounds.Width && _LivingObject.Position.Y <= this.bounds.Y + this.bounds.Height)
    //              {
    //                  return true;
    //              }
    //          }
    //          return false;
    //      }

    //      private bool giveLivingObjectToParent(LivingObject _LivingObject)
    //      {
    //          if (this.parent == null)
    //          {
    //              // OBJECT VERLÄSST CHUNK!! //???
    //          }
    //          else
    //          {
    //              if (this.parent.isObjectStillInBound(_LivingObject))
    //              {
    //                  this.parent.insert(_LivingObject);
    //                  //this.remove(_LivingObject);
    //                  return true; // ???
    //              }
    //              else
    //              {
    //                  return this.parent.giveLivingObjectToParent(_LivingObject);
    //              }
    //          }
    //          return false; // ???
    //      }

    //      public void update()
    //      {
    //          List<LivingObject> var_LivingObjectsLeftThisTree = new List<LivingObject>();
    //          List<LivingObject> var_LivingObjectsToRemove = new List<LivingObject>();
    //          foreach (LivingObject var_LivingObject in objects)
    //          {
    //              var_LivingObject.update();
    //              if (var_LivingObject.IsDead)
    //              {
    //                  var_LivingObjectsToRemove.Add(var_LivingObject);
    //              }
    //              else
    //              {
    //                  if (!isObjectStillInBound(var_LivingObject))
    //                  {
    //                      var_LivingObjectsLeftThisTree.Add(var_LivingObject);
    //                  }
    //              }
    //          }
    //          foreach (LivingObject var_LivingObject in var_LivingObjectsToRemove)
    //          {
    //              this.objects.Remove(var_LivingObject);
    //          }

              
    //          foreach (LivingObject var_LivingObject in var_LivingObjectsLeftThisTree)
    //          {
    //              if (this.giveLivingObjectToParent(var_LivingObject))
    //              {
    //                  this.objects.Remove(var_LivingObject);
    //              }
    //          }

    //          checkChildNodes();

    //          foreach (QuadTree var_Node in nodes)
    //          {
    //              if (var_Node != null)
    //              {
    //                  var_Node.update();
    //              }
    //          }
    //      }

    //      public void checkChildNodes()
    //      {
    //          if (this.nodes[0] != null)
    //              if (this.nodes[0].objects.Count + this.nodes[1].objects.Count + this.nodes[2].objects.Count + this.nodes[3].objects.Count <= MAX_OBJECTS)
    //              {
    //                  if (this.nodes[0].nodes[0] == null && this.nodes[1].nodes[0] == null && this.nodes[2].nodes[0] == null && this.nodes[3].nodes[0] == null)
    //                  {
    //                      List<LivingObject> var_LivingObjectsLeftThisTree = new List<LivingObject>();

    //                      foreach (QuadTree var_Node in this.nodes)
    //                      {
    //                          foreach (LivingObject var_LivingObject in var_Node.objects)
    //                          {
    //                              var_LivingObjectsLeftThisTree.Add(var_LivingObject);
    //                          }
    //                      }
    //                      //this.parent.clearNodes();
    //                      this.clearNodes();
    //                      foreach (LivingObject var_LivingObject in var_LivingObjectsLeftThisTree)
    //                      {
    //                          this.insert(var_LivingObject);
    //                      }
                          
    //                  }
    //              }
    //      }

    //      public void remove(LivingObject _LivingObject)
    //      {
    //          //this.objects.Remove(_LivingObject);
    //          // noch viel anderes zeug wie z.b. nodes auflösen //???

    //          if (this.parent != null)
    //          {
    //              if(this.parent.nodes[0] != null)
    //              if (this.parent.nodes[0].objects.Count + this.parent.nodes[1].objects.Count + this.parent.nodes[2].objects.Count + this.parent.nodes[3].objects.Count <= MAX_OBJECTS)
    //              {
    //                  if (this.nodes[0] == null)
    //                  {
    //                      List<LivingObject> var_LivingObjectsLeftThisTree = new List<LivingObject>();
              
    //                      foreach (QuadTree var_Node in this.parent.nodes)
    //                      {
    //                          foreach (LivingObject var_LivingObject in var_Node.objects)
    //                          {
    //                              var_LivingObjectsLeftThisTree.Add(var_LivingObject);
    //                          }
    //                      }
    //                      //this.parent.clearNodes();

    //                      foreach (LivingObject var_LivingObject in var_LivingObjectsLeftThisTree)
    //                      {
    //                          this.parent.insert(var_LivingObject);
    //                      }
    //                      this.parent.clearNodes();

    //                  }
    //              }
    //          }

    //      }


    //      public void DrawTest(GraphicsDevice _GraphicsDevice, SpriteBatch _SpriteBatch)
    //      {
    //         /*Texture2D texture = new Texture2D(_GraphicsDevice, 1, 1);
    //          texture.SetData<Color>(new Color[] { Color.White });

    //          _SpriteBatch.Draw(texture, this.bounds, color);*/

    //          foreach (LivingObject var_LivingObject in objects)
    //          {
    //              var_LivingObject.draw(_GraphicsDevice, _SpriteBatch, new Vector3(0,0,0), Color.White);
    //              //_SpriteBatch.Draw(Ressourcen.RessourcenManager.ressourcenManager.Texture[var_LivingObject.GraphicPath], new Vector2(var_LivingObject.Position.X, var_LivingObject.Position.Y), new Rectangle(0,0,32,32), Color.White);
    //          }         

    //          foreach (QuadTree var_Node in nodes)
    //          {
    //              if (var_Node != null)
    //              {
    //                  var_Node.DrawTest(_GraphicsDevice, _SpriteBatch);
    //              }
    //          }
    //      }

    //      public int getCountofAllObjects()
    //      {
    //          int var_Count = this.objects.Count;
    //          foreach (QuadTree var_Node in nodes)
    //          {
    //              if(var_Node!=null)
    //              var_Count += var_Node.getCountofAllObjects();
    //          }
    //          return var_Count;
    //      }

    //      public List<Object.LivingObject> getAllLivingObjects(List<Object.LivingObject> var_LivingObjects)
    //      {
    //          var_LivingObjects.AddRange(this.objects);
    //          foreach (QuadTree var_Node in nodes)
    //          {
    //              if(var_Node!=null)
    //              {
    //                  var_Node.getAllLivingObjects(var_LivingObjects);
    //              }
    //          }
    //          return var_LivingObjects;
    //      }
    //}
}

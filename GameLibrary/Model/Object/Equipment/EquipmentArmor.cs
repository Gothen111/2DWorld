using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

using GameLibrary.Factory.FactoryEnums;

namespace GameLibrary.Model.Object.Equipment
{
    [Serializable()]
    public class EquipmentArmor : EquipmentObject
    {
        private ArmorEnum armorEnum;

        public ArmorEnum ArmorEnum
        {
          get { return armorEnum; }
          set { armorEnum = value; }
        }

        private int normalArmor;

        public int NormalArmor
        {
            get { return normalArmor; }
            set { normalArmor = value; }
        }

        private List<Map.World.SearchFlags.Searchflag> searchFlags;

        public List<Map.World.SearchFlags.Searchflag> SearchFlags
        {
            get { return searchFlags; }
            set { searchFlags = value; }
        }

        public EquipmentArmor()
        {
            searchFlags = new List<Map.World.SearchFlags.Searchflag>();
        }

        public EquipmentArmor(SerializationInfo info, StreamingContext ctxt)
            : base(info, ctxt)
        {
            this.armorEnum = (ArmorEnum)info.GetValue("armorEnum", typeof(ArmorEnum));

            this.normalArmor = (int)info.GetValue("normalArmor", typeof(int));

            this.searchFlags = (List<Map.World.SearchFlags.Searchflag>)info.GetValue("searchFlags", typeof(List<Map.World.SearchFlags.Searchflag>));
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("armorEnum", armorEnum, typeof(ArmorEnum));

            info.AddValue("normalArmor", normalArmor, typeof(int));

            info.AddValue("searchFlags", this.searchFlags, typeof(List<Map.World.SearchFlags.Searchflag>));

            base.GetObjectData(info, ctxt);
        }

        public override void update()
        {

        }

        public override void draw(Microsoft.Xna.Framework.Graphics.GraphicsDevice _GraphicsDevice, Microsoft.Xna.Framework.Graphics.SpriteBatch _SpriteBatch, Microsoft.Xna.Framework.Vector3 _DrawPositionExtra, Microsoft.Xna.Framework.Color _Color)
        {
            if (this.Animation != null && !this.Animation.graphicPath().Equals(""))
            {
                _SpriteBatch.Draw(Ressourcen.RessourcenManager.ressourcenManager.Texture["Character/Cloth1"], new Microsoft.Xna.Framework.Vector2(this.Position.X, this.Position.Y), this.Animation.sourceRectangle(), this.Animation.drawColor(), 0f, Microsoft.Xna.Framework.Vector2.Zero, new Microsoft.Xna.Framework.Vector2(this.Scale, this.Scale), Microsoft.Xna.Framework.Graphics.SpriteEffects.None, this.LayerDepth);
            }
        }
    }
}

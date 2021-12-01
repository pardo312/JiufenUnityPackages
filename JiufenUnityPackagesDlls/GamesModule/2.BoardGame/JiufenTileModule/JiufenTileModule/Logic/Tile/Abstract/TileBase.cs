using UnityEngine;

namespace JiufenGames.TetrisAlike.Logic
{
    public abstract class TileBase : MonoBehaviour, ITile
    {
        #region Fields
        #region Interface 
        #region BackingFields
        private int m_tileRowField;
        private int m_tileColumnField;
        private object m_tileDataField = null;
        #endregion BackingFields

        #region Properties
        public int m_tileRow { get => m_tileRowField; set => m_tileRowField = value; }
        public int m_tileColumn { get => m_tileColumnField; set => m_tileColumnField = value; }
        public object m_tileData { get => m_tileDataField; set => m_tileDataField = value; }
        #endregion Properties
        #endregion Interface 
        #endregion Fields

        #region Methods
        public abstract void Awake();


        public virtual void ResetTile()
        {
            ChangeTileData();
        }

        public virtual object ChangeTileData(object _payload  = null)
        {
            if (_payload == null)
                return GetDefaultTileData();
            return _payload;
        }

        public abstract object GetDefaultTileData();


        #endregion Methods
    }
}
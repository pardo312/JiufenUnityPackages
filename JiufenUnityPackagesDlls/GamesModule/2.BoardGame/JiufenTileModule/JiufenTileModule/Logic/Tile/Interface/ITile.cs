using UnityEngine;

namespace JiufenGames.TetrisAlike.Logic
{
    public interface ITile
    {
        int m_tileRow { get; set; }
        int m_tileColumn { get; set; }

        object ChangeTileData(object _payload = null);
        void ResetTile();
    }
}
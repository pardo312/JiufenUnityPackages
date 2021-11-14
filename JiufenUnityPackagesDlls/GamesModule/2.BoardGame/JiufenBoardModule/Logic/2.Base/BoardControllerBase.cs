using System;
using System.Collections.Generic;
using UnityEngine;

namespace JiufenGames.Board.Logic
{
    public abstract class BoardControllerBase<T> : MonoBehaviour, IBoardController<T>
    {
        #region Fields
        #region Interface
        #region BackingFields
        [Header("Necessary References")]
        [SerializeField] private GameObject m_tilePrefabField;
        [SerializeField] private Transform m_tileParentField;
        [Header("Board")]
        private T[,] m_boardField;
        #endregion BackingFields

        #region Properties
        public GameObject m_tilePrefab { get => m_tilePrefabField; }
        public Transform m_tileParent { get => m_tileParentField; }
        public T[,] m_board { get => m_boardField; set => m_boardField = value; }
        #endregion Properties
        #endregion Interface
        #endregion Fields

        #region Methods
        public abstract void Init();

        public virtual void CreateBoard(object payload, Action<int, int> _createdTile = null)
        {
            BaseBoardPayload boardPayload;
            if (payload.GetType() != typeof(BaseBoardPayload))
                return;
            boardPayload = payload as BaseBoardPayload;

            m_board = new T[boardPayload._rows, boardPayload._columns];
            for (int i = 0; i < boardPayload._rows; i++)
                for (int j = 0; j < boardPayload._columns; j++)
                {
                    GameObject instancedGO = Instantiate(m_tilePrefab, m_tileParent.position + new Vector3(j * (1 * boardPayload._offsetTiles), i * (1 * boardPayload._offsetTiles), 0), Quaternion.identity, m_tileParent);
                    m_board[i, j] = instancedGO.GetComponent<T>();
                    instancedGO.transform.localScale = m_tileParent.localScale;
                    _createdTile?.Invoke(i, j);
                }
        }

        #endregion Methods
    }
}
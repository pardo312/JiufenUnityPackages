using System;
using System.Collections.Generic;
using UnityEngine;

namespace JiufenGames.Board.Logic
{
    public abstract class BoardControllerFullContainerBase<T> : BoardControllerBase<T>
    {
        public override void CreateBoard(object payload, Action<int, int> _createdTile = null)
        {
            //Init Payload
            BaseBoardPayload boardPayload;
            if (payload.GetType() != typeof(BaseBoardPayload))
                return;
            boardPayload = payload as BaseBoardPayload;

            //Set variables
            float offsetTilesCanvas = boardPayload._offsetTiles * 100;
            RectTransform parentRectTransform = m_tileParent.GetComponent<RectTransform>();

            //Get parent sizeDelta
            float parentWidth = parentRectTransform.rect.width;
            float parentHeight = parentRectTransform.rect.height;

            //Set sizes for childs
            Vector2 sizeForChildsWithOffset = new Vector2((parentWidth / boardPayload._columns) + offsetTilesCanvas, (parentHeight / boardPayload._rows) + offsetTilesCanvas);
            Vector2 sizeForChilds = sizeForChildsWithOffset - new Vector2(offsetTilesCanvas * boardPayload._columns, offsetTilesCanvas * boardPayload._columns);

            //InitBoard
            m_board = new T[boardPayload._rows, boardPayload._columns];
            for (int i = 0; i < boardPayload._rows; i++)
                for (int j = 0; j < boardPayload._columns; j++)
                {
                    GameObject instancedGO = Instantiate(m_tilePrefab, parentRectTransform.anchoredPosition + new Vector2(j * sizeForChildsWithOffset.x, i * sizeForChildsWithOffset.y), Quaternion.identity, m_tileParent);
                    instancedGO.GetComponent<RectTransform>().sizeDelta = sizeForChilds;

                    m_board[i, j] = instancedGO.GetComponent<T>();
                    _createdTile?.Invoke(i, j);
                }
        }

    }
}
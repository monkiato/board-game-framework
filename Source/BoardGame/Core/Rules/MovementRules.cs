using System.Collections.Generic;
using Monkiato.BoardGame.Core.Table.MapTile;
using Monkiato.BoardGame.Core.Unit;

namespace Monkiato.BoardGame.Core.Rules
{
    public class MovementRules
    {
        public MovementRules(bool orthogonalAllowed, bool diagonalAllowed)
        {
            OrthogonalAllowed = orthogonalAllowed;
            DiagonalAllowed = diagonalAllowed;
        }

        public bool DiagonalAllowed { get; set; }

        public bool OrthogonalAllowed { get; set; }

        public List<ITile> GetValidMoves(IMapTileBoard mapTileBoard, IMiniature miniature, int distance)
        {
            return null;
        }
    }
}
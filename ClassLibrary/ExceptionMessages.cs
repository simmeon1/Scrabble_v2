using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public static class ExceptionMessages
    {
        public const string BoardMustHaveAtLeastOneRow = "The board must have at least one row.";
        public const string BoardMustHaveAtLeastOneColumn = "The board must have at least one column.";
        public const string SpecifiedRowPositionIsNotAvailableInTheBoard = "The specified row position is not available in the board.";
        public const string SpecifiedColumnPositionIsNotAvailableInTheBoard = "The specified column position is not available in the board.";
        public const string LetterCanOnlyBeBetweenAAndZ = "The letter can only be between A and Z.";
        public const string ScoreMustBeGreaterThan0 = "The score must be greater than 0.";
        public const string BoardTilesAreNotVerticallyConnected = "The board tiles are not vertically connected.";
        public const string ABoardWordMustConsistOf2OrMoreLetters = "A board word must consist of 2 or more letters.";
        public const string BoardTilesAreNotHorizontallyConnected = "The board tiles are not horizontally connected.";
    }
}

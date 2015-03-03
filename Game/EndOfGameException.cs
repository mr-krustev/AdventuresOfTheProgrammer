using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class EndOfGameException : ApplicationException
{
    public bool GameWon { get; set; }

    public EndOfGameException(bool gameWon)
    {
        this.GameWon = gameWon;
    }

}


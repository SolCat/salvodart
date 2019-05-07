using System;

namespace Akinator_Peintures
{
    partial class Question:IComparable<Question>
    {
        public int id { get; protected set; }
        public int nbOuiNSP { get; protected set; }
        public int nbNonNSP { get; protected set; }
        public bool pertinence { get; protected set; }
        public int diffOuiNonNSP { get; protected set; }
        public int sommeOuiNonNSP { get; protected set; }

        public Question(int _id, int _nbOui, int _nbNon)
        {
            id = _id;
            nbOuiNSP = _nbOui;
            nbNonNSP = _nbNon;
            sommeOuiNonNSP = CalculerSommeOuiNon();
            diffOuiNonNSP = CalculerEcartOuiNon();
            pertinence = false;
        }
        public int CalculerSommeOuiNon()
        {
            return nbOuiNSP + nbNonNSP;
        }
        public int CalculerEcartOuiNon()
        {
            return Math.Abs(nbOuiNSP - nbNonNSP);
        }

        public int CompareTo(Question q)
        {
            return this.CalculerSommeOuiNon().CompareTo(q.CalculerSommeOuiNon());
        }
    }
}
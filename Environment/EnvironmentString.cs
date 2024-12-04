using Ideal.Coupling.Interaction;
using Ideal.Existence;

namespace Ideal.Environment
{
    /// <summary>
    /// This class implements the String Environment. Used by Georgeon & Hassas in their paper "Single agents can be constructivist too".
    /// </summary>
    public class EnvironmentString : Environment050
    {
        public const string LABEL_STEP = ">";
        public const string LABEL_FEEL = "-";
        public const string LABEL_SWAP = "i";
        public const string LABEL_TRUE = "t";
        public const string LABEL_FALSE = "f";

        private const int WIDTH = 20;
        private int position = 0;

        private int[] board = { 6, 3, 5, 4, 7, 3, 5, 3, 1, 5, 6, 3, 5, 4, 7, 3, 5, 3, 9, 5 };
        //private int[] board = {6, 3, 5, 4, 7, 3, 5, 3, 9, 5};	

        public EnvironmentString(Existence050 existence) : base(existence)
        {
            existence.AddOrGetPrimitiveInteraction(">t", 4);   // step_up
            existence.AddOrGetPrimitiveInteraction(">f", -10); // step_down
            existence.AddOrGetPrimitiveInteraction("-t", -4);  // feel_up
            existence.AddOrGetPrimitiveInteraction("-f", -4);  // feel_down
            existence.AddOrGetPrimitiveInteraction("it", 4);   // swap
            existence.AddOrGetPrimitiveInteraction("if", -10); // not_swp
        }

        public override Interaction Enact(Interaction intendedInteraction)
        {
            //TraceEnv();

            Interaction040 enactedInteraction = null;

            if (intendedInteraction == this.GetExistence().AddOrGetPrimitiveInteraction(">t", 0) ||
                intendedInteraction == this.GetExistence().AddOrGetPrimitiveInteraction(">f", 0))
                enactedInteraction = Step();
            else if (intendedInteraction == this.GetExistence().AddOrGetPrimitiveInteraction("-t", 0) ||
                    intendedInteraction == this.GetExistence().AddOrGetPrimitiveInteraction("-f", 0))
                enactedInteraction = Feel();
            else if (intendedInteraction == this.GetExistence().AddOrGetPrimitiveInteraction("it", 0) ||
                    intendedInteraction == this.GetExistence().AddOrGetPrimitiveInteraction("if", 0))
                enactedInteraction = Swap();

            return enactedInteraction;
        }

        /**
 * Step forward
 * @return true if the agent went up
 */
        private Interaction040 Step()
        {
            Interaction040 enactedInteraction = this.GetExistence().AddOrGetPrimitiveInteraction(">f", 0);

            if (position < WIDTH - 1)
            {
                if (board[position] <= board[position + 1])
                    enactedInteraction = this.GetExistence().AddOrGetPrimitiveInteraction(">t", 0);
                position++;
            }
            else
                position = 0;

            return enactedInteraction;
        }

        /**
         * @return true if the next item is greater than the current item
         */
        private Interaction040 Feel()
        {
            Interaction040 enactedInteraction = this.GetExistence().AddOrGetPrimitiveInteraction("-f", 0);

            if ((position < WIDTH - 1) && (board[position] <= board[position + 1]))
                enactedInteraction = this.GetExistence().AddOrGetPrimitiveInteraction(">t", 0);

            return enactedInteraction;
        }

        /**
         * Invert the next item and the current item
         * @return true if the next item is greater than the current item
         */
        private Interaction040 Swap()
        {
            Interaction040 enactedInteraction = this.GetExistence().AddOrGetPrimitiveInteraction("if", 0);

            int temp = board[position];
            if ((position < WIDTH - 1) && (board[position] > board[position + 1]))
            {
                board[position] = board[position + 1];
                board[position + 1] = temp;
                enactedInteraction = this.GetExistence().AddOrGetPrimitiveInteraction("it", 0);
            }

            return enactedInteraction;
        }

        //private void TraceEnv()
        //{
        //    Element e = Trace.AddEventElement("environment");

        //    // print the board
        //    string stringBoard = "";
        //    for (int i = 0; i < WIDTH; i++)
        //        stringBoard += this.board[i] + " ";
        //    Trace.AddSubelement(e, "board", stringBoard);

        //    // print the agent
        //    string stringAgent = "";
        //    for (int i = 0; i < position; i++)
        //        stringAgent += ".. ";
        //    stringAgent += ">";
        //    Trace.AddSubelement(e, "agent", stringAgent);
        //}
    }
}

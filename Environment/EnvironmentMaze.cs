using Ideal.Coupling.Interaction;
using Ideal.Existence;

namespace Ideal.Environment
{
    /// <summary>
    /// This class implements the Small Loop Environment
    /// The Small Loop Problem: A challenge for artificial emergent cognition.
    /// Olivier L.Georgeon, James B.Marshall.
    /// BICA2012, Annual Conference on Biologically Inspired Cognitive Architectures.
    /// Palermo, Italy. (October 31, 2012).
    /// </summary>
    public class EnvironmentMaze : Environment050
    {
        private const int ORIENTATION_UP = 0;
        private const int ORIENTATION_RIGHT = 1;
        private const int ORIENTATION_DOWN = 2;
        private const int ORIENTATION_LEFT = 3;
        // The Small Loop Environment
        private const int WIDTH = 6;
        private const int HEIGHT = 6;
        private int m_x = 4;
        private int m_y = 1;
        private int m_o = 2;

        private char[,] _board = new char[,]
        {
            {'x', 'x', 'x', 'x', 'x', 'x'},
            {'x', ' ', ' ', ' ', ' ', 'x'},
            {'x', ' ', 'x', 'x', ' ', 'x'},
            {'x', ' ', ' ', 'x', ' ', 'x'},
            {'x', 'x', ' ', ' ', ' ', 'x'},
            {'x', 'x', 'x', 'x', 'x', 'x'}
        };

        private char[] m_agent =
        { '^', '>', 'v', '<' };

        public EnvironmentMaze(Existence050 existence) : base(existence) {  }

        protected override void Init()
        {
            // Settings for a nice demo in the Simple Maze 
            Interaction040 turnLeft = this.GetExistence().AddOrGetPrimitiveInteraction("^t", -3); // Left toward empty
            Interaction040 turnRight = this.GetExistence().AddOrGetPrimitiveInteraction("vt", -3); // Right toward empty
            Interaction040 touchRight = this.GetExistence().AddOrGetPrimitiveInteraction("\\t", -1); // Touch right wall
            this.GetExistence().AddOrGetPrimitiveInteraction("\\f", -1); // Touch right empty
            Interaction040 touchLeft = this.GetExistence().AddOrGetPrimitiveInteraction("/t", -1); // Touch left wall
            this.GetExistence().AddOrGetPrimitiveInteraction("/f", -1); // Touch left empty
            Interaction040 forward = this.GetExistence().AddOrGetPrimitiveInteraction(">t", 5); // Move
            this.GetExistence().AddOrGetPrimitiveInteraction(">f", -10); // Bump        
            Interaction040 touchForward = this.GetExistence().AddOrGetPrimitiveInteraction("-t", -1); // Touch wall
            this.GetExistence().AddOrGetPrimitiveInteraction("-f", -1); // Touch empty
            this.GetExistence().AddOrGetAbstractExperience(turnLeft);
            this.GetExistence().AddOrGetAbstractExperience(turnRight);
            this.GetExistence().AddOrGetAbstractExperience(touchRight);
            this.GetExistence().AddOrGetAbstractExperience(touchLeft);
            this.GetExistence().AddOrGetAbstractExperience(forward);
            this.GetExistence().AddOrGetAbstractExperience(touchForward);
        }

        public override Interaction Enact(Interaction intendedInteraction)
        {
            Interaction040 enactedInteraction = null;

            if (intendedInteraction.GetLabel().Substring(0, 1) == ">")
                enactedInteraction = Move();
            else if (intendedInteraction.GetLabel().Substring(0, 1) == "^")
                enactedInteraction = Left();
            else if (intendedInteraction.GetLabel().Substring(0, 1) == "v")
                enactedInteraction = Right();
            else if (intendedInteraction.GetLabel().Substring(0, 1) == "-")
                enactedInteraction = Touch();
            else if (intendedInteraction.GetLabel().Substring(0, 1) == "\\")
                enactedInteraction = TouchRight();
            else if (intendedInteraction.GetLabel().Substring(0, 1) == "/")
                enactedInteraction = TouchLeft();

            // Print the maze.
            for (int i = 0; i < HEIGHT; i++)
            {
                for (int j = 0; j < WIDTH; j++)
                {
                    if (i == m_y && j == m_x)
                        Console.Write(m_agent[m_o]);
                    else
                    if ((m_o == ORIENTATION_UP) && (m_y > 0) && (_board[m_y - 1, m_x] == ' '))
                    {
                        m_y--;
                        enactedInteraction = this.GetExistence().AddOrGetPrimitiveInteraction(">t", 0);
                    }

                    if ((m_o == ORIENTATION_DOWN) && (m_y < HEIGHT - 1) && (_board[m_y + 1, m_x] == ' '))
                    {
                        m_y++;
                        enactedInteraction = this.GetExistence().AddOrGetPrimitiveInteraction(">t", 0);
                    }

                    if ((m_o == ORIENTATION_RIGHT) && (m_x < WIDTH - 1) && (_board[m_y, m_x + 1] == ' '))
                    {
                        m_x++;
                        enactedInteraction = this.GetExistence().AddOrGetPrimitiveInteraction(">t", 0);
                    }
                    if ((m_o == ORIENTATION_LEFT) && (m_x > 0) && (_board[m_y, m_x - 1] == ' '))
                    {
                        m_x--;
                        enactedInteraction = this.GetExistence().AddOrGetPrimitiveInteraction(">t", 0);
                    }
                    Console.Write(_board[i, j]);
                }
                Console.WriteLine();
            }

            return enactedInteraction;
        }
        /// <summary>
        /// Turn to the right.
        /// </summary>
        /// <returns>An additive interaction.</returns>
        private Interaction040 Right()
        {
            m_o++;
            if (m_o > ORIENTATION_LEFT)
                m_o = ORIENTATION_UP;

            return this.GetExistence().AddOrGetPrimitiveInteraction("vt", 0);
        }
        /// <summary>
        /// Turn to the left.
        /// </summary>
        /// <returns>An additive interaction.</returns>
        private Interaction040 Left()
        {
            m_o--;
            if (m_o < 0)
                m_o = ORIENTATION_LEFT;

            return this.GetExistence().AddOrGetPrimitiveInteraction("^t", 0);
        }
        /// <summary>
        /// Move forward to the direction of the current orientation.
        /// </summary>
        /// <returns>An additive interaction.</returns>
        private Interaction040 Move()
        {
            Interaction040 enactedInteraction = this.GetExistence().AddOrGetPrimitiveInteraction(">f", 0);

            if ((m_o == ORIENTATION_UP) && (m_y > 0) && (_board[m_y - 1, m_x] == ' '))
            {
                m_y--;
                enactedInteraction = this.GetExistence().AddOrGetPrimitiveInteraction(">t", 0);
            }

            if ((m_o == ORIENTATION_DOWN) && (m_y < HEIGHT) && (_board[m_y + 1, m_x] == ' '))
            {
                m_y++;
                enactedInteraction = this.GetExistence().AddOrGetPrimitiveInteraction(">t", 0);
            }

            if ((m_o == ORIENTATION_RIGHT) && (m_x < WIDTH) && (_board[m_y, m_x + 1] == ' '))
            {
                m_x++;
                enactedInteraction = this.GetExistence().AddOrGetPrimitiveInteraction(">t", 0);
            }
            if ((m_o == ORIENTATION_LEFT) && (m_x > 0) && (_board[m_y, m_x - 1] == ' '))
            {
                m_x--;
                enactedInteraction = this.GetExistence().AddOrGetPrimitiveInteraction(">t", 0);
            }
            {
                m_x--;
                enactedInteraction = this.GetExistence().AddOrGetPrimitiveInteraction(">t", 0);
            }

            return enactedInteraction;
        }
        /// <summary>
        /// Touch the square forward.
        /// </summary>
        /// <returns>Succeeds if there is a wall, fails otherwise</returns>
        private Interaction040 Touch()
        {
            Interaction040 enactedInteraction = this.GetExistence().AddOrGetPrimitiveInteraction("-t", 0);

            if ((m_o == ORIENTATION_UP && m_y > 0 && _board[m_y - 1, m_x] == ' ') ||
                (m_o == ORIENTATION_DOWN && m_y < HEIGHT && _board[m_y + 1, m_x] == ' ') ||
                (m_o == ORIENTATION_RIGHT && m_x < WIDTH && _board[m_y, m_x + 1] == ' ') ||
                (m_o == ORIENTATION_LEFT && m_x > 0 && _board[m_y, m_x - 1] == ' '))
            {
                enactedInteraction = this.GetExistence().AddOrGetPrimitiveInteraction("-f", 0);
            }

            return enactedInteraction;
        }
        /// <summary>
        /// Touch the square to the right.
        /// </summary>
        /// <returns>Succeeds if there is a wall, fails otherwise</returns>
        private Interaction040 TouchRight()
        {
            Interaction040 enactedInteraction = this.GetExistence().AddOrGetPrimitiveInteraction("\\t", 0);

            if ((m_o == ORIENTATION_UP && m_x > 0 && _board[m_y, m_x + 1] == ' ') ||
                (m_o == ORIENTATION_DOWN && m_x < WIDTH && _board[m_y, m_x - 1] == ' ') ||
                (m_o == ORIENTATION_RIGHT && m_y < HEIGHT && _board[m_y + 1, m_x] == ' ') ||
                (m_o == ORIENTATION_LEFT && m_y > 0 && _board[m_y - 1, m_x] == ' '))
            {
                enactedInteraction = GetExistence().AddOrGetPrimitiveInteraction("\\f", 0);
            }

            return enactedInteraction;
        }
        /// <summary>
        /// Touch the square to the left.
        /// </summary>
        /// <returns>Succeeds if there is a wall, fails otherwise</returns>
        private Interaction040 TouchLeft()
        {
            Interaction040 enactedInteraction = this.GetExistence().AddOrGetPrimitiveInteraction("/t", 0);

            if ((m_o == ORIENTATION_UP && m_x > 0 && _board[m_y, m_x - 1] == ' ') ||
                (m_o == ORIENTATION_DOWN && m_x < WIDTH && _board[m_y, m_x + 1] == ' ') ||
                (m_o == ORIENTATION_RIGHT && m_y > 0 && _board[m_y - 1, m_x] == ' ') ||
                (m_o == ORIENTATION_LEFT && m_y < HEIGHT && _board[m_y + 1, m_x] == ' '))
            {
                enactedInteraction = this.GetExistence().AddOrGetPrimitiveInteraction("/f", 0);
            }

            return enactedInteraction;
        }
    }
}

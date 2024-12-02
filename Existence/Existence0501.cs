using Ideal.Agent;
using Ideal.Coupling.Interaction;
using Ideal.Coupling;
using Ideal.Environment;

namespace Ideal.Existence
{
    /// <summary>
    /// Existence050 implements radical interactionism.
    /// </summary>
    public class Existence0501 : Existence050
    {
        // This code is translated from Java to C#.

        private IEnvironment _environment;

        protected IEnvironment GetEnvironment()
        {
            return _environment;
        }

        protected override void InitExistence()
        {
            // You can instantiate another environment here.
            _environment = new Environment050(this);
            //_environment = new EnvironmentMaze(this);
        }

        public override string Step()
        {
            List<IAnticipation> anticipations = Anticipate();
            Experiment040 experience = (Experiment040)SelectExperience(anticipations);

            Interaction040 intendedInteraction = experience.GetIntendedInteraction();
            Console.WriteLine("Intended " + intendedInteraction.ToString());

            Interaction040 enactedInteraction = Enact(intendedInteraction);

            if (enactedInteraction != intendedInteraction)
            {
                Result failResult = CreateOrGetResult(enactedInteraction.GetLabel().Replace('e', 'E').Replace('r', 'R') + ">");
                if (enactedInteraction.GetExperience() == null)
                {
                    enactedInteraction.SetExperience(experience);
                    enactedInteraction.SetResult(failResult);
                }
                else if (enactedInteraction.GetExperience() != experience)
                {
                    int valence = enactedInteraction.GetValence();
                    enactedInteraction = (Interaction040)AddOrGetPrimitiveInteraction(experience, failResult, valence);
                }
            }
            Console.WriteLine("Enacted " + enactedInteraction.ToString());

            if (enactedInteraction.GetValence() >= 0)
                SetMood(Mood.Pleased);
            else
                SetMood(Mood.Pained);

            LearnCompositeInteraction(enactedInteraction);

            SetPreviousSuperInteraction(GetLastSuperInteraction());
            SetEnactedInteraction(enactedInteraction);

            return "" + GetMood();
        }

        public override Interaction040 AddOrGetPrimitiveInteraction(string label, int valence)
        {
            if (!Interactions.ContainsKey(label))
            {
                Interaction040 interaction = CreateInteraction(label);
                interaction.SetValence(valence);
                Interactions[label] = interaction;
            }
            Interaction040 retrievedInteraction = (Interaction040)Interactions[label];
            return retrievedInteraction;
        }

        protected override List<IAnticipation> GetDefaultAnticipations()
        {
            List<IAnticipation> anticipations = new List<IAnticipation>();
            foreach (Experiment experience in Experiences.Values)
            {
                Experiment040 defaultExperience = (Experiment040)experience;
                if (defaultExperience.GetIntendedInteraction().IsPrimitive())
                {
                    Anticipation031 anticipation = new Anticipation031(experience, 0);
                    anticipations.Add(anticipation);
                }
            }
            return anticipations;
        }

        public override Interaction040 Enact(Interaction030 intendedInteraction)
        {
            if (intendedInteraction.IsPrimitive())
            {
                return (Interaction040)GetEnvironment().Enact(intendedInteraction);
            }
            else
            {
                // Enact the pre-interaction
                Interaction040 enactedPreInteraction = Enact(intendedInteraction.GetPreInteraction());
                if (!enactedPreInteraction.Equals(intendedInteraction.GetPreInteraction()))
                {
                    // if the preInteraction failed then the enaction of the intendedInteraction is interrupted here.
                    return enactedPreInteraction;
                }
                else
                {
                    // Enact the post-interaction
                    Interaction040 enactedPostInteraction = Enact(intendedInteraction.GetPostInteraction());
                    return (Interaction040)AddOrGetCompositeInteraction(enactedPreInteraction, enactedPostInteraction);
                }
            }
        }
    }
}

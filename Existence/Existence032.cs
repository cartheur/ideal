using Ideal.Agent;
using Ideal.Coupling.Interaction;
using Ideal.Coupling;

namespace Ideal.Existence
{
    public class Existence032 : Existence031
    {
        public override string Step()
        {
            List<IAnticipation> anticipations = Anticipate();
            Interaction032 intendedInteraction = (Interaction032)SelectInteraction(anticipations);
            Experiment experience = intendedInteraction.GetExperience();

            // Result result = ReturnResult010(experience);
            // Result result = ReturnResult030(experience);
            Result result = ReturnResult031(experience);

            Interaction030 enactedInteraction = GetInteraction(experience.GetLabel() + result.GetLabel());
            Console.WriteLine("Enacted " + enactedInteraction.ToString());

            if (enactedInteraction != intendedInteraction)
            {
                intendedInteraction.AddAlternateInteraction(enactedInteraction);
                Console.WriteLine("Alternate " + enactedInteraction.GetLabel());
            }

            if (enactedInteraction.GetValence() >= 0)
                this.SetMood(Mood.Pleased);
            else
                this.SetMood(Mood.Pained);

            this.LearnCompositeInteraction(enactedInteraction);
            this.SetEnactedInteraction(enactedInteraction);

            return "" + this.GetMood();
        }

        public override Interaction032 SelectInteraction(List<IAnticipation> anticipations)
        {
            anticipations.Sort();
            foreach (IAnticipation anticipation in anticipations)
                Console.WriteLine("anticipate " + anticipation.ToString());

            Anticipation030 selectedAnticipation = (Anticipation030)anticipations[0];
            Interaction032 intendedInteraction = (Interaction032)selectedAnticipation.GetInteraction();

            return intendedInteraction;
        }
        /// <summary>
        /// Computes the list of anticipations. 
        /// </summary>
        /// <returns>A list of anticipations</returns>
        public override List<IAnticipation> Anticipate()
        {

            List<IAnticipation> anticipations = GetDefaultAnticipations();
            List<Interaction> activatedInteractions = this.GetActivatedInteractions();

            foreach (Interaction activatedInteraction in activatedInteractions)
            {
                Interaction032 proposedInteraction = (Interaction032)((Interaction032)activatedInteraction).GetPostInteraction();
                int proclivity = ((Interaction032)activatedInteraction).GetWeight() * proposedInteraction.GetValence();
                Anticipation032 anticipation = new Anticipation032(proposedInteraction, proclivity);
                int index = anticipations.IndexOf(anticipation);
                if (index < 0)
                    anticipations.Add(anticipation);
                else
                    ((Anticipation032)anticipations[index]).AddProclivity(proclivity);
            }

            foreach (IAnticipation anticipation in anticipations)
            {
                foreach (Interaction interaction in ((Interaction032)((Anticipation032)anticipation).GetInteraction()).GetAlternateInteractions())
                {
                    foreach (Interaction activatedInteraction in activatedInteractions)
                    {
                        if (interaction == ((Interaction031)activatedInteraction).GetPostInteraction())
                        {
                            int proclivity = ((Interaction031)activatedInteraction).GetWeight() * ((Interaction031)interaction).GetValence();
                            ((Anticipation032)anticipation).AddProclivity(proclivity);
                        }
                    }
                }
            }

            return anticipations;
        }

        /// <summary>
        /// Gets the default anticipations. All primitive interactions are proposed by default with a proclivity of 0, return the list of anticipations.
        /// </summary>
        /// <returns>A list of anticipations</returns>
        protected override List<IAnticipation> GetDefaultAnticipations()
        {
            List<IAnticipation> anticipations = new List<IAnticipation>();
            foreach (Interaction i in anticipations)
            {
                Interaction032 interaction = (Interaction032)i;
                if (interaction.IsPrimitive())
                {
                    Anticipation032 anticipation = new Anticipation032(interaction, 0);
                    anticipations.Add(anticipation);
                }
            }
            return anticipations;
        }

        protected override Interaction032 CreateInteraction(string label)
        {
            return new Interaction032(label);
        }
    }
}

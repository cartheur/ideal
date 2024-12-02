using Ideal.Agent;
using Ideal.Coupling;
using Ideal.Coupling.Interaction;

namespace Ideal.Existence
{
    public class Existence050 : Existence040
    {
        private Environment environment;
        protected Environment GetEnvironment()
        {
            return this.environment;
        }

        protected override void InitExistence()
        {
            // You can instantiate another environment here.
            this.environment = new Environment050(this);
            //this.environment = new EnvironmentMaze(this);
        }

        public override string Step()
        {
            List<Anticipation> anticipations = Anticipate();
            Experiment050 experience = (Experiment050)SelectExperience(anticipations);

            Interaction040 intendedInteraction = experience.GetIntendedInteraction();
            Console.WriteLine("Intended " + intendedInteraction.ToString());

            Interaction040 enactedInteraction = Enact(intendedInteraction);

            if (enactedInteraction != intendedInteraction)
                experience.AddEnactedInteraction(enactedInteraction);

            Console.WriteLine("Enacted " + enactedInteraction.ToString());

            if (enactedInteraction.GetValence() >= 0)
                this.SetMood(Mood.Pleased);
            else
                this.SetMood(Mood.Pained);

            this.LearnCompositeInteraction(enactedInteraction);

            this.SetPreviousSuperInteraction(this.GetLastSuperInteraction());
            this.SetEnactedInteraction(enactedInteraction);

            return "" + this.GetMood();
        }
        /// <summary>
        /// Computes the list of anticipations.
        /// </summary>
        /// <returns>The list of aniticipations.</returns>
        public List<Anticipation> Anticipate()
        {
            List<Anticipation> anticipations = GetDefaultAnticipations();
            List<Interaction> activatedInteractions = this.GetActivatedInteractions();

            if (this.GetEnactedInteraction() != null)
            {
                foreach (Interaction activatedInteraction in activatedInteractions)
                {
                    if (((Interaction031)activatedInteraction).GetPostInteraction().GetExperience() != null)
                    {
                        Anticipation031 anticipation = new Anticipation031(
                            ((Interaction031)activatedInteraction).GetPostInteraction().GetExperience(),
                            ((Interaction031)activatedInteraction).GetWeight() * ((Interaction031)activatedInteraction).GetPostInteraction().GetValence()
                        );
                        int index = anticipations.IndexOf(anticipation);
                        if (index < 0)
                            anticipations.Add(anticipation);
                        else
                            ((Anticipation031)anticipations[index]).AddProclivity(((Interaction031)activatedInteraction).GetWeight() * ((Interaction031)activatedInteraction).GetPostInteraction().GetValence());
                    }
                }
            }

            foreach (Anticipation anticipation in anticipations)
            {
                foreach (Interaction interaction in ((Experiment050)((Anticipation031)anticipation).GetExperience()).GetEnactedInteractions())
                {
                    foreach (Interaction activatedInteraction in activatedInteractions)
                    {
                        if (interaction == ((Interaction032)activatedInteraction).GetPostInteraction())
                        {
                            int proclivity = ((Interaction032)activatedInteraction).GetWeight() * ((Interaction032)interaction).GetValence();
                            ((Anticipation031)anticipation).AddProclivity(proclivity);
                        }
                    }
                }
            }

            return anticipations;
        }

        public Experiment050 AddOrGetAbstractExperience(Interaction040 interaction)
        {
            string label = interaction.GetLabel().Replace('e', 'E').Replace('r', 'R').Replace('>', '|');
            if (!Experiences.ContainsKey(label))
            {
                Experiment050 abstractExperience = new Experiment050(label);
                abstractExperience.SetIntendedInteraction(interaction);
                interaction.SetExperience(abstractExperience);
                EXPERIENCES[label] = abstractExperience;
            }
            return (Experiment050)EXPERIENCES[label];
        }


        
    }
}


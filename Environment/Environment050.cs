using Ideal.Coupling.Interaction;
using Ideal.Existence;

namespace Ideal.Environment
{
    public class Environment050 : IEnvironment
    {
        private Existence050 existence;

        public Environment050(Existence050 existence)
        {
            this.existence = existence;
            init();
        }

        protected void init()
        {
            this.GetExistence().AddOrGetPrimitiveInteraction(this.GetExistence().LABEL_E1 + this.GetExistence().LABEL_R1, -1);
            Interaction040 i12 = this.GetExistence().AddOrGetPrimitiveInteraction(this.GetExistence().LABEL_E1 + this.GetExistence().LABEL_R2, 1);
            this.GetExistence().AddOrGetPrimitiveInteraction(this.GetExistence().LABEL_E2 + this.GetExistence().LABEL_R1, -1);
            Interaction040 i22 = this.GetExistence().AddOrGetPrimitiveInteraction(this.GetExistence().LABEL_E2 + this.GetExistence().LABEL_R2, 1);
            this.GetExistence().AddOrGetAbstractExperience(i12);
            this.GetExistence().AddOrGetAbstractExperience(i22);
        }

        protected Existence050 GetExistence()
        {
            return this.existence;
        }

        private Interaction previousInteraction;

        protected void SetPreviousInteraction(Interaction previousInteraction)
        {
            this.previousInteraction = previousInteraction;
        }

        protected Interaction GetPreviousInteraction()
        {
            return this.previousInteraction;
        }

        private Interaction penultimateInteraction;

        protected void SetPenultimateInteraction(Interaction penultimateInteraction)
        {
            this.penultimateInteraction = penultimateInteraction;
        }

        protected Interaction GetPenultimateInteraction()
        {
            return this.penultimateInteraction;
        }

        public Interaction Enact(Interaction intendedInteraction)
        {
            Interaction enactedInteraction = null;

            if (intendedInteraction.GetLabel().Contains(this.GetExistence().LABEL_E1))
            {
                if (this.GetPreviousInteraction() != null &&
                    (this.GetPenultimateInteraction() == null || this.GetPenultimateInteraction().GetLabel().Contains(this.GetExistence().LABEL_E2)) &&
                    this.GetPreviousInteraction().GetLabel().Contains(this.GetExistence().LABEL_E1))
                {
                    enactedInteraction = this.GetExistence().AddOrGetPrimitiveInteraction(this.GetExistence().LABEL_E1 + this.GetExistence().LABEL_R2, 0);
                }
                else
                {
                    enactedInteraction = this.GetExistence().AddOrGetPrimitiveInteraction(this.GetExistence().LABEL_E1 + this.GetExistence().LABEL_R1, 0);
                }
            }
            else
            {
                if (this.GetPreviousInteraction() != null &&
                    (this.GetPenultimateInteraction() == null || this.GetPenultimateInteraction().GetLabel().Contains(this.GetExistence().LABEL_E1)) &&
                    this.GetPreviousInteraction().GetLabel().Contains(this.GetExistence().LABEL_E2))
                {
                    enactedInteraction = this.GetExistence().AddOrGetPrimitiveInteraction(this.GetExistence().LABEL_E2 + this.GetExistence().LABEL_R2, 0);
                }
                else
                {
                    enactedInteraction = this.GetExistence().AddOrGetPrimitiveInteraction(this.GetExistence().LABEL_E2 + this.GetExistence().LABEL_R1, 0);
                }
            }

            this.SetPenultimateInteraction(this.GetPreviousInteraction());
            this.SetPreviousInteraction(enactedInteraction);

            return enactedInteraction;
        }
    }
}

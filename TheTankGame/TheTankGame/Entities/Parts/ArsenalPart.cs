namespace TheTankGame.Entities.Parts
{
    using Contracts;

    public class ArsenalPart : BasePart, IAttackModifyingPart
    {
        public ArsenalPart(string model, double weight, decimal price, int attackModifier)
            : base(model, weight, price)
        {
            this.AttackModifier = attackModifier;
        }

        public int AttackModifier { get; }

        public override string ToString()
        {
            return base.ToString() + $"+{this.AttackModifier} Attack";
        }
    }
}
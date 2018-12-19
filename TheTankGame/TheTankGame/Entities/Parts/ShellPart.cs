namespace TheTankGame.Entities.Parts
{
    using Contracts;

    public class ShellPart : BasePart, IDefenseModifyingPart
    {
        public ShellPart(string model, double weight, decimal price, int defenceModifier) 
            : base(model, weight, price)
        {
            this.DefenseModifier = defenceModifier;
        }

        public int DefenseModifier { get; }

        public override string ToString()
        {
            return base.ToString() + $"+{this.DefenseModifier} Defence";
        }

    }
}
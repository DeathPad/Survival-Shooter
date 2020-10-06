namespace Shooter
{
    public class StatsModifier
    {
        public string Identifier { get; private set; }
        public float Value { get; private set; }

        public StatsModifier(string identifier, float value)
        {
            Identifier = identifier;
            Value = value;
        }
    }
}
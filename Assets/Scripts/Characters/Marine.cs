namespace Sweet_And_Salty_Studios
{
    public class Marine : Unit
    {
        public override void Initialize(
             PlayerEngine owner,
             int maxHealth,
             int armorClass,
             float speed)
        {
            base.Initialize(owner, maxHealth, armorClass, speed);
        }
    }
}

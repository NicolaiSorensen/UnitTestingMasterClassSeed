namespace TestUtilities
{
    using AutoFixture.AutoMoq;

    public class AutoMoqWithMembersCustomization : AutoMoqCustomization
    {
        public AutoMoqWithMembersCustomization()
        {
            ConfigureMembers = true;
        }
    }
}
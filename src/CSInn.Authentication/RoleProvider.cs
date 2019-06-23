namespace CSInn.Application.Discord.Authentication
{
    public class RoleProvider : IRoleProvider
    {
        public string GetRole(string identifier)
        {
            //TODO:
            //Proper implementation once db tables are set up. For now, everyone are "members".
            return "Member";
        }
    }
}
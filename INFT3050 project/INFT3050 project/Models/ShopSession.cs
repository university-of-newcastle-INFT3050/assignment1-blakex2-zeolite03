namespace INFT3050_project.Models
{
    //handles session data
    public class ShopSession
    {
        private ISession session { get; set; }
        public ShopSession(ISession session) => this.session = session;
        private const string ConfKey = "conf";
        private const string DivKey = "div";
        public int UserId { get; set; }

        public void SetActiveConf(string activeConf) =>
           session.SetString(ConfKey, activeConf);
        public string GetActiveConf() =>
            session.GetString(ConfKey) ?? string.Empty;

        public void SetActiveDiv(string activeDiv) =>
            session.SetString(DivKey, activeDiv);
        public string GetActiveDiv() =>
            session.GetString(DivKey) ?? string.Empty;
    }
}

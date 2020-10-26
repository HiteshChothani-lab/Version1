namespace UserManagement.Entity
{
    public class UserSignatureRequestEntity : EntityBase
    {
        public string Id { get; set; }
        public string SurveyId { get; set; }
        public string MasterStoreId { get; set; }
    }
}

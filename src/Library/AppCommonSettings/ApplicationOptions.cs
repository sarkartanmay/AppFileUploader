namespace AppCommonSettings
{
    public class ApplicationOptions
    {
        public string AllowedHosts { get; set; } = string.Empty;
        public string ValidOrigin { get; set; } = string.Empty;
        public InfraStructure InfraStructure { get; set; } = new InfraStructure();
        public MySqlDb MySqlDb { get; set; } = new MySqlDb();
        public JwtConfig JwtConfig { get; set; } = new JwtConfig();
    }
    public class InfraStructure
    {
        public string Mode { get; set; } = string.Empty;
        public OnPrem OnPrem { get; set; } = new OnPrem();
        public AzureBlob AzureBlob { get; set; } = new AzureBlob();
    }
    public class OnPrem
    {
        public string UploadPath { get; set; } = string.Empty;
    }
    public class AzureBlob
    {
        public string BlobStorageConn { get; set; } = string.Empty;
        public string BlobStorageContainer { get; set; } = string.Empty;
    }
    public class MySqlDb
    {
        public string Server { get; set; } = string.Empty;
        public string User { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Database { get; set; } = string.Empty;
        public string SSLMode { get; set; } = string.Empty;
    }
    public class JwtConfig
    {
        public string Key { get; set; } = string.Empty;
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
    }
}

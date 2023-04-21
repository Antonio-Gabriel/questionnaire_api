namespace Tests
{
    public static class Globals
    {
        public static string MyConnection { get; private set; } = 
            "Server=localhost,1433;Database=master;User=sa;Password=Questionnaire@ssW0rd!;TrustServerCertificate=True;Encrypt=false;";
    }
}
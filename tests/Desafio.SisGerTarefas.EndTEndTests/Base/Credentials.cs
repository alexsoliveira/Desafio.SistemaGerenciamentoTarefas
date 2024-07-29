namespace Desafio.SisGerTarefas.EndTEndTests.Base
{
    internal class UsuarioRespostaLogin
    {
        public string AccessToken { get; set; }
        public double ExpiresIn { get; set; }
        public UsuarioToken UsuarioToken { get; set; }
    }

    internal class UsuarioToken
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public IEnumerable<UsuarioClaim> Claims { get; set; }
    }

    internal class UsuarioClaim
    {
        public string Value { get; set; }
        public string Type { get; set; }
    }
}

namespace Contracts;

public class RetornoApi
{
    public bool? sucesso { get; set; }
    public string[] mensagens { get; set; }

    public RetornoApi(): this(true) { }

    public RetornoApi(bool sucesso = true)
    {
        this.sucesso = sucesso;
    }
    public RetornoApi(string mensagem, bool sucesso = true)
    {
        this.sucesso = sucesso;
        this.mensagens = new string[] { mensagem };
    }
    public RetornoApi(string[] mensagens, bool sucesso = true)
    {
        this.sucesso = sucesso;
        this.mensagens = mensagens;
    }

    public RetornoApi(ICollection<string> mensagens, bool sucesso = true)
    {
        this.sucesso = sucesso;
        this.mensagens = mensagens.ToArray();
    }
}

public class RetornoApi<T> : RetornoApi
{
    public RetornoApi()
    {

    }
    public RetornoApi(string mensagem, bool sucesso = true) : base(mensagem, sucesso) { }

    public RetornoApi(T[] dados) { this.dados = dados; this.sucesso = true; }
    public RetornoApi(ICollection<T> dados) : this(dados.ToArray()) { }
    public RetornoApi(T dado) : this(new T[] { dado }) { }

    public T[] dados { get; set; }
}
using System;

class MaterialBiblioteca
{
    private int codigo;
    private string titulo;
    private int anoPublicacao;
    private bool disponivel;
    private DateTime? dataDevolucao;

    public MaterialBiblioteca(int codigo, string titulo, int anoPublicacao)
    {
        this.codigo = codigo;
        this.titulo = titulo;
        this.anoPublicacao = anoPublicacao;
        disponivel = true;
    }

    public virtual int PrazoEmprestimo()
    {
        return 7;
    }

    public void Consultar()
    {
        Console.WriteLine("\nCódigo: " + codigo);
        Console.WriteLine("Título: " + titulo);
        Console.WriteLine("Ano: " + anoPublicacao);
        Console.WriteLine("Disponível: " + (disponivel ? "Sim" : "Não"));
    }

    public void Emprestar()
    {
        if (!disponivel)
        {
            Console.WriteLine("Material já está emprestado!");
            return;
        }

        disponivel = false;
        dataDevolucao = DateTime.Today.AddDays(PrazoEmprestimo());

        Console.WriteLine("Empréstimo realizado!");
        Console.WriteLine("Data de devolução: " +
            dataDevolucao.Value.ToString("dd/MM/yyyy"));
    }

    public void Devolver()
    {
        if (disponivel)
        {
            Console.WriteLine("Este material não está emprestado!");
            return;
        }

        DateTime hoje = DateTime.Today;
        int atraso = Math.Max(0,
            (hoje - dataDevolucao.Value).Days);

        if (atraso > 0)
            Console.WriteLine("Devolução atrasada em " + atraso + " dia(s).");
        else
            Console.WriteLine("Devolução dentro do prazo.");

        disponivel = true;
        dataDevolucao = null;

        Console.WriteLine("Material devolvido!");
    }
}

class Livro : MaterialBiblioteca
{
    private string autor;
    private string genero;

    public Livro(int codigo, string titulo, int ano,
        string autor, string genero)
        : base(codigo, titulo, ano)
    {
        this.autor = autor;
        this.genero = genero;
    }

    public override int PrazoEmprestimo()
    {
        return 14;
    }

    public void MostrarDetalhes()
    {
        Consultar();
        Console.WriteLine("Autor: " + autor);
        Console.WriteLine("Gênero: " + genero);
    }
}

class Revista : MaterialBiblioteca
{
    private int edicao;

    public Revista(int codigo, string titulo, int ano, int edicao)
        : base(codigo, titulo, ano)
    {
        this.edicao = edicao;
    }

    public override int PrazoEmprestimo()
    {
        return 7;
    }

    public void MostrarDetalhes()
    {
        Consultar();
        Console.WriteLine("Edição: " + edicao);
    }
}

class DVD : MaterialBiblioteca
{
    private string classificacao;

    public DVD(int codigo, string titulo, int ano, string classificacao)
        : base(codigo, titulo, ano)
    {
        this.classificacao = classificacao;
    }

    public override int PrazoEmprestimo()
    {
        return 3;
    }

    public void MostrarDetalhes()
    {
        Consultar();
        Console.WriteLine("Classificação: " + classificacao);
    }
}

class Program
{
    static void Main()
    {
        Livro livro = new Livro(
            1, "Dom Casmurro", 1899,
            "Machado de Assis", "Romance");

        Revista revista = new Revista(
            2, "Revista de Tecnologia", 2025, 10);

        DVD dvd = new DVD(
            3, "Filme de Aventura", 2024, "Livre");

        Console.WriteLine("=== BIBLIOTECA MUNICIPAL ===");

        Console.WriteLine("\n=== LIVRO ===");
        livro.MostrarDetalhes();
        livro.Emprestar();
        livro.Emprestar();
        livro.Devolver();

        Console.WriteLine("\n=== REVISTA ===");
        revista.MostrarDetalhes();
        revista.Emprestar();
        revista.Devolver();

        Console.WriteLine("\n=== DVD ===");
        dvd.MostrarDetalhes();
        dvd.Emprestar();
        dvd.Devolver();
    }
}
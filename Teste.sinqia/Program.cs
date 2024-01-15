bool Continuar = true;

while (Continuar)
{
    Console.Clear();


    Console.WriteLine("Escolha uma opção:");
    Console.WriteLine("1. Verificar Triângulo");
    Console.WriteLine("2. Calcular Soma dos Valores nas Posições Pares do Vetor");
    Console.WriteLine("3. Encerrar");


    int opcao;

    while (!int.TryParse(Console.ReadLine(), out opcao) || (opcao != 1 && opcao != 2 && opcao != 3))
    {
        Console.WriteLine("Opção inválida. Por favor, escolha 1, 2 ou 3.");
    }

    switch (opcao)
    {
        case 1:
            await FuncaoTriangulo();
            break;
        case 2:
            await FuncaoSomaValoresArray();
            break;
        case 3:
            Continuar = false;
            break;
    }

    if (Continuar)
    {
        Console.WriteLine("\nPressione qualquer tecla para voltar ao menu...");
        Console.ReadKey();
    }

}


//Soma Valores Metotos
static async Task FuncaoSomaValoresArray()
{
    int Novamente = 0;
    while (Novamente == 0)
    {
        Console.Clear();
        Console.WriteLine("Você escolheu a função do Calculo de Pares em um vetor de até 100 posições.");
        Console.WriteLine("");
        Console.WriteLine("Você Deseja que o sistema Gere um Vetor de 100 elementos Aleatórios, Ou Voce deseja Adicinar?.");
        Console.WriteLine("");
        Console.WriteLine("Digite 1 para o sistema Gerar Aleatoriamente ou 2 para voce colocar o texto.");
        var Escolha = Console.ReadLine();

        List<Task> ValidaEntrada = new()
            {
                Task.Run(() => VerificaEntradaStringVaziaAsync(Escolha)),
                Task.Run(() => ValidaEntradaNumericaAsync(Escolha)),
            };
        await Task.WhenAll(ValidaEntrada);

        if (!((Task<bool>)ValidaEntrada[0]).Result)
        {
            Console.WriteLine("Você não digitou nada");
            Console.WriteLine("Deseja tentar novamente? (Digite 's' para Sim, qualquer outro texto para Não)");
            string resposta = Console.ReadLine();

            if (resposta.ToLower() == "s")
            {
                continue;
            }
            else
            {
                Novamente = 1;
                continue;
            }

        }


        if (!((Task<bool>)ValidaEntrada[1]).Result)
        {
            Console.WriteLine("Você digitou algo diferente de numero, ou digitou zero.");
            Console.WriteLine("Deseja tentar novamente? (Digite 's' para Sim, qualquer outro texto para Não)");
            string resposta = Console.ReadLine();

            if (resposta.ToLower() == "s")
            {
                continue;
            }
            else
            {
                Novamente = 1;
                continue;
            }

        }

        var entrada = Convert.ToInt32(Escolha);
        if (entrada == 1 || entrada == 2)
        {
            await SomaValoresVetorPar(entrada);
            return;
        }
        else
        {
            Console.WriteLine("Por favor, Digite 1 ou 2");
            Console.WriteLine("Deseja tentar novamente? (Digite 's' para Sim, qualquer outro texto para Não)");
            string resposta = Console.ReadLine();

            if (resposta.ToLower() == "s")
            {
                continue;
            }
            else
            {
                Novamente = 1;
                continue;
            }

        }
    }
}
static async Task SomaValoresVetorPar(int escolha)
{

    if (escolha == 1)
    {
        int[] vetor = await GerarVetorAleatorio(100);
        int somaPares = await CalcularSomaPares(vetor);

        Console.WriteLine("Valores do Vetor:");
        for (int i = 0; i < vetor.Length; i++)
        {
            Console.Write(vetor[i] + " ");
        }

        Console.WriteLine();
        Console.WriteLine($"A soma dos valores nas posições pares do vetor é: {somaPares}");
    }
    else
    {
        bool Loop = true;
        while (Loop)
        {
            Console.Clear();
            Console.WriteLine("Por favor, adicione numeros separados por ', '");
            Console.WriteLine("Exemplo: 1,23,15,16,3,7,4,2,6,3");
            Console.WriteLine();
            Console.WriteLine("Assim o sistema ira ler e gerar o array e dar soma, Certifique-se de digitar somente numeros e logo em seguida virgula");
            string array = Console.ReadLine();

            if (await VerificaEntradaStringVaziaAsync(array))
            {
                if (ValidaStringNumerica(array, out int[] vetorNumerico))
                {
                    Console.WriteLine("Valores do Vetor:");
                    for (int i = 0; i < vetorNumerico.Length; i++)
                    {
                        Console.Write(vetorNumerico[i] + " ");
                    }

                    Console.WriteLine();
                    int somaPares = await CalcularSomaPares(vetorNumerico);
                    Console.WriteLine($"A soma dos valores nas posições pares do vetor é: {somaPares}");
                    Loop = false;
                    continue;
                }
                else
                {
                    Console.WriteLine("Voce não digitou apenas Numeros e depois vírgula, Contem letras A-Z");
                    Console.WriteLine("Deseja tentar novamente? (Digite 's' para Sim, qualquer outro texto para Não)");
                    string resposta = Console.ReadLine();

                    if (resposta.ToLower() == "s")
                    {
                        continue;
                    }
                    else
                    {
                        Loop = false;
                        continue;
                    }
                }

            }
            else
            {
                Console.WriteLine("Você não digitou nada");
                Console.WriteLine("Deseja tentar novamente? (Digite 's' para Sim, qualquer outro texto para Não)");
                string resposta = Console.ReadLine();

                if (resposta.ToLower() == "s")
                {
                    continue;
                }
                else
                {
                    Loop = false;
                    continue;
                }

            }
        }

    }
}


//Triangulo Metodos
static async Task FuncaoTriangulo()
{
    bool Loop = true;
    while (Loop)
    {
        Console.Clear();
        Console.WriteLine("Você escolheu a função do Triangulo!!!");
        Console.WriteLine("Informe os comprimentos dos lados do triângulo:");
        string LadoX, LadoY, LadoZ;

        Console.WriteLine("Por favor, insira um valor numérico válido e maior que zero para o lado X:");
        LadoX = Console.ReadLine();

        Console.WriteLine("Por favor, insira um valor numérico válido e maior que zero para o lado Y:");
        LadoY = Console.ReadLine();

        Console.WriteLine("Por favor, insira um valor numérico válido e maior que zero para o lado Z:");
        LadoZ = Console.ReadLine();

        if (await ValidaEntradasTrianguloAsync(LadoX, LadoY, LadoZ))
        {
            await VerificaTrianguloAsync(Convert.ToDouble(LadoX), Convert.ToDouble(LadoY), Convert.ToDouble(LadoZ));
            Loop = false;
            continue;
        }
        else
        {
            Console.WriteLine();
            Console.WriteLine("Deseja tentar novamente? (Digite 's' para Sim, qualquer outro texto para Não)");
            string resposta = Console.ReadLine();

            if (resposta.ToLower() == "s")
            {
                continue;
            }
            else
            {
                Loop = false;
                continue;
            }
        }


    }
}
static async Task VerificaTrianguloAsync(double X, double Y, double Z)
{
    //Chama função para validar se forma os valores informados iram formar um triangulo.
    var Triangulo = await FormaTrianguloAsync(X, Y, Z);
    if (Triangulo)
    {

        Console.WriteLine("Os valores formam um triângulo.");

        if (X == Y && Y == Z)
            Console.WriteLine("É um triângulo equilátero.");
        else if (X == Y || Y == Z || X == Z)
            Console.WriteLine("É um triângulo isósceles.");
        else
            Console.WriteLine("É um triângulo escaleno.");

        return;
    }
    Console.WriteLine("Os valores não formam um triângulo.");
    return;


    static async Task<bool> FormaTrianguloAsync(double x, double y, double z)
    {
        var TrianguloValido = (x + y > z) && (x + z > y) && (y + z > x);

        return TrianguloValido;
    }
}
static async Task<bool> ValidaEntradasTrianguloAsync(string Lado1, string Lado2, string Lado3)
{
    //Chama Validados de String Vazia.
    List<Task> ValidaString = new List<Task>()
            {
                Task.Run(() => VerificaEntradaStringVaziaAsync(Lado1)),
                Task.Run(() => VerificaEntradaStringVaziaAsync(Lado2)),
                Task.Run(() => VerificaEntradaStringVaziaAsync(Lado3))
            };
    await Task.WhenAll(ValidaString);
    // Fim chamda funcao de validaString vazia.


    //Valida retorno caso alguma entrada de dados esteja vazia.
    if (!((Task<bool>)ValidaString[0]).Result || !((Task<bool>)ValidaString[1]).Result || !((Task<bool>)ValidaString[2]).Result)
    {
        Console.WriteLine("Em um dos lados do triangulo, Não foi digitado nada, Por favor Certifique-se que digitar algum número");
        return false;
    }
    //Fim retorno caso string vazia.


    //Chama função de tentar converter para numero e se o resultado e diferente de zero.
    List<Task> ValidaNumero = new List<Task>
            {
                Task.Run(() => ValidaEntradaNumericaAsync(Lado1)),
                Task.Run(() => ValidaEntradaNumericaAsync(Lado2)),
                Task.Run(() => ValidaEntradaNumericaAsync(Lado3))
            };
    await Task.WhenAll(ValidaNumero);
    //Fim converter para numero e ver se diferente de zero.


    //Valida retorno caso alguma entrada de dados nao seja numero ou seja um Zero.
    if (!((Task<bool>)ValidaNumero[0]).Result || !((Task<bool>)ValidaNumero[1]).Result || !((Task<bool>)ValidaNumero[2]).Result)
    {
        Console.WriteLine("Em um dos lados do triangulo, Não foi digitado um numero, Por favor Certifique-se que digitar algum número e que este numero seja maior que Zero");
        return false;
    }
    //Fim retorno caso entrada de dados nao seja numero ou seja um Zero.

    return true;
}



// Metodos auxiliares
static bool ValidaStringNumerica(string Numeros, out int[] vetorNumerico)
{
    string[] numerosSeparados = Numeros.Split(',');

    vetorNumerico = new int[numerosSeparados.Length];

    for (int i = 0; i < numerosSeparados.Length; i++)
    {
        if (!int.TryParse(numerosSeparados[i], out vetorNumerico[i]))
        {
            // Se a conversão falhar, retorna falso indicando que a string não contém apenas números
            return false;
        }
    }

    return true;
}
static async Task<int[]> GerarVetorAleatorio(int tamanho)
{
    Random random = new Random();
    int[] vetor = new int[tamanho];

    for (int i = 0; i < tamanho; i++)
    {
        vetor[i] = random.Next(1000); // Gera números aleatórios de 0 a 999
    }

    return vetor;
}
static async Task<int> CalcularSomaPares(int[] vetor)
{
    int soma = 0;

    for (int i = 0; i < vetor.Length; i += 2)
    {
        soma += vetor[i];
    }

    return soma;
}
static async Task<bool> VerificaEntradaStringVaziaAsync(string Texto)
{
    if (!string.IsNullOrWhiteSpace(Texto))
    {
        return true;
    }
    return false;
}
static async Task<bool> ValidaEntradaNumericaAsync(string Texto)
{
    return double.TryParse(Texto, out var val) && val > 0;
}

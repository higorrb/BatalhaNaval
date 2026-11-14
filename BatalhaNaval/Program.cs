int tamanholinha = 8, tamanhocoluna = 8, quantNavios = 5;

string[,] matrizJogador = new string[tamanholinha, tamanhocoluna];
string[,] matrizBot = new string[tamanholinha, tamanhocoluna];
string[,] matrizEspectroJogador = matrizJogador;
string[,] matrizEspectroBot = matrizBot;

string[] direcoes = { "^", ">", "v", "<" };

int linha = 0, coluna = 0;
string direcao;

void PopularMatriz(string[,] matriz)
{
    for (int i = 0; i < tamanholinha; i++)
    {
        for (int j = 0; j < tamanhocoluna; j++)
        {
            if (i != 0 && j != 0) matriz[i, j] = "~";
            else if (i == 0) matriz[i, j] = j.ToString();
            else if (j == 0) matriz[i, j] = i.ToString();
        }

    }

}

void MostrarMatriz(string[,] matriz, int linha, int coluna)
{
    for (int i = 0; i < linha; i++)
    {
        for (int j = 0; j < coluna; j++)
        {
            Console.Write($"[{matriz[i, j]}] \t");
        }
        Console.WriteLine(" ");
    }

}

bool ValidarCoordenadas(int linha, int coluna)
{
    if (linha < 1 || linha >= tamanholinha || coluna < 1 || coluna >= tamanhocoluna)
    {
        return false;
    }

    return true;
}

int ValidarDireçãoCima(string[,] matriz, int tamanhoNavio, int linha, int coluna)
{
    int caminhoBloq = 0;

    for (int j = 0; j < tamanhoNavio; j++)
    {
        if (linha - j < 1 || matriz[linha - j, coluna] != "~")
        {
            caminhoBloq++;
            break;
        }
    }
    return caminhoBloq;
}

int ValidarDireçãoDireita(string[,] matriz, int tamanhoNavio, int linha, int coluna)
{
    int caminhoBloq = 0;

    for (int j = 0; j < tamanhoNavio; j++)
    {
        if (coluna + j >= tamanhocoluna || matriz[linha, coluna + j] != "~")
        {
            caminhoBloq++;
            break;
        }
    }
    return caminhoBloq;
}

int ValidarDireçãoBaixo(string[,] matriz, int tamanhoNavio, int linha, int coluna)
{
    int caminhoBloq = 0;

    for (int j = 0; j < tamanhoNavio; j++)
    {
        if (linha + j >= tamanholinha || matriz[linha + j, coluna] != "~")
        {
            caminhoBloq++;
            break;
        }
    }
    return caminhoBloq;
}

int ValidarDireçãoEsquerda(string[,] matriz, int tamanhoNavio, int linha, int coluna)
{
    int caminhoBloq = 0;

    for (int j = 0; j < tamanhoNavio; j++)
    {
        if (coluna - j < 1 || matriz[linha, coluna - j] != "~")
        {
            caminhoBloq++;
            break;
        }
    }
    return caminhoBloq;
}

bool AdicionarNavios(string[,] matriz, int linha, int coluna, string direcao, int tamanhoNavio)
{
    switch (direcao)
    {
        case "^":
            if (ValidarDireçãoCima(matriz, tamanhoNavio, linha, coluna) == 0)
            {
                for (int j = 0; j < tamanhoNavio; j++)
                {
                    matriz[linha - j, coluna] = "N";
                }
                return true;
            }
            else
            {
                return false;
            }

        case ">":
            if (ValidarDireçãoDireita(matriz, tamanhoNavio, linha, coluna) == 0)
            {
                for (int j = 0; j < tamanhoNavio; j++)
                {
                    matriz[linha, coluna + j] = "N";
                }
                return true;
            }
            else
            {
                return false;
            }

        case "v":
            if (ValidarDireçãoBaixo(matriz, tamanhoNavio, linha, coluna) == 0)
            {
                for (int j = 0; j < tamanhoNavio; j++)
                {
                    matriz[linha + j, coluna] = "N";
                }
                return true;
            }
            else
            {
                return false;
            }

        case "<":
            if (ValidarDireçãoEsquerda(matriz, tamanhoNavio, linha, coluna) == 0)
            {
                for (int j = 0; j < tamanhoNavio; j++)
                {
                    matriz[linha, coluna - j] = "N";
                }
                return true;
            }
            else
            {
                return false;
            }

        default:
            Console.WriteLine("Direção invalida!, tente outra...");
            return false;
    }

}


//EXECUÇÃO
PopularMatriz(matrizJogador);
PopularMatriz(matrizBot);
PopularMatriz(matrizEspectroJogador);
PopularMatriz(matrizEspectroBot);

//JOGADOR ADICIONANDO NAVIOS
for (int i = quantNavios; i > 1; i--)
{
    MostrarMatriz(matrizJogador, tamanholinha, tamanhocoluna);

    Console.Write($"Digite a LINHA inicial para o navio de [{i}] lugares: ");
    linha = int.Parse(Console.ReadLine());

    Console.Write($"Digite a COLUNA inicial para o navio de [{i}] lugares: ");
    coluna = int.Parse(Console.ReadLine());

    if (ValidarCoordenadas(linha, coluna) == false)
    {
        Console.WriteLine("COORDENADAS INVÁLIDAS!, tente outras...\n");
        i++;
    }
    else
    {
        Console.Write($"" +
        $"[^] para cima\n" +
        $"[>] para direita\n" +
        $"[v] para baixo\n" +
        $"[<] para esquerda\n" +
        $"\nDigite a DIREÇÃO para o navio de [{i}] lugares: ");
        direcao = Console.ReadLine();

        if (AdicionarNavios(matrizJogador, linha, coluna, direcao, i) == false)
        {
            Console.WriteLine("DIREÇÃO INVÁLIDA!, tente outra...\n");
            i++;
        }
    }
}
MostrarMatriz(matrizJogador, tamanholinha, tamanhocoluna);
Console.WriteLine(" ");

//BOT ADICIONANDO NAVIOS
for (int i = quantNavios; i > 1; i--)
{
    MostrarMatriz(matrizBot, tamanholinha, tamanhocoluna);

    linha = Random.Shared.Next(1, 8);
    coluna = Random.Shared.Next(1, 8);
    direcao = direcoes[Random.Shared.Next(4)];

    Console.WriteLine($"\nBot tentando adicionar navio de {i} lugares");
    Console.WriteLine($"Linha: {linha}");
    Console.WriteLine($"Coluna: {coluna}");
    Console.WriteLine($"Direção: {direcao}");

    Thread.Sleep(2000);

    if (AdicionarNavios(matrizBot, linha, coluna, direcao, i) == false)
    {
        Console.WriteLine("DIREÇÃO INVÁLIDA!, tente outra...\n");
        i++;
    }
    else
    {
        Console.WriteLine($"Navio de [{i}] lugares adicionado com sucesso!\n");
    }
}
MostrarMatriz(matrizBot, tamanholinha, tamanhocoluna);
Console.WriteLine(" ");

//JOGAR
//IniciarPartida(matrizJogador, matrizBot);

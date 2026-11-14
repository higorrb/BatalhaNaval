int tamanholinha = 8, tamanhocoluna = 8, quantNavios = 5;

string[,] matrizJogador = new string[tamanholinha, tamanhocoluna];
string[,] matrizBot = new string[tamanholinha, tamanhocoluna];
string[,] matrizEspectroJogador = new string[tamanholinha, tamanhocoluna];
string[,] matrizEspectroBot = new string[tamanholinha, tamanhocoluna];

string[] direcoes = { "^", ">", "v", "<" };


int linha = 0, coluna = 0;
string direcao;

//GERAIS
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
    Thread.Sleep(1000);

}

//VALIDAÇÕES
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

//NAVIOS
bool AdicionarNavios(string[,] matriz, int linha, int coluna, string direcao, int tamanhoNavio)
{
    switch (direcao)
    {
        case "^":
            if (ValidarDireçãoCima(matriz, tamanhoNavio, linha, coluna) == 0)
            {
                for (int j = 0; j < tamanhoNavio; j++)
                {
                    matriz[linha - j, coluna] = IdentificarNavio(tamanhoNavio);
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
                    matriz[linha, coluna + j] = IdentificarNavio(tamanhoNavio);
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
                    matriz[linha + j, coluna] = IdentificarNavio(tamanhoNavio);
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
                    matriz[linha, coluna - j] = IdentificarNavio(tamanhoNavio);
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

int ContarNavios(string[,] matriz)
{
    int contador = 0;
    for (int i = 0; i < tamanholinha; i++)
    {
        for (int j = 0; j < tamanhocoluna; j++)
        {
            if (matriz[i, j] == "P" || matriz[i, j] == "E" || matriz[i, j] == "C" || matriz[i, j] == "D")
                contador++;
        }
    }
    return contador;
}

string IdentificarNavio(int tamanhoNavio)
{
    string tipoNavio;

    switch (tamanhoNavio)
    {
        case 5:
            tipoNavio = "P";
            return tipoNavio;
        case 4:
            tipoNavio = "E";
            return tipoNavio;
        case 3:
            tipoNavio = "C";
            return tipoNavio;
        case 2:
            tipoNavio = "D";
            return tipoNavio;
        default:
            return tamanhoNavio.ToString();
    }
}

//JOGAR
void IniciarPartida(string[,] matrizJogador, string[,] matrizBot)
{
    do
    {
        bool acertoJogador = true, acertoBot = true;

        while (acertoJogador == true && ContarNavios(matrizBot) > 0) 
        {
            Console.WriteLine("TURNO DO JOGADOR\t");
            //turno do jogador - mostrando o tabuleiro do bot
            Console.WriteLine("matriz do jogador:\t");
            MostrarMatriz(matrizJogador, tamanholinha, tamanhocoluna);
            Console.WriteLine("");
            Console.WriteLine("matriz para o jogador atacar:\t");
            MostrarMatriz(matrizEspectroBot, tamanholinha, tamanhocoluna);

            Console.Write("Digite a LINHA para encontrar o Navio ");
            linha = int.Parse(Console.ReadLine());

            Console.Write("Digite a COLUNA para encontrar o Navio: ");
            coluna = int.Parse(Console.ReadLine());

            if (ValidarCoordenadas(linha, coluna) == false)
            {
                Console.Write("COORDENADAS NAO EXISTEM, perdeu a vez! ");
                acertoJogador = false;
            }
            else
            {
                acertoJogador = VerificarPosicoes(matrizBot, matrizEspectroBot, linha, coluna);
            }
        }

        if (ContarNavios(matrizJogador) == 0) Console.WriteLine("O BOT VENCEU!!!");
        else if (ContarNavios(matrizBot) == 0) Console.WriteLine("O JOGADOR VENCEU!!!");

        Console.WriteLine("");

        while (acertoBot == true && ContarNavios(matrizJogador) > 0)
        {
            Console.WriteLine("TURNO DO BOT\t");
            //turno do bot - mostrando o tabuleiro do jogador
            Console.WriteLine("matriz do bot:\t");
            MostrarMatriz(matrizBot, tamanholinha, tamanhocoluna);
            Console.WriteLine("");
            Console.WriteLine("matriz para o bot atacar:\t");
            MostrarMatriz(matrizEspectroJogador, tamanholinha, tamanhocoluna);
            Thread.Sleep(2500);
            Console.WriteLine("");

            linha = Random.Shared.Next(1, 8);

            coluna = Random.Shared.Next(1, 8);

            acertoBot = VerificarPosicoes(matrizJogador, matrizEspectroJogador, linha, coluna);
            Thread.Sleep(1500);
        }

        if (ContarNavios(matrizJogador) == 0) Console.WriteLine("O BOT VENCEU!!!");
        else if (ContarNavios(matrizBot) == 0) Console.WriteLine("O JOGADOR VENCEU!!!");

    } while (ContarNavios(matrizJogador) != 0 && ContarNavios(matrizBot) != 0);
}

bool VerificarPosicoes(string[,] matriz1, string[,] matriz2, int linha, int coluna)
{
    if (matriz1[linha, coluna] == "X" || matriz1[linha, coluna] == "O")
    {
        Console.WriteLine("Essa posição já foi atacada!");
        Thread.Sleep(2500);
        return false;
    }

    if (matriz1[linha, coluna] != "~")
    {
        string navioAtingido = matriz1[linha, coluna];

        matriz1[linha, coluna] = "X";
        matriz2[linha, coluna] = "X";

        Console.WriteLine($"ACERTOU! Você atingiu o navio [{navioAtingido}]!");
        Thread.Sleep(2500);

        return true;
    }

    else
    {
        matriz1[linha, coluna] = "O";
        matriz2[linha, coluna] = "O";

        Console.WriteLine("ERROU! Você acertou a água!");
        Thread.Sleep(2500);

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

    Console.Write($"Digite a LINHA inicial para o navio [{IdentificarNavio(i)}] com [{i}] lugares: ");
    linha = int.Parse(Console.ReadLine());

    Console.Write($"Digite a COLUNA inicial para o navio [{IdentificarNavio(i)}] com [{i}] lugares: ");
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
        Thread.Sleep(2000);
        i++;
    }
    else
    {
        Console.WriteLine($"Navio de [{i}] lugares adicionado com sucesso!\n");
        Thread.Sleep(2000);
    }
}
MostrarMatriz(matrizBot, tamanholinha, tamanhocoluna);
Console.WriteLine(" ");

//JOGAR
IniciarPartida(matrizJogador, matrizBot);

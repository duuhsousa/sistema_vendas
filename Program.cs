using System;
using System.IO;

namespace sistema_vendas
{
    class Program
    {
        static void Main(string[] args)
        {
            string op2;
            do
            {
                
                Console.WriteLine("\nEscolha uma das opções abaixo\n1 - Cadastrar Clientes\n2 - Cadastrar Produtos\n3 - Realizar Vendas\n4 - Extrato de Clientes\n\n0 - Sair");
                do
                {
                    op2 = Console.ReadLine();
                } while (op2 != "1" && op2 != "2" && op2 != "3" && op2 != "4" && op2 != "0");

                switch (op2)
                {
                    case "0": Environment.Exit(0); break;
                    case "1": CadastrarClientes(); break;
                    case "2": CadastrarProdutos(); break;
                    case "3": RealizarVendas(); break;
                    case "4": ExtratoClientes(); break;
                }
            } while (op2 != "0");

        }

        private static void ExtratoClientes()
        {
            throw new NotImplementedException();
        }

        private static void RealizarVendas()
        {
            throw new NotImplementedException();
        }

        private static void CadastrarProdutos()
        {
            string[] produtos, temp;
            string op1;
            int cod;
            if(!File.Exists("Produtos.csv"))
            {
                File.Create("Produtos.csv").Close();
            }
            do
            {
                cod = 0;
                produtos = File.ReadAllLines("Produtos.csv");
                temp = produtos[produtos.Length-1].Split(';');
                cod = int.Parse(temp[0])+1;
                StreamWriter sw = new StreamWriter("Produtos.csv",true);
                sw.WriteLine();
                Console.WriteLine("\n### Cadastro de Produtos ###\n\nCód.Produto = "+cod);
                Console.Write("Digite o nome do Produto: ");
                sw.Write(cod+";"+Console.ReadLine());
                sw.Close();
                do
                {
                    Console.Write("\nDeseja realizar um novo cadastro? (S ou N)");
                    op1 = Console.ReadLine();
                } while (op1!="S" && op1!="N" && op1!="s" && op1!="n");
            } while(op1=="S" || op1=="s");
            
        }

        private static void CadastrarClientes()
        {
            throw new NotImplementedException();
        }
    }
}

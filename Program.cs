using System;
using System.IO;

namespace sistema_vendas
{
    class Program
    {
        static int[] chaveCPF = {10,9,8,7,6,5,4,3,2};
        static int[] chaveCPF2 = {11,10,9,8,7,6,5,4,3,2};
        static int[] chaveCNPJ = {5,4,3,2,9,8,7,6,5,4,3,2};
        static int[] chaveCNPJ2 = {6,5,4,3,2,9,8,7,6,5,4,3,2};
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
            int valid=0,valid2,valid3=0;
            string docCliente,codProduto;
            string[] clientes,produtos,vendas,findcliente=null,findproduto=null,findvenda=null;
            clientes = File.ReadAllLines("cliente.csv");
            produtos = File.ReadAllLines("Produtos.csv");
            vendas = File.ReadAllLines("Vendas.csv");
            do{
                do{
                    Console.Write("Digite o documento do Cliente: ");
                    docCliente = Console.ReadLine();     
                }while(docCliente.Length!=11 && docCliente.Length!=14);
                if(docCliente.Length==11){
                    Console.WriteLine("Validando CPF...");
                    valid = ValidarDocumento(docCliente,1);
                }else{
                    Console.WriteLine("Validando CNPJ...");
                    valid = ValidarDocumento(docCliente,2);
                } 
                if(valid==0)
                    Console.WriteLine("Documento Inválido!");        
            }while(valid==0);
            foreach(string cliente in clientes){
                    if(cliente.Contains(docCliente)){
                        findcliente = cliente.Split(';');
                }
            }
            Console.WriteLine("\nCliente: "+findcliente[1]+"\neMail: "+findcliente[2]+"\nCPF: "+findcliente[0]+"\n");
            foreach(string venda in vendas){
                if(venda.Contains(docCliente)){
                    findvenda = venda.Split(';');
                    foreach(string cliente in clientes){
                        if(cliente.Contains(findvenda[0])){
                            findcliente = cliente.Split(';');
                        }
                    }
                    foreach(string produto in produtos){
                        if(produto.Contains(findvenda[1])){
                            findproduto = produto.Split(';');
                        }
                    }
                    Console.WriteLine(findvenda[2]+"            "+findproduto[1]);
                }
            }
        }

        private static void RealizarVendas()
        {
            int valid=0,valid2,valid3=0;
            string docCliente,codProduto,op1;
            string[] clientes,produtos,findcliente=null,findproduto=null;
            clientes = File.ReadAllLines("cliente.csv");
            produtos = File.ReadAllLines("Produtos.csv");
            if(!File.Exists("Vendas.csv"))
            {
                File.Create("Vendas.csv").Close();
            }
            do{
                StreamWriter sw = new StreamWriter("Vendas.csv",true);
                do{
                    do{
                        Console.Write("Digite o documento do Cliente: ");
                        docCliente = Console.ReadLine();     
                    }while(docCliente.Length!=11 && docCliente.Length!=14);
                    if(docCliente.Length==11){
                        valid = ValidarDocumento(docCliente,1);
                    }else
                        valid = ValidarDocumento(docCliente,2);     
                }while(valid == 0);
                valid2=0;
                foreach(string cliente in clientes){
                    if(cliente.Contains(docCliente)){
                        findcliente = cliente.Split(';');
                        valid2 = 1;
                    }
                }
                
                if(valid2==1){
                    Console.WriteLine("\nCliente Encontrado");
                    Console.WriteLine("\nNome: "+findcliente[1]+"\nCPF: "+findcliente[0]+"\neMail: "+findcliente[2]+"\nDesde: "+findcliente[3]);
                    Console.WriteLine("\nLista de Produtos");
                    int cont=0;
                    foreach(string produto in produtos){
                        findproduto = produto.Split(';');
                        if(cont!=0){
                            Console.WriteLine(findproduto[0]+" - "+findproduto[1]);
                        }
                        cont=1;
                    }
                    sw.Close();
                    do{
                        sw = new StreamWriter("Vendas.csv",true);
                        do{
                            Console.Write("Digite o Código do Produto: ");
                            codProduto = Console.ReadLine();
                            foreach(string produto in produtos){
                                findproduto = produto.Split(';');
                                if(findproduto[0].Equals(codProduto)){
                                    valid3 = 1;
                                }
                            }
                        }while(valid3!=1);
                        sw.WriteLine(docCliente+";"+codProduto+";"+System.DateTime.Now.ToString());
                        sw.Close();
                        Console.WriteLine("Venda Realizada!");
                        do
                        {
                            Console.Write("\nDeseja realizar uma outra venda para "+findcliente[1]+"? (S ou N)");
                            op1 = Console.ReadLine();
                        } while (op1!="S" && op1!="N" && op1!="s" && op1!="n");
                    }while(op1=="S" || op1=="s");
                }else{
                    Console.WriteLine("\nCliente não Encontrado");

                }
                do
                {
                    Console.Write("\nDeseja realizar uma outra venda? (S ou N)");
                    op1 = Console.ReadLine();
                } while (op1!="S" && op1!="N" && op1!="s" && op1!="n");
            } while(op1=="S" || op1=="s");
        }

        private static int ValidarDocumento(String docCliente, int tipoCliente)
        {
            int[] chaveCPF = {10,9,8,7,6,5,4,3,2};
            int[] chaveCPF2 = {11,10,9,8,7,6,5,4,3,2};
            int[] chaveCNPJ = {5,4,3,2,9,8,7,6,5,4,3,2};
            int[] chaveCNPJ2 = {6,5,4,3,2,9,8,7,6,5,4,3,2};
            int[] chave1;
            int[] chave2;
            string tipoDoc;
            string primeiroDigito, segundoDigito;

            if(tipoCliente==1){
                tipoDoc = "CPF";
                chave1  = chaveCPF;
                chave2 = chaveCPF2;
            }
            else{
                tipoDoc = "CNPJ";
                chave1 = chaveCNPJ;
                chave2 = chaveCNPJ;
            }

            primeiroDigito = ValidaDigito(docCliente,chave1,tipoCliente);

            if (primeiroDigito != docCliente.Substring(chave1.Length,1))
            {
                Console.WriteLine(tipoDoc+" inválido!");
            }
            else
            {
                segundoDigito = ValidaDigito(docCliente,chave2,tipoCliente);

            if (docCliente.EndsWith(segundoDigito) == true)
            {
                return 1;
            }
            else
            {
                Console.WriteLine(tipoDoc+" inválido!");
            }
            }
        return 0;
        }

        private static string ValidaDigito(string doc, int[] chave, int tipoDoc)
        {
       int soma = 0, resto = 0;
       string tempdoc;
       tempdoc = doc.Substring(0,chave.Length);
       for(int i=0;i<chave.Length;i++){
                soma += Convert.ToInt16(tempdoc[i].ToString())*chave[i];
        }
            resto = soma % 11;
            
            if(resto<2)
            {
                return "0";        
            }
            else
            {
                /*if(resto==10 && tipoDoc==1){
                    return "0";
                }*/
                
                return (11-resto).ToString();
            }

    }

        private static void CadastrarProdutos()
        {
            string[] produtos, temp;
            string op1;
            int cod;
            //Verifica se o arquivo existe
            if(!File.Exists("Produtos.csv"))
            {
                File.Create("Produtos.csv").Close();
                StreamWriter sw = new StreamWriter("Produtos.csv",true);
                sw.Write("0;DescProdutos");
                sw.Close();
            }
            //Inicio do Cadastro
            do
            {
                cod = 0;
                produtos = File.ReadAllLines("Produtos.csv");
                //Leitura do código da última linha
                temp = produtos[produtos.Length-1].Split(';');
                cod = int.Parse(temp[0])+1;
                //Inicio da escrita em arquivo
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
            string nomeCliente;
            string emailCliente;
            string tipoCliente;
            string docCliente;
            string op1;
            int valid = 0;
            int duplicado;
            do{
                Console.WriteLine("\nCADASTRO DE CLIENTES: \n");
                do{
                    Console.Write("Digite 1 para CPF e 2 para CNPJ: ");
                    tipoCliente = Console.ReadLine();
                }while(tipoCliente!="1" && tipoCliente!="2");
                do{
                    if(tipoCliente=="1"){ 
                        do{
                            Console.Write("CPF: ");
                            docCliente = Console.ReadLine();
                            duplicado = PesquisaDocumento(docCliente);    
                        }while(docCliente.Length!=11 || duplicado!=0);
                        Console.WriteLine(docCliente);
                        valid = ValidarDocumento(docCliente, 1);
                        Console.WriteLine(valid);
                    }
                    else{
                        do{
                            Console.Write("CNPJ: ");
                            docCliente = Console.ReadLine();    
                            duplicado = PesquisaDocumento(docCliente);    
                        }while(docCliente.Length!=11 || duplicado!=0);
                        valid = ValidarDocumento(docCliente, 2);
                    }
                }while(valid!=1);

                StreamWriter writer = new StreamWriter("cliente.csv",true);
                Console.Write("Nome completo: ");
                nomeCliente = Console.ReadLine();
                Console.Write("Email: ");
                emailCliente = Console.ReadLine();
                writer.WriteLine(docCliente+";"+nomeCliente+";"+emailCliente+";"+System.DateTime.Now.ToString()+";");
                writer.Close();
                do
                {
                    Console.Write("\nDeseja realizar um novo cadastro? (S ou N)");
                    op1 = Console.ReadLine();
                } while (op1!="S" && op1!="N" && op1!="s" && op1!="n");
            } while(op1=="S" || op1=="s");
        }

        private static int PesquisaDocumento(string docCliente)
        {
            if(File.Exists("cliente.csv")){
                String[] clientes = File.ReadAllLines("cliente.csv");
                String[] dadosCliente;

                foreach(string cliente in clientes){
                    dadosCliente = cliente.Split(';');
                    if(dadosCliente[0].Equals(docCliente)){
                        Console.WriteLine("\nCliente já cadastrado no sistema!\n");
                        return 1;
                    }
                }
            }
            return 0;
        }
    }
}

using System;
using System.Collections.Generic;

namespace bancoDio
{
    public enum TipoConta { 
		PessoaFisica = 1,
		PessoaJuridica = 2
	}
	
	public class Conta { 
		private TipoConta TipoConta { get; set; }
		private double Saldo { get; set; }
		private double Credito { get; set; }
		private string Nome { get; set; }
		
		public Conta(TipoConta tipoConta, double saldo, double credito, string nome) {
			this.TipoConta = tipoConta;
			this.Saldo = saldo;
			this.Credito = credito;
			this.Nome = nome;
		}
		
		public bool Sacar(double valorSaque) { 
			if(this.Saldo - valorSaque < (this.Credito * -1)) { 
				Console.WriteLine("Saldo insuficiente");
				return false;
			}
			
			this.Saldo -= valorSaque;
			Console.WriteLine($"Novo saldo na conta de {this.Nome}: R$ {this.Saldo}");
			return true;
		}
		
		public void Depositar(double valorDeposito) { 
			this.Saldo += valorDeposito;
			Console.WriteLine($"Novo saldo na conta de {this.Nome}: R$ {this.Saldo}");
		}
		
		public void Transferir(double valorTransferir, Conta contaDestino) { 
			if(this.Sacar(valorTransferir)) { 
				contaDestino.Depositar(valorTransferir);
			}
		}
		
		public override string ToString() { 
			string textoRetorno = "";
			textoRetorno += "Tipo de Conta: " + this.TipoConta + " - ";
			textoRetorno += "Saldo: " + this.Saldo + " - ";
			textoRetorno += "Limite de Cheque: " + this.Credito + " - ";
			textoRetorno += "Nome do titular: " + this.Nome;
			return textoRetorno;
		}
	}

	public class Program {
		static List<Conta> listContas = new List<Conta>();
		
		public static void Main(string[] args) { 
			string op = EntryPoint();
			while(op != "6") {
				switch(op) {
					case "1":
						ListarContas();
						break;
					case "2":
						InserirContas();
						break;
					case "3":
						SacarConta();
						break;
					case "4":
						DepositarConta();
						break;
					case "5":
						TransferirEntreContas();
						break;
						
					default:
						throw new ArgumentOutOfRangeException();
				}
				
				op = EntryPoint();
			}
			
			Console.WriteLine("Obrigado por utilizar nossos Serivços!");
			Console.ReadLine();
		}
		
		public static string EntryPoint() { 
			string textoRetorno = "";
			textoRetorno += "Bem vindo ao Banco Digital DIO  \n";
			textoRetorno += "Informe a Opção desejada:  \n\n";
			textoRetorno += "1: Listar Contas Existentes  \n";
			textoRetorno += "2: Inserir nova Conta  \n";
			textoRetorno += "3: Realizar saque de Conta  \n";
			textoRetorno += "4: Realizar depósito de Conta  \n";
			textoRetorno += "5: Realizar transferência entre Contas  \n";
			textoRetorno += "6: Sair do Sistema  \n";
			
			Console.Write(textoRetorno);
			string opcaoEntrada = Console.ReadLine();
			Console.WriteLine();
			return opcaoEntrada;
		}
		
		private static void InserirContas() {
			Cabecalho("Cadastrar nova Conta: ");
			Console.Write("Digite 1: PF ou 2: PJ: ");
			int entradaTipoConta = int.Parse(Console.ReadLine());
			Console.Write("Digite o nome do Titular: ");
			string nomeTitular = Console.ReadLine();
			Console.Write("Digite o saldo de abertura: ");
			double saldoInicial = double.Parse(Console.ReadLine());
			Console.Write("Digite o crédito inicial: ");
			double creditoConta = double.Parse(Console.ReadLine());
			Conta newConta = new Conta((TipoConta)entradaTipoConta, saldoInicial, creditoConta, nomeTitular);
			listContas.Add(newConta);
			Rodape("Conta cadastrada com sucesso");
		}
		
		private static void ListarContas() {
			Cabecalho("Listagem de Contas: ");
			if(listContas.Count == 0) {
				Console.WriteLine("Nenhuma conta cadastrada!");
				Console.ReadLine();
				Console.Clear();
				return;
			}
			for(int i = 0; i < listContas.Count; i++) {
				Conta conta = listContas[i];
				Console.Write($"{i} - ");
				Console.WriteLine(conta);
			}
			Rodape("-- Fim da Listagem --");
		}
		
		private static void SacarConta() {
			Cabecalho("Realizar Saque: ");
			Console.Write("Informe o numero da Conta: ");
			int numeroConta = int.Parse(Console.ReadLine());
			Console.Write("Digite o valor ser retirado: ");
			double valorSaque = double.Parse(Console.ReadLine());
			listContas[numeroConta].Sacar(valorSaque);
			Rodape("-- Fim da Operação --");
		}
		
		private static void TransferirEntreContas() {
            Cabecalho("Realizar Transferencia: ");
			Console.Write("Informe o numero da Conta de origem: ");
			int numeroConta1 = int.Parse(Console.ReadLine());
			Console.Write("Informe o numero da Conta de destino: ");
			int numeroConta2 = int.Parse(Console.ReadLine());
			Console.Write("Digite o valor ser depositado: ");
			double valorDeposito = double.Parse(Console.ReadLine());
			listContas[numeroConta1].Transferir(valorDeposito, listContas[numeroConta2]);
			Rodape("-- Fim da Operação --");
		}
		
		private static void DepositarConta() {
            Cabecalho("Realizar Depósito: ");
			Console.Write("Informe o numero da Conta: ");
			int numeroConta = int.Parse(Console.ReadLine());
			Console.Write("Digite o valor ser depositado: ");
			double valorDeposito = double.Parse(Console.ReadLine());
			listContas[numeroConta].Depositar(valorDeposito);
			Rodape("-- Fim da Operação --");
		}

        public static void Rodape(string text) {
            Console.WriteLine(text);
			Console.ReadLine();
			Console.Clear();
        }

        public static void Cabecalho(string text) {
            Console.Clear();
			Console.WriteLine(text);
        }
	}
}

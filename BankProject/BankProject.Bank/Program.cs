using System;
using System.Collections.Generic;

namespace BankProject.Bank
{
    class Program
    {
        static List<Account> listAccounts = new List<Account>();
        static void Main(string[] args)
        {
            string userOption = GetUserOptions();

            while (userOption.ToUpper() != "X")
            {
                switch (userOption)
                {
                    case "1":
                        ListAccounts();
                        break;
                    case "2":
                        InsertAccount();
                        break;
                    case "3":
                        Transfer();
                        break;
                    case "4":
                        Withdraw();
                        break;
                    case "5":
                        Deposit();
                        break;
                    case "C":
                        Console.Clear();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                userOption = GetUserOptions();
            }
        }

        private static void Transfer()
        {
            Console.WriteLine("Digite o número da conta origem: ");
            int OriginAccountIndex = int.Parse(Console.ReadLine());

            Console.WriteLine("Digite o número da conta destino: ");
            int DestinationAccountIndex = int.Parse(Console.ReadLine());

            Console.WriteLine("Digite o valor a ser transferido: ");
            double transferValue = double.Parse(Console.ReadLine());

            listAccounts[OriginAccountIndex].Transfer(transferValue, listAccounts[DestinationAccountIndex]);
        }

        private static void Deposit()
        {
            Console.WriteLine("Digite o número da conta: ");
            int accountIndex = int.Parse(Console.ReadLine());

            Console.WriteLine("Digite o valor a ser depositado: ");
            double depositValue = double.Parse(Console.ReadLine());

            listAccounts[accountIndex].Deposit(depositValue);
        }

        private static void Withdraw()
        {
            Console.WriteLine("Digite o número da conta: ");
            int accountIndex = int.Parse(Console.ReadLine());

            Console.WriteLine("Digite o valor a ser sacado: ");
            double withdrawValue = double.Parse(Console.ReadLine());

            listAccounts[accountIndex].Withdraw(withdrawValue);
        }

        private static void ListAccounts()
        {
            Console.WriteLine("Listar contas");

            if (listAccounts.Count == 0)
            {
                Console.WriteLine("Nenhuma conta cadastrada");
                return;
            }

            for (int i = 0; i < listAccounts.Count; i++)
            {
                Account account = listAccounts[i];
                Console.Write("#{0}  -  ", i);
                Console.WriteLine(account);
            }
        }

        private static void InsertAccount()
        {
            Console.WriteLine("Inserir nova conta");

            Console.WriteLine("Digite 1 para Conta física ou 2 para Jurídica: ");
            int newAccountType = int.Parse(Console.ReadLine());

            Console.WriteLine("Digite o nome do Cliente: ");
            string newAccountName = Console.ReadLine();

            Console.WriteLine("Digite o saldo inicial: ");
            double newAccountBalance = double.Parse(Console.ReadLine());

            Console.WriteLine("Digite o crédito: ");
            double newAccountCredit = double.Parse(Console.ReadLine());

            Account newAccount = new Account(
                                                accountType: (AccountType)newAccountType,
                                                balance: newAccountBalance,
                                                credit: newAccountCredit,
                                                name: newAccountName
                                            );

            listAccounts.Add(newAccount);
        }

        private static string GetUserOptions()
        {
            Console.WriteLine();
            Console.WriteLine("DIO Bank a seu dispor.");
            Console.WriteLine("Informe a opção desejada");

            Console.WriteLine("1- Listar contas");
            Console.WriteLine("2- Inserir nova conta");
            Console.WriteLine("3- Transferir");
            Console.WriteLine("4- Sacar");
            Console.WriteLine("5- Depositar");
            Console.WriteLine("C- Limpar tela");
            Console.WriteLine("X- Sair");
            Console.WriteLine();

            string userOption = Console.ReadLine().ToUpper();
            Console.WriteLine();
            return userOption;
        }
    }
}

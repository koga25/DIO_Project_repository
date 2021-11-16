using System;

namespace BankProject.Bank
{
    public class Account
    {
        private AccountType AccountType { get; set; }
        private double Balance { get; set; }
        private double Credit { get; set; }
        private string Name { get; set; }

        public Account(AccountType accountType, double balance, double credit, string name)
        {
            this.AccountType = accountType;
            this.Balance = balance;
            this.Credit = credit;
            this.Name = name;
        }

        public bool Withdraw(double withdrawValue)
        {
            if ((this.Balance - withdrawValue) < (this.Credit * -1))
            {
                Console.WriteLine("Saldo Insuficiente");
                return false;
            }

            this.Balance -= withdrawValue;
            Console.WriteLine("Saldo atual da conta de {0} é {1}", this.Name, this.Balance);
            return true;
        }

        public void Deposit(double depositValue)
        {
            this.Balance += depositValue;
            Console.WriteLine("Saldo atual da conta de {0} é {1}", this.Name, this.Balance);
        }

        public void Transfer(double transferValue, Account destinationAccount)
        {
            if (this.Withdraw(transferValue))
            {
                destinationAccount.Deposit(transferValue);
            }
        }

        public override string ToString()
        {
            string BankString = "";
            BankString += "TipoConta " + this.AccountType + " | ";
            BankString += "Nome " + this.Name + " | ";
            BankString += "Saldo " + this.Balance + " | ";
            BankString += "Crédito " + this.Credit;
            return BankString;
        }
    }
}
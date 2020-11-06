﻿using RoomBooking.Core.Contracts;
using RoomBooking.Core.Entities;
using RoomBooking.Persistence;
using RoomBooking.Wpf.Common;
using RoomBooking.Wpf.Common.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Windows.Input;

namespace RoomBooking.Wpf.ViewModels
{
    public class EditCustomerViewModel :BaseViewModel
    {
        private Customer _customer;
        private string _lastname;
        private string _firstname;
        private string _iban;

        public Customer Customer
        {
            get => _customer;
            set
            {
                _customer = value;
                OnPropertyChanged(nameof(Customer));
            }
        }

        public string LastName
        {
            get => _lastname;
            set
            {
                _lastname = value;
                OnPropertyChanged(nameof(LastName));
            }
        }

        public string FirstName
        {
            get => _firstname;
            set
            {
                _firstname = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }

        public string Iban
        {
            get => _iban;
            set
            {
                _iban = value;
                OnPropertyChanged(nameof(Iban));
            }
        }


        public EditCustomerViewModel(IWindowController controller, Customer customer) : base(controller)
        {
            Customer = customer;
            Init();
       
        }

        private void Init()
        {
            FirstName = Customer.FirstName;
            LastName = Customer.LastName;
            Iban = Customer.Iban;
        }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }

        private ICommand _cmdSave;

        public ICommand CmdSave
        {
            get
            {
                if (_cmdSave == null)
                {
                    _cmdSave = new RelayCommand(
                      execute: _ =>
                      {
                          using IUnitOfWork uow = new UnitOfWork();
                          _customer.FirstName = FirstName;
                          _customer.LastName = LastName;
                          _customer.Iban = Iban;
                          uow.Customers.Update(_customer);
                          uow.SaveAsync();
                          Controller.CloseWindow(this);

                      },
                      canExecute: _ => _customer != null);
                
                }
                return _cmdSave;
            }
            set { _cmdSave = value; }
        }

        private ICommand _cmdUndo;

        public ICommand CmdUndo
        {
            get
            {
                if (_cmdUndo == null)
                {
                    _cmdUndo = new RelayCommand(
                      execute: _ =>
                      {
                          Init();

                      },
                      canExecute: _ => true);

                }
                return _cmdUndo;
            }
        }

    }
}

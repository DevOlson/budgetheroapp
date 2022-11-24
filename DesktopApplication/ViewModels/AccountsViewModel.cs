﻿using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DesktopApplication.Contracts.Data;
using DesktopApplication.Contracts.Services;
using DesktopApplication.CustomEventArgs;
using DesktopApplication.Helpers;
using DesktopApplication.Models;
using DesktopApplication.ViewModels.Forms;
using DesktopApplication.Views.Forms;
using ModelsLibrary;

namespace DesktopApplication.ViewModels;

public class AccountsViewModel : ObservableRecipient
{
    private readonly ISessionService _sessionService;
    private readonly IDialogService _dialogService;
    private readonly IDataStore _dataStore;

    public AccountsViewModel()
    {
        _sessionService = App.GetService<ISessionService>();
        _dialogService = App.GetService<IDialogService>();
        _dataStore = App.GetService<IDataStore>();

        ShowAddDialogCommand = new AsyncRelayCommand(ShowAddDialog);
        ShowEditDialogCommand = new AsyncRelayCommand(ShowEditDialog);
        ShowDeleteDialogCommand = new AsyncRelayCommand(ShowDeleteDialog);
    }

    public IAsyncRelayCommand ShowAddDialogCommand { get; }
    public IAsyncRelayCommand ShowEditDialogCommand { get; }
    public IAsyncRelayCommand ShowDeleteDialogCommand { get; }

    private IEnumerable<Transaction>? allTransactions;
    public ObservableCollection<ObservableBankAccount> BankAccounts { get; set; } = new();
    public ObservableCollection<Transaction> AccountTransactions { get; set; } = new();
    
    private ObservableBankAccount? _selectedBankAccount;
    public ObservableBankAccount? SelectedBankAccount
    {
        get => _selectedBankAccount;
        set
        {
            SetProperty(ref _selectedBankAccount, value);
            if (value is not null)
                FilterAccountTransactions(value.BankAccount.BankAccountId);
            HasItemSelected = value is not null;
        }
    }

    private bool _hasItemSelected;
    public bool HasItemSelected
    {
        get => _hasItemSelected;
        set => SetProperty(ref _hasItemSelected, value);
    }

    public async Task LoadAsync()
    {
        if (BankAccounts.Any()) return;

        IEnumerable<BankAccount?> bankAccounts = await _dataStore.BankAccount.ListAsync(a => a.UserId == _sessionService.GetSessionUserId());
        if (bankAccounts is not null)
        {
            foreach (var bankAccount in bankAccounts)
            {
                BankAccounts.Add(new ObservableBankAccount(bankAccount!));
            }
        }

        allTransactions = _dataStore.Transaction.GetAll(t => t.BankAccount.UserId == _sessionService.GetSessionUserId());
    }

    private async Task ShowAddDialog()
    {
        _dialogService.OnSaved += AddBankAccountAsync;
        
        string dialogTitle = "Add Account";        
        await _dialogService.ShowDialogAsync<BankAccountForm>(dialogTitle);

        _dialogService.OnSaved -= AddBankAccountAsync;
    }

    private async Task ShowEditDialog()
    {
        _dialogService.OnSaved += EditBankAccountAsync;
        
        string dialogTitle = "Edit Account";
        BankAccount mutableBankAccount = EntityUtilities.Duplicate(SelectedBankAccount!.BankAccount);
        await _dialogService.ShowDialogAsync<BankAccountForm>(dialogTitle, mutableBankAccount);
        
        _dialogService.OnSaved -= EditBankAccountAsync;
    }

    private async Task ShowDeleteDialog()
    {
        _dialogService.OnSaved += DeleteBankAccountAsync;

        string dialogTitle = "Delete Account";
        BankAccount _selectedBankAccount = SelectedBankAccount!.BankAccount;
        await _dialogService.ShowDialogAsync<DeleteItemForm>(dialogTitle, _selectedBankAccount);

        _dialogService.OnSaved -= DeleteBankAccountAsync;
    }

    private async void AddBankAccountAsync(object? sender, DialogServiceEventArgs e)
    {
        BankAccount newBankAccount = GetBankAccount(e);
        newBankAccount.UserId = _sessionService.GetSessionUserId();

        int result = await _dataStore.BankAccount.AddAsync(newBankAccount);

        if (result == 1)
        {
            BankAccounts.Add(new ObservableBankAccount(newBankAccount));
        }
    }

    private async void EditBankAccountAsync(object? sender, DialogServiceEventArgs e)
    {
        BankAccount existingBankAccount = SelectedBankAccount!.BankAccount;
        existingBankAccount = EntityUtilities.Update(existingBankAccount, GetBankAccount(e));

        ObservableBankAccount? listedBankAccount = BankAccounts.FirstOrDefault(
            a => a.BankAccount.BankAccountId == existingBankAccount.BankAccountId);
        int index;

        if (listedBankAccount is not null)
        {
            await _dataStore.BankAccount.Update(existingBankAccount);

            index = BankAccounts.IndexOf(listedBankAccount);
            BankAccounts[index].BankAccount = existingBankAccount;
        }
    }

    private async void DeleteBankAccountAsync(object? sender, DialogServiceEventArgs e)
    {
        BankAccount selectedBankAccount = _selectedBankAccount!.BankAccount;
        await _dataStore.BankAccount.DeleteAsync(selectedBankAccount);

        BankAccounts.Remove(_selectedBankAccount);
    }

    private static BankAccount GetBankAccount(DialogServiceEventArgs e)
    {
        BankAccountForm accountForm = (BankAccountForm)e.Content;
        return accountForm.ViewModel.BankAccount;
    }

    private void FilterAccountTransactions(int accountId)
    {
        if (allTransactions is not null)
        {
            AccountTransactions.Clear();
            List<Transaction> accountTransactions = allTransactions.Where(t => t.BankAccountId == accountId)
                                                                   .OrderByDescending(t => t.TransactionDate).ToList();
            accountTransactions.ForEach(AccountTransactions.Add);
        }
    }
}
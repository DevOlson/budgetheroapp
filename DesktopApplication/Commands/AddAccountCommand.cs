﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesktopApplication.Contracts.Services;

namespace DesktopApplication.Commands;
internal class AddAccountCommand : CommandBase
{
    private readonly IDialogService _dialogService;

    public AddAccountCommand()
    {
        _dialogService = App.GetService<IDialogService>();
    }

    public override void Execute(object? parameter)
    {
        _dialogService.AddAccountDialog();
    }
}

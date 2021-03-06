﻿using Ringen.Core.UI;
using System;
using System.Collections.Generic;

namespace Ringen.Core.Services
{
    public interface IPluginService
    {
        IReadOnlyList<IButton> RegisteredButtons { get; }
        IReadOnlyList<IButton> RegisteredMenuButtons { get; }

        void OpenMainPage(Type plugin);
        void RegisterButton(IButton button);
        void RegisterMenuButton(IButton button);
        //void RegisterSettings(PluginSystem.IPlugabble plugin, Type settingsInterface, Type settingsImplementation);
        void Close(Type plugin);
        void InitializeSystem();
        void OnHostLoaded();
    }
}
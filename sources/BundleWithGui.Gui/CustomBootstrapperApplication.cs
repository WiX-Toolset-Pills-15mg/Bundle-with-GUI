﻿// Wix Toolset Pills 15mg
// Copyright (C) 2019-2021 Dust in the Wind
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Windows.Threading;
using BundleWithGui.Gui.ViewModels;
using BundleWithGui.Gui.Views;
using Microsoft.Tools.WindowsInstallerXml.Bootstrapper;

namespace BundleWithGui.Gui
{
    public class CustomBootstrapperApplication : BootstrapperApplication
    {
        private static Dispatcher bootstrapperDispatcher;

        protected override void Run()
        {
            Engine.Log(LogLevel.Verbose, "Launching custom Bootstrapper Application UX");

            bootstrapperDispatcher = Dispatcher.CurrentDispatcher;

            MainViewModel viewModel = new MainViewModel(this);

            MainWindow view = new MainWindow { DataContext = viewModel };
            view.Closed += HandleViewClosed;

            Engine.Detect();
            view.Show();

            Dispatcher.Run();

            Engine.Quit(0);
        }

        private static void HandleViewClosed(object sender, EventArgs e)
        {
            bootstrapperDispatcher.InvokeShutdown();
        }

        public void InvokeShutDown()
        {
            bootstrapperDispatcher.InvokeShutdown();
        }

        protected override void OnPlanComplete(PlanCompleteEventArgs args)
        {
            base.OnPlanComplete(args);

            if (args.Status >= 0)
                Engine.Apply(System.IntPtr.Zero);
        }
    }
}
﻿// WiX Toolset Pills 15mg
// Copyright (C) 2019-2022 Dust in the Wind
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
using DustInTheWind.BundleWithCustomGui.CustomBootstrapperApplication.Presentation;
using Microsoft.Tools.WindowsInstallerXml.Bootstrapper;

namespace DustInTheWind.BundleWithCustomGui.CustomBootstrapperApplication
{
    public class CustomBootstrapperApplication : BootstrapperApplication
    {
        private GuiApplication guiApplication;

        protected override void Run()
        {
            try
            {
                Engine.Log(LogLevel.Verbose, "Launching custom Bootstrapper Application UX");

                WixEngine wixEngine = new WixEngine(this);
                guiApplication = new GuiApplication(wixEngine);
                guiApplication.Run();

                Engine.Quit(0);
            }
            catch (Exception ex)
            {
                Engine.Log(LogLevel.Error, $"ERROR: {ex}");
            }
        }

        public void InvokeShutdown()
        {
            guiApplication.InvokeShutdown();
        }
    }
}
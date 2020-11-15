///////////////////////////////////////////////////////////////////////////////////////////////////
// This file is part of the Breezy project (https://github.com/griffinplus/breezy)
//
// Copyright (c) 2020 Sascha Falk <sascha@falk-online.eu>
//
// The source code is licensed under the AGPL-3.0 license.
//
// This code is free software: you can redistribute it and/or modify
// it under the terms of the GNU Affero General Public License, version 3,
// as published by the Free Software Foundation.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU Affero General Public License for more details.
//
// You should have received a copy of the GNU Affero General Public License, version 3,
// along with this program. If not, see <http://www.gnu.org/licenses/>
///////////////////////////////////////////////////////////////////////////////////////////////////

using System.Reflection;

[assembly: AssemblyCompany("Griffin+")]
[assembly: AssemblyProduct("Breezy")]
[assembly: AssemblyCopyright("Copyright (c) 2020 Sascha Falk and Contributors")]
[assembly: AssemblyVersion("0.0.0.0")]
[assembly: AssemblyFileVersion("0.0.0.0")]
[assembly: AssemblyInformationalVersion("--GENERATED--")]

#if DEBUG
[assembly: AssemblyConfiguration("Debug")]
#else
[assembly: AssemblyConfiguration("Release")]
#endif